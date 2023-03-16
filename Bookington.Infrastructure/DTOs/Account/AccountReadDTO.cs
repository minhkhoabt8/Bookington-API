﻿using Bookington.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Account
{
    public class AccountReadDTO
    {
        public string Id { get; set; }

        public string RoleName { get; set; }

        public string Phone { get; set; }

        public DateTime DateOfBirth { get; set; }        

        public string FullName { get; set; }

        public DateTime CreateAt { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class AccountLoginInputDTO
    {
        public string Phone { get; set; }
         public string Password
         {
            get { return password; }
            set { password = EncryptPassword(value); }
         }

        private string password;

        public static string EncryptPassword(string pass)
        {
            return BCrypt.Net.BCrypt.HashPassword(pass);
        }
    }

    public class AccountLoginOutputDTO
    {
        public string UserID { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public string SysToken { get; set; }
        public int SysTokenExpires { get; set; }
        public string RefreshToken { get; set; }
        public int RefreshTokenExpires { get; set; }
    }

    public class ConfirmUserDTO
    {
        public string Phone { get; set; }
        public string OTP { get; set; }
    }

    public class AccountProfileReadDTO
    {        
        public string Phone { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public virtual AccountAvatar RefAvatarNavigation { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
