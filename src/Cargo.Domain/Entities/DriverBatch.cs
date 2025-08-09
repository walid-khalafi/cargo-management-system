using System;
using System.Collections.Generic;
using System.Linq;
using Cargo.Domain.Enums;
using Cargo.Domain.ValueObjects;

namespace Cargo.Domain.Entities
{
    /// <summary>
    /// Aggregate root: driver statement(batch) with snapshot of rates, ownership, and taxes.
    /// Matches Excel/PDF columns and preserves historical correctness.
    /// </summary>
    public class DriverBatch : BaseEntity
    {
        private readonly List<DriverBatchLoad> _loads;
        private readonly List<DriverBatchWait> _waits;
        private readonly List<DriverBatchHourly> _hourlies;

        // Identity & period
        public string BatchNumber { get; private set; }
        public Guid DriverId { get; private set; }
        public virtual Driver Driver { get; private set; }
        public DateTime StatementStartDate { get; private set; }
        public DateTime StatementEndDate { get; private set; }

        // OPTIONAL: Link to the specific vehicle ownership used in this batch (for audit)
        public Guid? VehicleOwnershipId { get; private set; }
        public virtual VehicleOwnership? VehicleOwnership { get; private set; }

        // Snapshot of ownership at the time of the batch (to drive commission/admin rules)
        public OwnershipType OwnershipTypeAtBatch { get; private set; } = OwnershipType.OwnedByFleet;

        // Dray components (if your template/Gross includes them; otherwise keep 0)
        public decimal DrayPay { get; private set; }
        public decimal DrayFsc { get; private set; }
        public decimal TemporaryEmergencyFsc { get; private set; }
        public decimal DrayTotal => Round2(DrayPay + DrayFsc + TemporaryEmergencyFsc);

        // Waiting payout factor (e.g., 0.5m for 50%)
        public decimal WaitingPayoutPercentage { get; private set; }

        // Commission / Share for company drivers (e.g., 0.40m == 40%)
        // For owner-operators set to 1.0m typically.
        public decimal DriverSharePercentage { get; private set; }

        // Admin fee snapshot (applies to owner-operators; percent in 0..1, flat in currency)
        public decimal AdminFeePercent { get; private set; } // e.g., 0.07m for 7%; set 0 if not used
        public decimal AdminFeeFlat { get; private set; }    // fixed amount; 0 if not used
        public bool AdminFeeAppliesBeforeTaxes { get; private set; } // default false

        // Adjustments (e.g., Truckwash -20) applied after taxes by default
        public decimal AdjustmentsTotal { get; private set; }

        // Snapshot tax profile (store rates and rules exactly as used on invoice)
        public TaxProfile TaxProfile { get; private set; }

        // Collections
        public IReadOnlyCollection<DriverBatchLoad> Loads => _loads.AsReadOnly();
        public IReadOnlyCollection<DriverBatchWait> Waits => _waits.AsReadOnly();
        public IReadOnlyCollection<DriverBatchHourly> Hourlies => _hourlies.AsReadOnly();

        // Totals
        public decimal TripTotal { get; private set; }
        public decimal WaitingRawTotal { get; private set; }
        public decimal WaitingTotal { get; private set; }
        public decimal HourlyTotal { get; private set; }
        public decimal WaitPay { get; private set; } // mirror for template
        public decimal GrossRevenue { get; private set; }

        // Share + taxes
        public decimal DriverShareAmount { get; private set; }
        public TaxAmounts Taxes { get; private set; } // GST/QST/PST/HST breakdown
        public decimal NetPay { get; private set; }
        public decimal TotalPay => NetPay;

        public BatchStatus Status { get; private set; }

        private DriverBatch() { } // EF

        public DriverBatch(
            string batchNumber,
            Driver driver,
            DateTime statementStartDate,
            DateTime statementEndDate,
            OwnershipType ownershipTypeAtBatch,
            TaxProfile taxProfile,
            decimal waitingPayoutPercentage,
            decimal driverSharePercentage,
            decimal adminFeePercent = 0m,
            decimal adminFeeFlat = 0m,
            bool adminFeeAppliesBeforeTaxes = false,
            Guid? vehicleOwnershipId = null)
        {
            if (string.IsNullOrWhiteSpace(batchNumber))
                throw new ArgumentException("Batch number cannot be empty.", nameof(batchNumber));
            if (statementStartDate.Date > statementEndDate.Date)
                throw new ArgumentException("Statement start date must be before or equal to end date.");
            if (!IsFraction(waitingPayoutPercentage))
                throw new ArgumentOutOfRangeException(nameof(waitingPayoutPercentage), "Must be between 0 and 1.");
            if (!IsFraction(driverSharePercentage))
                throw new ArgumentOutOfRangeException(nameof(driverSharePercentage), "Must be between 0 and 1.");
            if (!IsFraction(adminFeePercent))
                throw new ArgumentOutOfRangeException(nameof(adminFeePercent), "Must be between 0 and 1.");

            BatchNumber = batchNumber.Trim();
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
            DriverId = driver.Id;
            StatementStartDate = statementStartDate.Date;
            StatementEndDate = statementEndDate.Date;

            OwnershipTypeAtBatch = ownershipTypeAtBatch;
            TaxProfile = taxProfile ?? throw new ArgumentNullException(nameof(taxProfile));

            WaitingPayoutPercentage = waitingPayoutPercentage;
            DriverSharePercentage = driverSharePercentage;

            AdminFeePercent = adminFeePercent;
            AdminFeeFlat = adminFeeFlat;
            AdminFeeAppliesBeforeTaxes = adminFeeAppliesBeforeTaxes;

            VehicleOwnershipId = vehicleOwnershipId;

            _loads = new List<DriverBatchLoad>();
            _waits = new List<DriverBatchWait>();
            _hourlies = new List<DriverBatchHourly>();

            Status = BatchStatus.Draft;

            RecalculateTotals();
        }

