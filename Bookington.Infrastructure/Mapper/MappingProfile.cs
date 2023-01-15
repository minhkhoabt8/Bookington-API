using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.Court;
using Bookington.Infrastructure.DTOs.Role;
using Bookington.Infrastructure.DTOs.Booking;
using Bookington.Infrastructure.DTOs.SubCourt;
using Bookington.Infrastructure.DTOs.Slot;
using Bookington.Infrastructure.DTOs.Voucher;
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
            CreateMap<CourtWriteDTO, Court>()
                .ForMember(x => x.OpenAt,opt => opt.MapFrom(src => (src.OpenAt).Hours + src.OpenAt.Minutes))
                .ForMember(x => x.CloseAt, opt => opt.MapFrom(src => (src.CloseAt).Hours + src.OpenAt.Minutes));
            // Sub Court
            CreateMap<SubCourt, SubCourtReadDTO>();
            CreateMap<SubCourtWriteDTO, SubCourt>();
            // Role
            CreateMap<Role, RoleReadDTO>();
            CreateMap<RoleWriteDTO, Role>();
            // Booking
            CreateMap<Booking, BookingReadDTO>();
            CreateMap<BookingWriteDTO, Booking>();
            CreateMap<Booking, CourtBookingHistoryReadDTO>()
            .ForMember(des => des.TimeSlot, act => act.MapFrom(src => src.RefSlotNavigation.StartTime.ToString() + " - " + src.RefSlotNavigation.EndTime.ToString()))
            .ForMember(des => des.Customer, act => act.MapFrom(src => src.BookByNavigation.FullName))
            .ForMember(des => des.Phone, act => act.MapFrom(src => src.BookByNavigation.Phone))
            .ForMember(des => des.VoucherDiscount, act => act.MapFrom(src => src.VoucherCodeNavigation.Discount));
            // Slot
            CreateMap<Slot, SlotReadDTO>();
            CreateMap<SlotWriteDTO, Slot>();
            // Voucher
            CreateMap<Voucher, VoucherReadDTO>();
            CreateMap<VoucherWriteDTO, Voucher>();
        }
    }
}
