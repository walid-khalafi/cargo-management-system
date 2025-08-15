using Microsoft.EntityFrameworkCore;
using Cargo.Domain.Entities;
using Cargo.Domain.Enums;
using Cargo.Infrastructure.Data;
using Cargo.Domain.Interfaces;

namespace Cargo.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for Driver entity with specific operations.
    /// </summary>
    public class DriverRepository : GenericRepository<Driver>, IDriverRepository
    {
        private readonly CargoDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DriverRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public DriverRepository(CargoDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<Driver>> GetByCompanyIdAsync(Guid companyId, CancellationToken ct = default)
        {
            return await _context.Drivers
                .Where(d => d.CompanyId == companyId)
                .ToListAsync(ct);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<Driver>> GetActiveDriversAsync(CancellationToken ct = default)
        {
            return await _context.Drivers
                .Where(d => d.Status == DriverStatus.Active)
                .ToListAsync(ct);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<Driver>> GetByStatusAsync(DriverStatus status, CancellationToken ct = default)
        {
            return await _context.Drivers
                .Where(d => d.Status == status)
                .ToListAsync(ct);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<DriverContract>> GetDriverContractsAsync(Guid driverId, CancellationToken ct = default)
        {
            return await _context.DriverContracts
                .Where(dc => dc.DriverId == driverId)
                .OrderByDescending(dc => dc.StartDate)
                .ToListAsync(ct);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<DriverVehicleAssignment>> GetDriverVehicleAssignmentsAsync(Guid driverId, CancellationToken ct = default)
        {
            return await _context.DriverVehicleAssignments
                .Where(dva => dva.DriverId == driverId)
                .OrderByDescending(dva => dva.AssignedAt)
                .ToListAsync(ct);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<DriverVehicleAssignment>> GetActiveDriverVehicleAssignmentsAsync(Guid driverId, CancellationToken ct = default)
        {
            return await _context.DriverVehicleAssignments
                .Where(dva => dva.DriverId == driverId && dva.Status == AssignmentStatus.Active)
                .OrderByDescending(dva => dva.AssignedAt)
                .ToListAsync(ct);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<DriverBatch>> GetDriverBatchesAsync(Guid driverId, CancellationToken ct = default)
        {
            return await _context.DriverBatches
                .Where(db => db.DriverId == driverId)
                .OrderByDescending(db => db.StatementStartDate)
                .ToListAsync(ct);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<DriverBatchHourly>> GetDriverBatchHourliesAsync(Guid driverId, CancellationToken ct = default)
        {
            return await _context.DriverBatchHourlies
                .Where(dbh => dbh.DriverBatch.DriverId == driverId)
                .OrderByDescending(dbh => dbh.CreatedAt)
                .ToListAsync(ct);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<DriverBatchLoad>> GetDriverBatchLoadsAsync(Guid driverId, CancellationToken ct = default)
        {
            return await _context.DriverBatchLoads
                .Where(dbl => dbl.DriverBatch.DriverId == driverId)
                .OrderByDescending(dbl => dbl.CreatedAt)
                .ToListAsync(ct);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<DriverBatchWait>> GetDriverBatchWaitsAsync(Guid driverId, CancellationToken ct = default)
        {
            return await _context.DriverBatchWaits
                .Where(dbw => dbw.DriverBatch.DriverId == driverId)
                .OrderByDescending(dbw => dbw.CreatedAt)
                .ToListAsync(ct);
        }
    }
}


