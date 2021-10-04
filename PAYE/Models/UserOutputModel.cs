using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAYE.Models
{
    public class UserOutputModel
    {
        public double BasicSalary { get; set; }
        public double TotalPayeTax { get; set; }
        public double EmployerPensionContribution_Tier_One { get; set; }
        public double EmployeePensionAmount_Tier_One { get; set; }
        public double EmployerPensionContribution_Tier_Two { get; set; }
        public double EmployeePensionAmount_Tier_Two { get; set; }
        public double EmployerPensionContribution_Tier_Three { get; set; }
        public double EmployeePensionAmount_Tier_Three { get; set; }
        public double GrossSalary { get; set; }
        public object Deductions { get; set; }
        public double TotalAllowances { get; set; }
    }
}
