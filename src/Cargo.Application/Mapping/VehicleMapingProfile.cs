using AutoMapper;
using Cargo.Application.DTOs.Common;
using Cargo.Application.DTOs.Vehicles;
using Cargo.Domain.Entities;
using Cargo.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Application.Mapping
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {

            CreateMap<Vehicle, BaseEntityDto>()
                .ForMember(d => d.CreatedByIP, o => o.Ignore())
                .ForMember(d => d.UpdatedByIP, o => o.Ignore())
                .ForMember(d => d.CreatedBy, o => o.Ignore())
                .ForMember(d => d.UpdatedBy, o => o.Ignore());

            // PlateNumberDto ↔ PlateNumber
            CreateMap<PlateNumberDto, PlateNumber>()
                .ConstructUsing(src =>
                    src == null
                        ? null
                        : new PlateNumber(
                            src.Value ?? string.Empty,
                            src.IssuingAuthority ?? string.Empty,
                            string.IsNullOrWhiteSpace(src.PlateType) ? "Standard" : src.PlateType
                          ));

            CreateMap<PlateNumber, PlateNumberDto>()
                .ForMember(d => d.Value, m => m.MapFrom(s => s.Value))
                .ForMember(d => d.IssuingAuthority, m => m.MapFrom(s => s.IssuingAuthority))
                .ForMember(d => d.PlateType, m => m.MapFrom(s => s.PlateType));


            // Entity → VehicleDto
            CreateMap<Vehicle, VehicleDto>()
               .IncludeBase<Vehicle, BaseEntityDto>()
               .ForMember(d => d.PlateNumber, m => m.MapFrom(s => s.PlateNumber));



            // CreateDto → Entity
            CreateMap<VehicleCreateDto, Vehicle>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.UpdatedAt, o => o.Ignore())
                .ForMember(d => d.Status, o => o.Ignore())        // پیش‌فرض Domain
                .ForMember(d => d.IsAvailable, o => o.Ignore())   // پیش‌فرض Domain
                .ForMember(d => d.Mileage, o => o.Ignore())
                .ForMember(d => d.CurrentLocation, o => o.Ignore()) // ← اضافه شد
                .ForMember(d => d.DriverId, o => o.Ignore())        // ← اضافه شد
                .ForMember(d => d.CreatedByIP, o => o.Ignore())     // ← اضافه شد
                .ForMember(d => d.UpdatedByIP, o => o.Ignore())     // ← اضافه شد
                .ForMember(d => d.CreatedBy, o => o.Ignore())       // ← اضافه شد
                .ForMember(d => d.UpdatedBy, o => o.Ignore())       // ← اضافه شد
                .ForMember(d => d.PlateNumber, m => m.MapFrom(s => s.PlateNumber))
                .ForMember(d => d.OwnerCompany, o => o.Ignore())
                 .AfterMap((src, dest) =>
                 {
                     dest.CreatedAt = DateTime.UtcNow;
                     dest.CreatedBy = "System";
                     dest.CreatedByIP = "System";
                     dest.UpdateLocation("");
                 });


            // UpdateDto → Entity
            CreateMap<VehicleUpdateDto, Vehicle>()
     .ForMember(d => d.Id, o => o.Ignore()) 
     .ForMember(d => d.CreatedAt, o => o.Ignore())
     .ForMember(d => d.UpdatedAt, o => o.Ignore())
     .ForMember(d => d.Status, o => o.Ignore())          // ← اضافه شد
     .ForMember(d => d.Mileage, o => o.Ignore())         // ← اضافه شد
     .ForMember(d => d.OwnerCompanyId, o => o.Ignore())  // ← اضافه شد
     .ForMember(d => d.CreatedByIP, o => o.Ignore())     // ← اضافه شد
     .ForMember(d => d.UpdatedByIP, o => o.Ignore())     // ← اضافه شد
     .ForMember(d => d.CreatedBy, o => o.Ignore())       // ← اضافه شد
     .ForMember(d => d.UpdatedBy, o => o.Ignore())       // ← اضافه شد
     .ForMember(d => d.PlateNumber, m => m.MapFrom(s => s.PlateNumber))
     .ForMember(d => d.OwnerCompany, o => o.Ignore())
      .AfterMap((src, dest) =>
      {
          dest.UpdatedAt = DateTime.UtcNow;
          dest.UpdatedBy = "System";
          dest.UpdateLocation("");
      });



        }
    }
}
