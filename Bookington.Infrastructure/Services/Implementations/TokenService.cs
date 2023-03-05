using Bookington.Core.Entities;
using Bookington.Infrastructure.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Bookington.Infrastructure.UOW;
using System.Security.Cryptography;

namespace Bookington.Infrastructure.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public TokenService(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public LoginToken GenerateRefreshToken(Account account)
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64);

            var refreshToken = new LoginToken
            {
                Token = Convert.ToBase64String(randomBytes),
                // Last for 15 minute
                ExpireAt = DateTime.UtcNow.AddMinutes(15),
                RefAccount = account.Id
            };

            return refreshToken;
        }

        public Task<string> GenerateTokenAsync(Account account)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));
            var userRole =  _unitOfWork.RoleRepository.FindAsync(account.RoleId);
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier,account.Id.ToString()),
                new(ClaimTypes.MobilePhone, account.Phone),
                new(ClaimTypes.Name, account.FullName),
                new(ClaimTypes.Role,userRole.Result.RoleName )
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                Issuer = _configuration["JWT:Issuer"],
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha384Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Task.FromResult(tokenHandler.WriteToken(token));
        }
    }
}
