using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PAYE.Models
{
    public class UserInputModel
    {
        [Required]
        public double NetSalary { get; set; }
        public double Allowance { get; set; }
        public double OtherAllowance { get; set; }
        public double Bonus { get; set; }
    }
}
