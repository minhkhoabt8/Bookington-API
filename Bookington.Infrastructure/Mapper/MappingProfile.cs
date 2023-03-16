using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.Court;
using Bookington.Infrastructure.DTOs.Role;
using Bookington.Infrastructure.DTOs.Booking;
using Bookington.Infrastructure.DTOs.SubCourt;
using Bookington.Infrastructure.DTOs.Slot;
using Bookington.Infrastructure.DTOs.Voucher;
using Bookington.Infrastructure.DTOs.Province;
using Bookington.Infrastructure.DTOs.District;
using Bookington.Infrastructure.DTOs.Report;
using Bookington.Infrastructure.DTOs.UserBalance;
using Bookington.Infrastructure.DTOs.TransactionHistory;
using Bookington.Infrastructure.DTOs.Order;
using Bookington.Infrastructure.DTOs.CheckOut;
using Bookington.Infrastructure.DTOs.IncomingMatch;
using Bookington.Infrastructure.DTOs.Notification;
using Bookington.Infrastructure.DTOs.ReportResponse;

namespace Bookington.Infrastructure.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Account
            CreateMap<Account, AccountReadDTO>();
            CreateMap<Account, AccountProfileReadDTO>();
            CreateMap<AccountWriteDTO, Account>();
            CreateMap<AccountUpdateDTO,Account>()
                .ForMember(dest => dest.DateOfBirth, options => options.MapFrom(src => DateTime.Parse(src.DateOfBirth.ToString())));            
            //Otp
            CreateMap<OtpDTO, AccountOtp>();

            // Court
            CreateMap<Court, CourtReadDTO>()
                .ForMember(dest => dest.DistrictName, options => options.MapFrom(src => src.District.DistrictName));
            CreateMap<CourtWriteDTO, Court>()
                .ForMember(dest => dest.OpenAt,options => options.MapFrom(src => TimeSpan.Parse(src.OpenAt.ToString())))
                .ForMember(dest => dest.CloseAt, options => options.MapFrom(src => TimeSpan.Parse(src.CloseAt.ToString())));
            CreateMap<Court, CourtQueryResponse>()
                .ForMember(dest => dest.DistrictName, options => options.MapFrom(src=>src.District.DistrictName))
                .ForMember(dest => dest.ProvinceName, options => options.MapFrom(src => src.District.Province.ProvinceName))
                .ForMember(dest => dest.NumberOfSubCourt, options => options.MapFrom(src => src.SubCourts.Count()))
                //.ForMember(dest => dest.RatingStar, options => options.MapFrom(src => src.Comments.Sum(a => a.Rating) / src.Comments.Where(c=>c.Id == src.Id).Count()))
                .ForMember(des => des.CourtPicture, act => act.MapFrom(src => src.CourtImages.Where(c=>c.Id==src.Id)));
            // Sub Court
            CreateMap<SubCourt, SubCourtReadDTO>();
            CreateMap<SubCourtWriteDTO, SubCourt>();
            CreateMap<SubCourt, SubCourtForBookingReadDTO>()
                .ForMember(dest => dest.IsAvailable, options => options.MapFrom(src => src.IsActive));

            // Role
            CreateMap<Role, RoleReadDTO>();
            CreateMap<RoleWriteDTO, Role>();
            // Booking
            CreateMap<Booking, BookingReadDTO>();
            CreateMap<BookingWriteDTO, Booking>();
            CreateMap<Booking, CourtBookingHistoryReadDTO>()
            .ForMember(des => des.TimeSlot, act => act.MapFrom(src => src.RefSlotNavigation.StartTime.ToString() + " - " + src.RefSlotNavigation.EndTime.ToString()))
            .ForMember(des => des.Customer, act => act.MapFrom(src => src.BookByNavigation.FullName))
            .ForMember(des => des.Phone, act => act.MapFrom(src => src.BookByNavigation.Phone));    
            CreateMap<Booking, IncomingMatchReadDTO>()
                .ForMember(des => des.SubCourtName, act => act.MapFrom(src => src.RefSlotNavigation.RefSubCourtNavigation.Name))
                .ForMember(des => des.CourtName, act => act.MapFrom(src => src.RefSlotNavigation.RefSubCourtNavigation.ParentCourt.Name))
                .ForMember(des => des.PlayDate, act => act.MapFrom(src => src.PlayDate.ToString()));
            // Slot
            CreateMap<Slot, SlotReadDTO>();
            CreateMap<Slot, SlotForBookingReadDTO>()
                .ForMember(dest => dest.IsAvailable, options => options.MapFrom(src => src.IsActive));
            CreateMap<SlotWriteDTO, Slot>();
            // Voucher
            CreateMap<Voucher, VoucherReadDTO>();
            CreateMap<VoucherWriteDTO, Voucher>();
            //Province
            CreateMap<Province, ProvinceReadDTO>();
            CreateMap<ProvinceWriteDTO, Province>();
            //District
            CreateMap<District, DistrictReadDTO>();
            CreateMap<DistrictWriteDTO, District>();
            //Court Report
            CreateMap<CourtReport, CourtReportReadDTO>();
            CreateMap<CourtReportWriteDTO, CourtReport>();
            //Court Report Response
            CreateMap<CourtReportResponse, CourtReportResponseReadDTO>();
            CreateMap<CourtReportResponseWriteDTO, CourtReportResponse>();           
            //User Report
            CreateMap<UserReport, UserReportReadDTO>();
            CreateMap<UserReportCreateDTO, UserReport>();
            CreateMap<UserReportUpdateDTO, UserReport>();
            //User Report Response
            CreateMap<UserReportResponse, UserReportResponseReadDTO>();
            CreateMap<UserReportResponseWriteDTO, UserReportResponse>();
            //User Balance
            CreateMap<UserBalance, UserBalanceReadDTO>();
            CreateMap<UserBalanceWriteDTO, UserBalance>();
            //Transaction History
            CreateMap<TransactionHistory, TransactionHistoryReadDTO>();
            CreateMap<TransactionHistoryWriteDTO, TransactionHistory>();
            //Order
            CreateMap<Order, OrderReadDTO>();
            CreateMap<OrderWriteDTO, Order>();
            //Notification
            CreateMap<Notification, NotificationReadDTO>();                
            CreateMap<NotificationWriteDTO, Notification>();          
        }
    }
}
