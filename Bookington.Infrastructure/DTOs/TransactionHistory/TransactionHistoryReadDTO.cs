﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.TransactionHistory
{
    public class TransactionHistoryReadDTO
    {
        public string Id { get; set; } = null!;

        public string RefFrom { get; set; } = null!;

        public string FromUsername { get; set; } = null!;

        public string RefTo { get; set; } = null!;

        public string ToUsername { get; set; } = null!;

        public double Amount { get; set; }
    }
}