        // Mutations (Draft only)
        public void AddLoad(DriverBatchLoad load)
        {
            EnsureMutable(); if (load == null) throw new ArgumentNullException(nameof(load));
            _loads.Add(load); RecalculateTotals();
        }

        public void AddWait(DriverBatchWait wait)
        {
            EnsureMutable(); if (wait == null) throw new ArgumentNullException(nameof(wait));
            _waits.Add(wait); RecalculateTotals();
        }

        public void AddHourly(DriverBatchHourly hourly)
        {
            EnsureMutable(); if (hourly == null) throw new ArgumentNullException(nameof(hourly));
            _hourlies.Add(hourly); RecalculateTotals();
        }

        public void SetDray(decimal drayPay, decimal drayFsc, decimal tempEmergencyFsc)
        {
            EnsureMutable();
            DrayPay = drayPay; DrayFsc = drayFsc; TemporaryEmergencyFsc = tempEmergencyFsc;
            RecalculateTotals();
        }

        public void SetPercentages(decimal waitingPayoutPercentage, decimal driverSharePercentage)
        {
            EnsureMutable();
            if (!IsFraction(waitingPayoutPercentage)) throw new ArgumentOutOfRangeException(nameof(waitingPayoutPercentage));
            if (!IsFraction(driverSharePercentage)) throw new ArgumentOutOfRangeException(nameof(driverSharePercentage));
            WaitingPayoutPercentage = waitingPayoutPercentage;
            DriverSharePercentage = driverSharePercentage;
            RecalculateTotals();
        }

        public void SetAdminFees(decimal adminFeePercent, decimal adminFeeFlat, bool appliesBeforeTaxes = false)
        {
            EnsureMutable();
            if (!IsFraction(adminFeePercent)) throw new ArgumentOutOfRangeException(nameof(adminFeePercent));
            AdminFeePercent = adminFeePercent;
            AdminFeeFlat = adminFeeFlat;
            AdminFeeAppliesBeforeTaxes = appliesBeforeTaxes;
            RecalculateTotals();
        }

        public void SetOwnershipSnapshot(OwnershipType type, Guid? vehicleOwnershipId = null)
        {
            EnsureMutable();
            OwnershipTypeAtBatch = type;
            VehicleOwnershipId = vehicleOwnershipId;
            RecalculateTotals();
        }

        public void SetTaxProfile(TaxProfile profile)
        {
            EnsureMutable();
            TaxProfile = profile ?? throw new ArgumentNullException(nameof(profile));
            RecalculateTotals();
        }

        public void AddAdjustment(decimal amount) // can be negative (e.g., -20 for truckwash)
        {
            EnsureMutable();
            AdjustmentsTotal = Round2(AdjustmentsTotal + amount);
            RecalculateTotals();
        }

        public void SetStatus(BatchStatus status)
        {
            Status = status;
        }

        // Calculations (uniform rounding)
        public void RecalculateTotals()
        {
            TripTotal = Round2(_loads.Sum(l => l.CalculateNetPay()));
            WaitingRawTotal = Round2(_waits.Sum(w => w.CalculateRawPay()));
            WaitingTotal = Round2(WaitingRawTotal * WaitingPayoutPercentage);
            HourlyTotal = Round2(_hourlies.Sum(h => h.CalculatePay()));

            // If your Excel Gross includes dray, keep DrayTotal here; if not, set dray values to 0.
            GrossRevenue = Round2(TripTotal + WaitingTotal + HourlyTotal + DrayTotal);

            // Commission/share based on ownership snapshot
            decimal baseBeforeAdmin = OwnershipTypeAtBatch == OwnershipType.OwnedByDriverCompany
                ? GrossRevenue // owner-operator: full gross
                : Round2(GrossRevenue * DriverSharePercentage); // company driver: e.g., 40%

            // Admin fee (owner-operator). If applies before taxes, subtract from base, else after taxes.
            var adminFeeAmount = Round2(baseBeforeAdmin * AdminFeePercent) + AdminFeeFlat;
            decimal taxableBase;

            if (AdminFeeAppliesBeforeTaxes)
            {
                var afterAdmin = Round2(baseBeforeAdmin - adminFeeAmount);
                DriverShareAmount = afterAdmin;
                taxableBase = DriverShareAmount;
            }
            else
            {
                DriverShareAmount = baseBeforeAdmin;
                taxableBase = DriverShareAmount;
            }

            // Taxes on driver share (Quebec PDFs show GST/QST applied on the commission base)
            Taxes = TaxAmounts.Calculate(taxableBase, TaxProfile, 2, MidpointRounding.AwayFromZero);

            // NetPay = share + taxes - admin(after-taxes if configured) + adjustments
            var net = DriverShareAmount + Taxes.TotalTaxes;

            if (!AdminFeeAppliesBeforeTaxes)
                net = Round2(net - adminFeeAmount);

            net = Round2(net + AdjustmentsTotal);

            NetPay = net;

            // Mirror for template
            WaitPay = WaitingTotal;
        }

        private void EnsureMutable()
        {
            if (Status != BatchStatus.Draft)
                throw new InvalidOperationException("Cannot modify batch unless status is Draft.");
        }

        private static bool IsFraction(decimal v) => v >= 0m && v <= 1m;
        private static decimal Round2(decimal value) => Math.Round(value, 2, MidpointRounding.AwayFromZero);
    }
}
