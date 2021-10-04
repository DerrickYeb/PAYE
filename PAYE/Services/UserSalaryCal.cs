using PAYE.Api.Models;
using PAYE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAYE.Api.Services
{
    public static class UserSalaryCal
    {
        public static double BasicSalary(double grossPay)
        {
            
            double result = grossPay * 0.5;
            return result;
        }
        public static double Allowances(double allowance,double otherAllowances)
        {

            return allowance + otherAllowances;
        }
        public static double TierTwo(double salary,double rate)
        {
            return salary * rate;
        }
        public static double TierOne(double salary,double rate)
        {
            return salary * rate;
        }
        public static double PAYE(double salary)
        {
            if (salary >= 20000)
            {
                return Math.Floor(ChargeableIncome(20000, salary, 0.30, 4657.25));
            }
            else if (salary > 3539 && salary <= 20000)
            {
                return Math.Floor(ChargeableIncome(3539, salary, 0.25, 542));
            }
            else if (salary > 539 && salary <= 3539)
            {
                return Math.Floor(ChargeableIncome(539, salary, 0.175, 17));
            }
            else if (salary > 419 && salary <= 539)
            {
                return Math.Floor(ChargeableIncome(419, salary, 0.10, 5));
            }
            else if (salary > 319 && salary <= 419)
            {
                return Math.Floor(ChargeableIncome(319, salary, 0, 0));
            }
            return 0;
        }
        //public static double GrossSalary(double netAmount, double salary, double[] allowancess) => netAmount + Deductions(BasicSalary(salary));
        public static double Deductions(double tier1,double tier2,double tier3)
        {
            return tier1 + tier2 + tier3;
        }
        public static double ThreeTier(double basicSalary)
        {
            return basicSalary * 0.5;
        }
        public static double TotalAllowances(double allowances,double others)
        {

            return allowances + others;
        }

        private static double ChargeableIncome(double amount, double salary, double rate, double cum_tax)
        {
            double net_salary = salary - amount;
            double cumulative_tax_total = net_salary * rate;
            double cumulative_tax = cumulative_tax_total + cum_tax;
            return cumulative_tax;
        }
    }
}
