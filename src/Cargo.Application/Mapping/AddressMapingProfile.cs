using AutoMapper;
using Cargo.Application.DTOs.Common;
using Cargo.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Application.Mapping
{
    public class AddressMapingProfile : Profile
    {
        public AddressMapingProfile()
        {
            CreateMap<Address, AddressDto>();
            CreateMap<AddressDto, Address>().ReverseMap();

        }
    }
}
