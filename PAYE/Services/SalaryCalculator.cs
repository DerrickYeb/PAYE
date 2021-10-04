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
        double salary = 0;
        public Task<UserOutputModel> GenerateSalary(UserInputModel userInput,UserOutputModel userOutput)
        {
            double totalAllowances = UserSalaryCal.TotalAllowances(userInput.Allowance, userInput.OtherAllowance);
            deductions = employerTier1 + employeeTier2 + UserSalaryCal.ThreeTier(salary);
            double initialGross = (double)(userInput.NetSalary + totalAllowances);
             salary = initialGross * 0.5;
            employerTier1 = UserSalaryCal.TierOne(salary, Rates.EmployerFirstTier);
            employeeTier2 = UserSalaryCal.TierTwo(salary, Rates.EmployeeTierTwo);
            userOutput.EmployerPensionContribution_Tier_Three = UserSalaryCal.ThreeTier(salary);
            userOutput.EmployeePensionAmount_Tier_Three = UserSalaryCal.ThreeTier(salary);
            userOutput.EmployeePensionAmount_Tier_One = 0;
            userOutput.EmployeePensionAmount_Tier_Two = employeeTier2;
            userOutput.EmployerPensionContribution_Tier_One = employerTier1;
            userOutput.TotalPayeTax = UserSalaryCal.PAYE(salary);

            userOutput.TotalAllowances = totalAllowances;
            userOutput.Deductions = deductions;
            userOutput.GrossSalary = initialGross;
            userOutput.BasicSalary = salary;

            return Task.FromResult(userOutput);
        }
    }
}
