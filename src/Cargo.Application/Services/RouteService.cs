using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cargo.Application.DTOs.Routes;
using Cargo.Application.Interfaces;
using Cargo.Domain.Entities;
using Cargo.Domain.Enums;
using Cargo.Domain.Interfaces;
using AutoMapper;

namespace Cargo.Application.Services
{
    /// <summary>
    /// Service implementation for route management and optimization using UnitOfWork pattern
    /// </summary>
    public class RouteService : IRouteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RouteService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<RouteDto> GetRouteByIdAsync(Guid id)
        {
            var route = await _unitOfWork.Routes.GetByIdAsync(id);
            return _mapper.Map<RouteDto>(route);
        }

        public async Task<IEnumerable<RouteDto>> GetAllRoutesAsync()
        {
            var routes = await _unitOfWork.Routes.GetAllAsync();
            return _mapper.Map<IEnumerable<RouteDto>>(routes);
        }

        public async Task<RouteDto> CreateRouteAsync(RouteCreateDto dto)
        {
            var route = _mapper.Map<Route>(dto);
            await _unitOfWork.Routes.AddAsync(route);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<RouteDto>(route);
        }

        public async Task<RouteDto> UpdateRouteAsync(Guid id, RouteUpdateDto dto)
        {
            var route = await _unitOfWork.Routes.GetByIdAsync(id);
            if (route == null)
                throw new KeyNotFoundException($"Route with ID {id} not found");

            _mapper.Map(dto, route);
            await _unitOfWork.Routes.UpdateAsync(route);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<RouteDto>(route);
        }

        public async Task<bool> DeleteRouteAsync(Guid id)
        {
            var route = await _unitOfWork.Routes.GetByIdAsync(id);
            if (route == null)
                return false;

            await _unitOfWork.Routes.RemoveAsync(route);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<RouteDto>> GetRoutesByStatusAsync(RouteStatus status)
        {
            var routes = await _unitOfWork.Routes.FindAsync(r => r.Status == status);
            return _mapper.Map<IEnumerable<RouteDto>>(routes);
        }

        public async Task<IEnumerable<RouteDto>> GetRoutesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var routes = await _unitOfWork.Routes.FindAsync(r => 
                r.CreatedAt >= startDate && r.CreatedAt <= endDate);
            return _mapper.Map<IEnumerable<RouteDto>>(routes);
        }

        public async Task<IEnumerable<RouteDto>> GetActiveRoutesAsync()
        {
            var routes = await _unitOfWork.Routes.FindAsync(r => r.Status == RouteStatus.Active);
            return _mapper.Map<IEnumerable<RouteDto>>(routes);
        }

        public async Task<IEnumerable<RouteDto>> GetCompletedRoutesAsync()
        {
            // Since RouteStatus only has Active, we'll use Active as completed
            var routes = await _unitOfWork.Routes.FindAsync(r => r.Status == RouteStatus.Active);
            return _mapper.Map<IEnumerable<RouteDto>>(routes);
        }

        public async Task<IEnumerable<RouteDto>> GetRoutesByDriverAsync(Guid driverId)
        {
            // This would need to be implemented via driver-route relationships
            // For now, return empty list as this requires additional repository methods
            return new List<RouteDto>();
        }

        public async Task<IEnumerable<RouteDto>> GetRoutesByVehicleAsync(Guid vehicleId)
        {
            // This would need to be implemented via vehicle-route relationships
            // For now, return empty list as this requires additional repository methods
            return new List<RouteDto>();
        }

        public async Task<decimal> CalculateRouteCostAsync(RouteDto route)
        {
            return route.EstimatedFuelCost + route.EstimatedTollCost;
        }

        public async Task<RouteDto> OptimizeRouteAsync(RouteDto route)
        {
            // Placeholder for route optimization logic
            return route;
        }
    }
}
