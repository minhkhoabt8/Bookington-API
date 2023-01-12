using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.Court;
using Bookington.Infrastructure.DTOs.Role;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteAsync(int id)
        {
            var existRole = await _unitOfWork.RoleRepository.FindAsync(id);

            if (existRole == null) throw new EntityWithIDNotFoundException<Role>(id);

            _unitOfWork.RoleRepository.Delete(existRole);

            await _unitOfWork.CommitAsync();
        }

        public async Task<RoleReadDTO> CreateAsync(RoleWriteDTO dto)
        {
            var role = _mapper.Map<Role>(dto);

            await _unitOfWork.RoleRepository.AddAsync(role);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<RoleReadDTO>(role);
        }

        public async Task<IEnumerable<RoleReadDTO>> GetAllAsync()
        {
            var roles = await _unitOfWork.RoleRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<RoleReadDTO>>(roles);
        }

        public async Task<RoleReadDTO> GetByIdAsync(string id)
        {
            var existRole = await _unitOfWork.CourtRepository.FindAsync(id);

            if (existRole == null) throw new EntityWithIDNotFoundException<Role>(id);

            return _mapper.Map<RoleReadDTO>(existRole);
        }

        public async Task<RoleReadDTO> UpdateAsync(int id, RoleWriteDTO dto)
        {
            var existRole = await _unitOfWork.RoleRepository.FindAsync(id);

            if (existRole == null) throw new EntityWithIDNotFoundException<Court>(id);

            _mapper.Map(dto, existRole);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<RoleReadDTO>(existRole);
        }

        public async Task AssignRolesToAccount(Guid accountID, int[] roleIDs)
        {
            throw new NotImplementedException();
        }

    }
}
