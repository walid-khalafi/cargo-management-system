using System;
using System.Collections.Generic;
using System.Linq;
using Cargo.Domain.Enums;
using Cargo.Domain.ValueObjects;

namespace Cargo.Domain.Entities
{
    /// <summary>
    /// Represents a batch statement for a driver, containing payment and activity details.
    /// Acts as an aggregate root.
    /// </summary>
    public class DriverBatch : BaseEntity
    {
        private readonly List<DriverBatchLoad> _loads;
        private readonly List<DriverBatchWait> _waits;
        private readonly List<DriverBatchHourly> _hourlies;

        /// <summary>
        /// Gets the batch number identifier.
        /// </summary>
        public string BatchNumber { get; private set; }

        /// <summary>
        /// Gets the driver associated with this batch.
        /// </summary>
        public Driver Driver { get; private set; }

        /// <summary>
        /// Gets the foreign key for the associated driver.
        /// </summary>
        public Guid DriverId { get; private set; }

        /// <summary>
        /// Gets the start date of the statement period.
        /// </summary>
        public DateTime StatementStartDate { get; private set; }

        /// <summary>
        /// Gets the end date of the statement period.
        /// </summary>
        public DateTime StatementEndDate { get; private set; }

        /// <summary>
        /// Gets the dray pay amount.
        /// </summary>
        public decimal DrayPay { get; private set; }

        /// <summary>
        /// Gets the dray fuel surcharge amount.
        /// </summary>
        public decimal DrayFsc { get; private set; }

        /// <summary>
        /// Gets the temporary emergency fuel surcharge amount.
        /// </summary>
        public decimal TemporaryEmergencyFsc { get; private set; }

        /// <summary>
        /// Gets the wait pay amount.
        /// </summary>
        public decimal WaitPay { get; private set; }

        /// <summary>
        /// Gets the total pay amount.
        /// </summary>
        public decimal TotalPay => NetPay;

        /// <summary>
        /// Gets the collection of load details associated with this batch.
        /// </summary>
        public IReadOnlyCollection<DriverBatchLoad> Loads => _loads.AsReadOnly();

        /// <summary>
        /// Gets the collection of wait details associated with this batch.
        /// </summary>
        public IReadOnlyCollection<DriverBatchWait> Waits => _waits.AsReadOnly();

        /// <summary>
        /// Gets the collection of hourly details associated with this batch.
        /// </summary>
        public IReadOnlyCollection<DriverBatchHourly> Hourlies => _hourlies.AsReadOnly();

        /// <summary>
        /// Gets the total trip pay computed from loads.
        /// </summary>
        public decimal TripTotal { get; private set; }

        /// <summary>
        /// Gets the raw total waiting pay before payout percentage.
        /// </summary>
        public decimal WaitingRawTotal { get; private set; }

        /// <summary>
        /// Gets or sets the waiting payout percentage (e.g., 0.5m for 50%).
        /// </summary>
        public decimal WaitingPayoutPercentage { get; private set; }

        /// <summary>
        /// Gets the total waiting pay after applying payout percentage.
        /// </summary>
        public decimal WaitingTotal { get; private set; }

        /// <summary>
        /// Gets the total hourly pay.
        /// </summary>
        public decimal HourlyTotal { get; private set; }

        /// <summary>
        /// Gets the gross revenue (sum of trip, waiting, and hourly totals).
        /// </summary>
        public decimal GrossRevenue { get; private set; }

        /// <summary>
        /// Gets or sets the driver share percentage (e.g., 0.40m for 40%).
        /// </summary>
        public decimal DriverSharePercentage { get; private set; }

        /// <summary>
        /// Gets the driver share amount computed from gross revenue.
        /// </summary>
        public decimal DriverShareAmount { get; private set; }

        /// <summary>
        /// Gets the tax details (GST/QST) computed on driver share amount.
        /// </summary>
        public TaxDetails Taxes { get; private set; }

        /// <summary>
        /// Gets the net pay amount (driver share plus taxes).
        /// </summary>
        public decimal NetPay { get; private set; }

