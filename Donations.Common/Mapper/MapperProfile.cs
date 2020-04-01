using AutoMapper;
using Donations.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Donations.Common.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Address, Data.Models.Address>()
                .ReverseMap();
        }
    }
}
