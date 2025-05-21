using Ali.Delivery.Location.Application.Dtos;
using Ali.Delivery.Location.Infrastructure.ExternalServices.Models;
using MediatR;
using Refit;

namespace Ali.Delivery.Location.Infrastructure.ExternalServices;

public interface IFileServiceForLocation
{
    [Post("/api/v1/userlocation/create-location")]
    Task<ApiResponse<Unit>> CreateUserLocationAsync([Body] LocationDto dto);
        
    [Put("/api/v1/userlocation/update-location")]
    Task<ApiResponse<Unit>> UpdateUserLocationAsync([Body] LocationDto dto);
}