        /// <summary>
        /// Gets the current status of the batch.
        /// </summary>
        public BatchStatus Status { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DriverBatch"/> class.
        /// </summary>
        /// <param name="batchNumber">Batch number identifier.</param>
        /// <param name="driver">Associated driver.</param>
        /// <param name="statementStartDate">Statement start date.</param>
        /// <param name="statementEndDate">Statement end date.</param>
        /// <param name="waitingPayoutPercentage">Waiting payout percentage.</param>
        /// <param name="driverSharePercentage">Driver share percentage.</param>
        public DriverBatch(
            string batchNumber,
            Driver driver,
            DateTime statementStartDate,
            DateTime statementEndDate,
            decimal waitingPayoutPercentage,
            decimal driverSharePercentage)
        {
            if (string.IsNullOrWhiteSpace(batchNumber))
                throw new ArgumentException("Batch number cannot be empty.", nameof(batchNumber));

            if (statementStartDate > statementEndDate)
                throw new ArgumentException("Statement start date must be before or equal to end date.");

            BatchNumber = batchNumber;
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
            DriverId = driver.Id;
            StatementStartDate = statementStartDate;
            StatementEndDate = statementEndDate;
            WaitingPayoutPercentage = waitingPayoutPercentage;
            DriverSharePercentage = driverSharePercentage;

            _loads = new List<DriverBatchLoad>();
            _waits = new List<DriverBatchWait>();
            _hourlies = new List<DriverBatchHourly>();

            Status = BatchStatus.Draft;

            RecalculateTotals();
        }

        /// <summary>
        /// Adds a load entry to the batch.
        /// </summary>
        /// <param name="load">Load to add.</param>
        public void AddLoad(DriverBatchLoad load)
        {
            EnsureMutable();
            if (load == null) throw new ArgumentNullException(nameof(load));
            _loads.Add(load);
            RecalculateTotals();
        }

        /// <summary>
        /// Adds a wait entry to the batch.
        /// </summary>
        /// <param name="wait">Wait to add.</param>
        public void AddWait(DriverBatchWait wait)
        {
            EnsureMutable();
            if (wait == null) throw new ArgumentNullException(nameof(wait));
            _waits.Add(wait);
            RecalculateTotals();
        }

        /// <summary>
        /// Adds an hourly entry to the batch.
        /// </summary>
        /// <param name="hourly">Hourly entry to add.</param>
        public void AddHourly(DriverBatchHourly hourly)
        {
            EnsureMutable();
            if (hourly == null) throw new ArgumentNullException(nameof(hourly));
            _hourlies.Add(hourly);
            RecalculateTotals();
        }

        /// <summary>
        /// Recalculates all totals and computed properties.
        /// </summary>
        public void RecalculateTotals()
        {
            TripTotal = Math.Round(_loads.Sum(l => l.CalculateNetPay()), 2, MidpointRounding.AwayFromZero);
            WaitingRawTotal = Math.Round(_waits.Sum(w => w.CalculateRawPay()), 2, MidpointRounding.AwayFromZero);
            WaitingTotal = Math.Round(WaitingRawTotal * WaitingPayoutPercentage, 2, MidpointRounding.AwayFromZero);
            HourlyTotal = Math.Round(_hourlies.Sum(h => h.CalculatePay()), 2, MidpointRounding.AwayFromZero);
            GrossRevenue = Math.Round(TripTotal + WaitingTotal + HourlyTotal, 2, MidpointRounding.AwayFromZero);
            DriverShareAmount = Math.Round(GrossRevenue * DriverSharePercentage, 2, MidpointRounding.AwayFromZero);

            // Assuming GST 5% and QST 9.975% rates; these could be configurable
            Taxes = TaxDetails.Calculate(DriverShareAmount, 0.05m, 0.09975m);

            NetPay = DriverShareAmount + Taxes.TotalTaxes;
        }

        /// <summary>
        /// Sets the batch status.
        /// </summary>
        /// <param name="status">New status.</param>
        public void SetStatus(BatchStatus status)
        {
            Status = status;
        }

        private void EnsureMutable()
        {
            if (Status != BatchStatus.Draft)
                throw new InvalidOperationException("Cannot modify batch unless status is Draft.");
        }
    }
}
