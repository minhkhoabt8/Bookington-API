using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Slot
{
    public class SlotUpdateDTO
    {
        public string Id { get; set; }
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Invalid price format. Price must be a positive number.")]
        public double Price { get; set; }

        public bool IsActive { get; set; }
    }
}
