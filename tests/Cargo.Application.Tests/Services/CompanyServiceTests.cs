using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cargo.Application.DTOs.Company;
using Cargo.Application.Services;
using Cargo.Domain.Entities;
using Cargo.Domain.Interfaces;
using Moq;
using Xunit;
using FluentAssertions;

namespace Cargo.Application.Tests.Services
{

    public class CompanyServiceTests
    {
        // Mocked dependencies
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ICompanyRepository> _companyRepoMock;
        private readonly Mock<IMapper> _mapperMock;

        // System Under Test
        private readonly CompanyService _sut;

        public CompanyServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _companyRepoMock = new Mock<ICompanyRepository>();
            _mapperMock = new Mock<IMapper>();

            // Set up UnitOfWork to return our mock repository
            _unitOfWorkMock.Setup(u => u.Companies).Returns(_companyRepoMock.Object);

            _sut = new CompanyService(_unitOfWorkMock.Object, _mapperMock.Object);
        }



        [Fact]
        public async Task GetCompanyByIdAsync_ShouldReturnMappedDto_WhenCompanyExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity = new Company { Id = id, Name = "Acme" };
            var dto = new CompanyDto { Id = id, Name = "Acme" };

            _companyRepoMock
                .Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);

            _mapperMock.Setup(m => m.Map<CompanyDto>(entity)).Returns(dto);

            // Act
            var result = await _sut.GetCompanyByIdAsync(id);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(id);
        }


        [Fact]
        public async Task GetCompanyByIdAsync_ShouldReturnNull_WhenCompanyNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            _companyRepoMock
                .Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Company)null);

            // Act
            var result = await _sut.GetCompanyByIdAsync(id);

            // Assert
            result.Should().BeNull();
        }



        [Fact]
        public async Task GetAllCompaniesAsync_ShouldReturnMappedDtos()
        {
            // Arrange
            var entities = new List<Company>
            {
                new Company { Id = Guid.NewGuid(), Name = "A" },
                new Company { Id = Guid.NewGuid(), Name = "B" }
            };
            var dtos = new List<CompanyDto>
            {
                new CompanyDto { Id = entities[0].Id, Name = "A" },
                new CompanyDto { Id = entities[1].Id, Name = "B" }
            };

            _companyRepoMock
                .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(entities);

            _mapperMock
                .Setup(m => m.Map<IEnumerable<CompanyDto>>(entities))
                .Returns(dtos);

            // Act
            var result = await _sut.GetAllCompaniesAsync();

            // Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task CreateCompanyAsync_ShouldAddEntity_AndReturnMappedDto()
        {
            // Arrange
            var createDto = new CompanyCreateDto { Name = "NewCo" };
            var entity = new Company { Id = Guid.NewGuid(), Name = "NewCo" };
            var resultDto = new CompanyDto { Id = entity.Id, Name = entity.Name };

            _mapperMock.Setup(m => m.Map<Company>(createDto)).Returns(entity);
            _mapperMock.Setup(m => m.Map<CompanyDto>(entity)).Returns(resultDto);

            _companyRepoMock
                .Setup(r => r.AddAsync(entity, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _unitOfWorkMock
                .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var result = await _sut.CreateCompanyAsync(createDto);

            // Assert
            result.Should().NotBeNull();
            result!.Name.Should().Be("NewCo");
            _companyRepoMock.Verify(r => r.AddAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }


        [Fact]
        public async Task UpdateCompanyAsync_ShouldUpdateEntity_WhenCompanyExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity = new Company { Id = id, Name = "OldName" };
            var updateDto = new CompanyUpdateDto { Name = "NewName" };
            var resultDto = new CompanyDto { Id = id, Name = "NewName" };

            _companyRepoMock
                .Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);

            _mapperMock.Setup(m => m.Map(updateDto, entity)).Returns(entity);
            _companyRepoMock.Setup(r => r.UpdateAsync(entity, It.IsAny<CancellationToken>()))
                            .Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            _mapperMock.Setup(m => m.Map<CompanyDto>(entity)).Returns(resultDto);

            // Act
            var result = await _sut.UpdateCompanyAsync(id, updateDto);

            // Assert
            result.Should().NotBeNull();
            result!.Name.Should().Be("NewName");
        }

        [Fact]
        public async Task UpdateCompanyAsync_ShouldThrow_WhenCompanyNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            _companyRepoMock
                .Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Company)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(
                () => _sut.UpdateCompanyAsync(id, new CompanyUpdateDto())
            );
        }


        [Fact]
        public async Task DeleteCompanyAsync_ShouldReturnTrue_WhenCompanyExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity = new Company { Id = id };

            _companyRepoMock
                .Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);

            _companyRepoMock
                .Setup(r => r.RemoveAsync(entity, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _unitOfWorkMock
                .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var result = await _sut.DeleteCompanyAsync(id);

            // Assert
            result.Should().BeTrue();
        }


        [Fact]
        public async Task DeleteCompanyAsync_ShouldReturnFalse_WhenCompanyNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            _companyRepoMock
                .Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Company)null);

            // Act
            var result = await _sut.DeleteCompanyAsync(id);

            // Assert
            result.Should().BeFalse();
        }



    }
}
