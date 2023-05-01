using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.Ban;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Services.Implementations
{
    public class BanService : IBanServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public BanService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            
        }

        public async Task<BanReadDTO?> FindCourtBanByCourtIdAsync(string courtId)
        {
            var courtBan = await _unitOfWork.BanRepository.FindCourtBanByCourtIdAsync(courtId);
            return _mapper.Map<BanReadDTO>(courtBan);   
        }

        public async Task<BanReadDTO?> FindUserBanByUserIdAsync(string userId)
        {
            var userBan = await _unitOfWork.BanRepository.FindUserBanByUserIdAsync(userId);
            return _mapper.Map<BanReadDTO>(userBan);
        }

        public async Task UnBanCourtCronJobAsync()
        {
            //Get All Ref-Court from ban table that have ban until < datetime.now
            var expiredBans = await _unitOfWork.BanRepository.GetAllExpiredCourtBanAsync();

            //foreach ban court get current Court
            foreach(var item in expiredBans)
            {
                var court = await _unitOfWork.CourtRepository.FindAsync(item.RefCourt);
                if(court != null)
                {
                    //Update IsActive of court to true
                    court.IsActive = true;
                    _unitOfWork.CourtRepository.Update(court);
                }
                //Update IsActive of Ban to false

                item.IsActive = false;
                _unitOfWork.BanRepository.Update(item);
            }
            //Save ChangeDB
            await _unitOfWork.CommitAsync();    
        }

        public async Task UnBanUserCronJobAsync()
        {
            //Get All Ref-Account from ban table that have ban until < datetime.now
            var expiredBans = await _unitOfWork.BanRepository.GetAllExpiredUserBanAsync();

            //foreach ban Account get current Account
            foreach (var item in expiredBans)
            {
                var account = await _unitOfWork.AccountRepository.FindAsync(item.RefAccount);
                if (account != null)
                {
                    //Update IsActive of account to true
                    account.IsActive = true;
                    _unitOfWork.AccountRepository.Update(account);
                }
                //Update IsActive of Ban to false

                item.IsActive = false;
                _unitOfWork.BanRepository.Update(item);
            }
            //Save ChangeDB
            await _unitOfWork.CommitAsync();
        }
    }
}
