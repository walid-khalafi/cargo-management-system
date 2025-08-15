using Cargo.Domain.Entities;
using Cargo.Domain.Enums;
using Cargo.Domain.Interfaces;
using Cargo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cargo.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for Vehicle entity with specific operations.
    /// </summary>
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        private readonly CargoDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public VehicleRepository(CargoDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<Vehicle>> GetByCompanyIdAsync(Guid companyId, CancellationToken ct = default)
        {
            return await _context.Vehicles
                .Where(v => v.OwnerCompanyId == companyId)
                .ToListAsync(ct);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<Vehicle>> GetActiveVehiclesAsync(CancellationToken ct = default)
        {
            return await _context.Vehicles
                .Where(v => v.Status == VehicleStatus.Available && v.IsAvailable)
                .ToListAsync(ct);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<Vehicle>> GetByStatusAsync(VehicleStatus status, CancellationToken ct = default)
        {
            return await _context.Vehicles
                .Where(v => v.Status == status)
                .ToListAsync(ct);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<Vehicle>> GetByFuelTypeAsync(string fuelType, CancellationToken ct = default)
        {
            return await _context.Vehicles
                .Where(v => v.FuelType == fuelType)
                .ToListAsync(ct);
        }

        /// <inheritdoc />
        public async Task<Vehicle?> GetByLicensePlateAsync(string licensePlate, CancellationToken ct = default)
        {
            return await _context.Vehicles
                .FirstOrDefaultAsync(v => v.PlateNumber.Value == licensePlate, ct);
        }

        #region VehicleOwnership Operations

        /// <inheritdoc />
        public async Task<IReadOnlyList<VehicleOwnership>> GetVehicleOwnershipsAsync(Guid vehicleId, CancellationToken ct = default)
        {
            return await _context.VehicleOwnerships
                .Where(vo => vo.VehicleId == vehicleId)
                .OrderByDescending(vo => vo.OwnedFrom)
                .ToListAsync(ct);
        }

        /// <inheritdoc />
        public async Task<VehicleOwnership?> GetCurrentVehicleOwnershipAsync(Guid vehicleId, CancellationToken ct = default)
        {
            return await _context.VehicleOwnerships
                .Where(vo => vo.VehicleId == vehicleId && vo.OwnedUntil == null)
                .OrderByDescending(vo => vo.OwnedFrom)
                .FirstOrDefaultAsync(ct);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<Vehicle>> GetVehiclesByCompanyOwnershipAsync(Guid companyId, CancellationToken ct = default)
        {
            return await _context.Vehicles
                .Where(v => _context.VehicleOwnerships
                    .Any(vo => vo.VehicleId == v.Id && vo.OwnerCompanyId == companyId && vo.OwnedUntil == null))
                .ToListAsync(ct);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<VehicleOwnership>> GetActiveVehicleOwnershipsByCompanyAsync(Guid companyId, CancellationToken ct = default)
        {
            return await _context.VehicleOwnerships
                .Where(vo => vo.OwnerCompanyId == companyId && vo.OwnedUntil == null)
                .OrderByDescending(vo => vo.OwnedFrom)
                .ToListAsync(ct);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<VehicleOwnership>> GetVehicleOwnershipsByTypeAsync(OwnershipType ownershipType, CancellationToken ct = default)
        {
            return await _context.VehicleOwnerships
                .Where(vo => vo.Type == ownershipType)
                .OrderByDescending(vo => vo.OwnedFrom)
                .ToListAsync(ct);
        }

        #endregion
    }
}