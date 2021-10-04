using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PAYE.Api.Services;
using PAYE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAYE.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController : ControllerBase
    {
        private readonly ILogger<SalaryController> _logger;
        private readonly ISalaryCalculator _salary;
        public SalaryController(ISalaryCalculator salary)
        {
           // _logger = logger;
            _salary = salary;
        }

        [HttpPost("generate/salary")]
        public async Task<IActionResult> GenerateSalary(UserInputModel userInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserOutputModel userOutput = new();
                    var result = await _salary.GenerateSalary(userInput,userOutput);
                    return Ok(new
                    {
                        IsSucessful = result != null,
                        Message = result != null ? "Salary Generated Successfully":"Failed to generate salary",
                        Data = result
                    });
                }
                else
                return BadRequest(userInput);
            }
            catch (Exception e)
            {

               // _logger.LogError(e, "{MethodName}");
                return null;
            }
        }
    }
}
