using PAYE.Api.Models;
using PAYE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAYE.Api.Services
{
    public class SalaryCalculator : ISalaryCalculator
    {
        double employerTier1 = 0;
        double employeeTier2,deductions = 0;
        double salary = 0,initialGross = 0;
        public Task<UserOutputModel> GenerateSalary(UserInputModel userInput,UserOutputModel userOutput)
        {
            double totalAllowances = UserSalaryCal.TotalAllowances(userInput.Allowance, userInput.OtherAllowance);
            initialGross = (double)(userInput.NetSalary + deductions);
             salary = initialGross * 0.5;
            employerTier1 = UserSalaryCal.TierOne(salary, Rates.EmployerFirstTier);
            employeeTier2 = UserSalaryCal.TierTwo(salary, Rates.EmployeeTierTwo);
            deductions = employerTier1 + employeeTier2 + UserSalaryCal.ThreeTier(salary);

            userOutput.EmployerPensionContribution_Tier_Three = UserSalaryCal.ThreeTier(salary);
            userOutput.EmployeePensionAmount_Tier_Three = UserSalaryCal.ThreeTier(salary);
            userOutput.EmployeePensionAmount_Tier_One = 0;
            userOutput.EmployeePensionAmount_Tier_Two = employeeTier2;
            userOutput.EmployerPensionContribution_Tier_One = employerTier1;
            userOutput.TotalPayeTax = UserSalaryCal.PAYE(salary);

            userOutput.TotalAllowances = totalAllowances;
            userOutput.GrossSalary = initialGross + deductions;
            userOutput.BasicSalary = initialGross * 0.5;

            return Task.FromResult(userOutput);
        }
    }
}
