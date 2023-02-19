using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.Role;
using Bookington.Infrastructure.DTOs.Voucher;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Bookington.Infrastructure.Services.Implementations
{
    public class VoucherService : IVoucherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        

        public VoucherService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<VoucherReadDTO> CreateAsync(VoucherWriteDTO dto)
        {
            var existVoucher = await _unitOfWork.VoucherRepository.FindAsync(dto.VoucherCode);
            
            if(existVoucher != null) throw new UniqueConstraintException<Voucher>(nameof(Voucher.VoucherCode), dto.VoucherCode);

            var voucher= _mapper.Map<Voucher>(dto);

            await _unitOfWork.VoucherRepository.AddAsync(voucher);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<VoucherReadDTO>(voucher);
           
        }

        public async Task DeleteAsync(string id)
        {
            var existVoucher = await _unitOfWork.VoucherRepository.FindAsync(id);

            if (existVoucher == null) throw new EntityWithIDNotFoundException<Role>(id);

            _unitOfWork.VoucherRepository.Delete(existVoucher);

            await _unitOfWork.CommitAsync();
        }


        public async Task<IEnumerable<VoucherReadDTO>> GetAllVoucherOfACourtAsync(string courtId)
        {
            var vouchers = await _unitOfWork.VoucherRepository.GetAllVoucherOfACourtAsync(courtId);

            if (vouchers == null) throw new EntityWithIDNotFoundException<Voucher>(courtId);

            return _mapper.Map<IEnumerable<VoucherReadDTO>>(vouchers);
        }


        public async Task<VoucherReadDTO> UpdateAsync(string id, VoucherWriteDTO dto)
        {
            var existVoucher = await _unitOfWork.VoucherRepository.FindAsync(id);

            if (existVoucher == null) throw new EntityWithIDNotFoundException<Voucher>(id);

            _mapper.Map(dto, existVoucher);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<VoucherReadDTO>(existVoucher);
        }
    }
}
