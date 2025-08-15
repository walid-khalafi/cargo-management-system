using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Cargo.Domain.Entities;
using Cargo.Domain.Enums;
using Cargo.Domain.Interfaces;
using Cargo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cargo.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for DriverBatchLoad entity with specific operations.
    /// </summary>
    public class DriverBatchLoadRepository : GenericRepository<DriverBatchLoad>, IDriverBatchLoadRepository
    {
        private readonly CargoDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DriverBatchLoadRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public DriverBatchLoadRepository(CargoDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<DriverBatchLoad>> GetByDriverBatchIdAsync(Guid driverBatchId, CancellationToken ct = default)
        {
            return await _context.DriverBatchLoads
                .Where(dbl => dbl.DriverBatchId == driverBatchId)
                .OrderBy(dbl => dbl.LoadNumber)
                .ToListAsync(ct);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<DriverBatchLoad>> GetByDriverIdAsync(Guid driverId, CancellationToken ct = default)
        {
            return await _context.DriverBatchLoads
                .Include(dbl => dbl.DriverBatch)
                .Where(dbl => dbl.DriverBatch.DriverId == driverId)
                .OrderByDescending(dbl => dbl.CreatedAt)
                .ToListAsync(ct);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<DriverBatchLoad>> GetByLoadTypeAsync(LoadType loadType, CancellationToken ct = default)
        {
            return await _context.DriverBatchLoads
                .Where(dbl => dbl.LoadType == loadType)
                .OrderByDescending(dbl => dbl.CreatedAt)
                .ToListAsync(ct);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<DriverBatchLoad>> GetByRateTypeAsync(RateType rateType, CancellationToken ct = default)
        {
            return await _context.DriverBatchLoads
                .Where(dbl => dbl.RateType == rateType)
                .OrderByDescending(dbl => dbl.CreatedAt)
                .ToListAsync(ct);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<DriverBatchLoad>> GetByDarNumberAsync(string darNumber, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(darNumber))
                return new List<DriverBatchLoad>();

            return await _context.DriverBatchLoads
                .Where(dbl => dbl.DarNumber == darNumber)
                .OrderByDescending(dbl => dbl.CreatedAt)
                .ToListAsync(ct);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<DriverBatchLoad>> GetByLoadNumberAsync(string loadNumber, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(loadNumber))
                return new List<DriverBatchLoad>();

            return await _context.DriverBatchLoads
                .Where(dbl => dbl.LoadNumber == loadNumber)
                .OrderByDescending(dbl => dbl.CreatedAt)
                .ToListAsync(ct);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<DriverBatchLoad>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken ct = default)
        {
            return await _context.DriverBatchLoads
                .Include(dbl => dbl.DriverBatch)
                .Where(dbl => dbl.DriverBatch.CreatedAt >= startDate && dbl.DriverBatch.CreatedAt <= endDate)
                .OrderByDescending(dbl => dbl.CreatedAt)
                .ToListAsync(ct);
        }

        /// <inheritdoc />
        public async Task<decimal> GetTotalPayByDriverBatchIdAsync(Guid driverBatchId, CancellationToken ct = default)
        {
            return await _context.DriverBatchLoads
                .Where(dbl => dbl.DriverBatchId == driverBatchId)
                .SumAsync(dbl => dbl.NetWefp, ct);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<DriverBatchLoad>> GetPagedAsync(int pageIndex, int pageSize, CancellationToken ct = default)
        {
            if (pageIndex < 0) pageIndex = 0;
            if (pageSize <= 0) pageSize = 10;

            return await _context.DriverBatchLoads
                .Include(dbl => dbl.DriverBatch)
                .OrderByDescending(dbl => dbl.CreatedAt)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);
        }
    }
}
