using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cargo.Application.DTOs.Routes;
using Cargo.Domain.Enums;

namespace Cargo.Application.Interfaces
{
    /// <summary>
    /// Interface for route planning and optimization
    /// </summary>
    public interface IRouteService
{
    Task<RouteDto> GetRouteByIdAsync(Guid id);
    Task<IEnumerable<RouteDto>> GetAllRoutesAsync();
    Task<RouteDto> CreateRouteAsync(RouteCreateDto dto);
    Task<RouteDto> UpdateRouteAsync(Guid id, RouteUpdateDto dto);
    Task<bool> DeleteRouteAsync(Guid id);
    Task<IEnumerable<RouteDto>> GetRoutesByVehicleAsync(Guid vehicleId);
    Task<IEnumerable<RouteDto>> GetRoutesByDriverAsync(Guid driverId);
    Task<RouteDto> OptimizeRouteAsync(RouteDto route);
    Task<decimal> CalculateRouteCostAsync(RouteDto route);
    Task<IEnumerable<RouteDto>> GetRoutesByDateRangeAsync(DateTime startDate, DateTime endDate);
}
}
