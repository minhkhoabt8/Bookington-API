using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.Court;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Account
            CreateMap<Account, AccountReadDTO>();
            CreateMap<AccountWriteDTO, Account>();
            // Court
            CreateMap<Court, CourtReadDTO>();
            CreateMap<CourtWriteDTO, Court>();
        }
    }
}
