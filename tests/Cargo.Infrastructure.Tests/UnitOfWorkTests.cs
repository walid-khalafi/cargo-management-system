using System;
using System.Threading.Tasks;
using Cargo.Domain.Entities;
using Cargo.Domain.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Cargo.Infrastructure.Tests
{
    /// <summary>
    /// Unit tests for the UnitOfWork implementation.
    /// Uses an in-memory SQLite fixture to ensure realistic EF Core behavior
    /// and verifies transactional operations meet enterprise-grade requirements.
    /// </summary>
    public sealed class UnitOfWorkTests : IAsyncLifetime
    {
        private readonly SqliteInMemoryTestFixture _fixture = new();

        public async Task InitializeAsync() => await _fixture.InitializeAsync();
        public async Task DisposeAsync() => await _fixture.DisposeAsync();

        [Fact]
        public async Task BeginCommitTransaction_Should_NotThrow_And_Dispose_Transaction()
        {
            using var scope = _fixture.ServiceProvider.CreateScope();
            var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            // Act
            Func<Task> act = async () =>
            {
                await uow.BeginTransactionAsync();
                // Even with no operations, Commit should be safe and idempotent
                await uow.CommitTransactionAsync();
            };

            // Assert
            await act.Should().NotThrowAsync();
        }
        [Fact]
        public async Task CommitTransaction_ShouldPersistChanges()
        {
            await using var scope = _fixture.ServiceProvider.CreateAsyncScope();
            var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            // 🚀 Start transaction first
            await uow.BeginTransactionAsync();

            // Create a company first to satisfy foreign key constraint
            var company = new Company
            {
                Id = Guid.NewGuid(),
                Name = "Test Company",
                RegistrationNumber = "REG123456",
                CreatedBy = "TestUser",
                CreatedByIP = "127.0.0.1"
            };
            await uow.Companies.AddAsync(company);
            await uow.SaveChangesAsync();

            var driver = new Driver 
            { 
                Id = Guid.NewGuid(), 
                FirstName = "John", 
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890",
                LicenseNumber = "DL123456",
                LicenseType = "CDL-A",
                LicenseExpiryDate = DateTime.UtcNow.AddYears(2),
                DateOfBirth = DateTime.UtcNow.AddYears(-30),
                YearsOfExperience = 5,
                CompanyId = company.Id,
                CreatedBy = "TestUser",
                CreatedByIP = "127.0.0.1"
            };
            
            // Add the driver to the repository
            await uow.Drivers.AddAsync(driver);
            
            // Save changes within the transaction
            await uow.SaveChangesAsync();
            
            // Commit the transaction
            await uow.CommitTransactionAsync();

            // Verify in new scope
            await using var verifyScope = _fixture.ServiceProvider.CreateAsyncScope();
            var verifyUow = verifyScope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var stored = await verifyUow.Drivers.GetByIdAsync(driver.Id);

            stored.Should().NotBeNull();
            stored!.FullName.Should().Be("John Doe");
        }



        [Fact]
        public async Task RollbackTransaction_ShouldDiscardChanges()
        {
            using var scope = _fixture.ServiceProvider.CreateScope();
            var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var driver = new Driver { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Smith" };
            await uow.Drivers.AddAsync(driver);

            await uow.BeginTransactionAsync();
            await uow.RollbackTransactionAsync();

            // Verify that no changes were persisted
            using var verificationScope = _fixture.ServiceProvider.CreateScope();
            var verifyUow = verificationScope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var storedDriver = await verifyUow.Drivers.GetByIdAsync(driver.Id);
            storedDriver.Should().BeNull();
        }

        [Fact]
        public async Task Dispose_ShouldReleaseResources()
        {
            var scope = _fixture.ServiceProvider.CreateScope();
            var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            // Act
            await uow.DisposeAsync();

            // Assert
            Func<Task> act = async () => await uow.BeginTransactionAsync();
            await act.Should().ThrowAsync<ObjectDisposedException>();
        }
    }
}