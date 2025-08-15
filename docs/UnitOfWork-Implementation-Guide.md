# Unit of Work Implementation Guide

## Overview

The Unit of Work pattern has been successfully implemented in the Cargo Management System to provide transaction management across multiple repositories. This pattern ensures atomic operations and maintains data consistency.

## Architecture

### Components

1. **IUnitOfWork Interface** (`src/Cargo.Domain/Interfaces/IUnitOfWork.cs`)
   - Defines the contract for Unit of Work
   - Includes repository properties for all aggregate roots
   - Provides transaction management methods

2. **UnitOfWork Implementation** (`src/Cargo.Infrastructure/Repositories/UnitOfWork.cs`)
   - Concrete implementation of IUnitOfWork
   - Manages all repository instances
   - Handles database transactions

3. **Updated GenericRepository** (`src/Cargo.Infrastructure/Repositories/GenericRepository.cs`)
   - Now implements `IGenericRepository<T>` from Domain layer
   - Removed SaveChangesAsync method (handled by UnitOfWork)

## Usage

### Basic Usage

```csharp
public class MyService
{
    private readonly IUnitOfWork _unitOfWork;

    public MyService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateDriverAsync(string firstName, string lastName)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var driver = new Driver
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                // ... other properties
            };

            await _unitOfWork.Drivers.AddAsync(driver);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}
```

### Complex Transaction Example

```csharp
public async Task CreateDriverWithCompanyAsync(string firstName, string lastName, string companyName)
{
    try
    {
        await _unitOfWork.BeginTransactionAsync();

        // Create company
        var company = new Company { /* properties */ };
        await _unitOfWork.Companies.AddAsync(company);

        // Create driver
        var driver = new Driver 
        { 
            CompanyId = company.Id,
            /* other properties */ 
        };
        await _unitOfWork.Drivers.AddAsync(driver);

        // Save all changes atomically
        await _unitOfWork.SaveChangesAsync();
        await _unitOfWork.CommitAsync();
    }
    catch (Exception)
    {
        await _unitOfWork.RollbackAsync();
        throw;
    }
}
```

## Available Repositories

The UnitOfWork provides access to all entity repositories:

- **Companies**: `IGenericRepository<Company>`
- **Drivers**: `IGenericRepository<Driver>`
- **Vehicles**: `IGenericRepository<Vehicle>`
- **Routes**: `IGenericRepository<Route>`
- **DriverBatches**: `IGenericRepository<DriverBatch>`
- **DriverBatchHourlies**: `IGenericRepository<DriverBatchHourly>`
- **DriverBatchLoads**: `IGenericRepository<DriverBatchLoad>`
- **DriverBatchWaits**: `IGenericRepository<DriverBatchWait>`
- **DriverContracts**: `IGenericRepository<DriverContract>`
- **DriverVehicleAssignments**: `IGenericRepository<DriverVehicleAssignment>`
- **VehicleOwnerships**: `IGenericRepository<VehicleOwnership>`

## Transaction Management

### Methods

- `BeginTransactionAsync()`: Starts a new database transaction
- `CommitAsync()`: Commits the current transaction
- `RollbackAsync()`: Rolls back the current transaction
- `SaveChangesAsync()`: Saves all pending changes
- `HasChanges()`: Checks if there are pending changes

### Best Practices

1. **Always use transactions for multi-entity operations**
2. **Use try-catch blocks with proper rollback**
3. **Dispose UnitOfWork properly (handled by DI container)**
4. **Avoid mixing UnitOfWork with individual repository SaveChanges**

## Dependency Injection

The UnitOfWork is registered in the DI container as a scoped service:

```csharp
// In Program.cs
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
```

## Migration from Individual Repositories

### Before (Individual Repositories)
```csharp
// Each repository had its own SaveChangesAsync
await _driverRepository.AddAsync(driver);
await _driverRepository.SaveChangesAsync();

await _companyRepository.AddAsync(company);
await _companyRepository.SaveChangesAsync();
```

### After (UnitOfWork)
```csharp
// All changes are saved atomically
await _unitOfWork.Drivers.AddAsync(driver);
await _unitOfWork.Companies.AddAsync(company);
await _unitOfWork.SaveChangesAsync();
```

## Testing

The UnitOfWork pattern makes testing easier by allowing you to:

1. **Mock the entire UnitOfWork** for service tests
2. **Verify transaction boundaries** in integration tests
3. **Test rollback scenarios** by throwing exceptions

## Example Service

See `UnitOfWorkExampleService.cs` in the Application layer for complete usage examples.

## Benefits

1. **Atomic Operations**: Ensures all changes succeed or fail together
2. **Transaction Management**: Provides explicit control over transactions
3. **Consistency**: Maintains data integrity across multiple repositories
4. **Performance**: Reduces database round trips
5. **Testability**: Easier to mock and test
6. **Clean Architecture**: Follows SOLID principles and clean architecture

## Next Steps

1. Update existing services to use UnitOfWork instead of individual repositories
2. Review transaction boundaries in business logic
3. Add integration tests for transaction scenarios
4. Consider adding specific repository interfaces for complex queries
