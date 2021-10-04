using Microsoft.AspNetCore.Mvc;
using PAYE.Api.Controllers;
using PAYE.Api.Services;
using PAYE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PayeApi.Test
{
    public class SalaryCalculatorControllerTest:IClassFixture<SalaryCalculatorControllerTest>
    {
        ISalaryCalculator _salary;
        SalaryController _controller;
        private readonly HttpClient _client;
        public SalaryCalculatorControllerTest()
        {
            _salary = new SalaryCalculator();
            _controller = new SalaryController(_salary);
            _client = new HttpClient();
        }
        [Fact]
        public async Task Send_InvalidObjectsPassed_ReturnsBadRequest()
        {
            var itemMissing = new UserInputModel
            {
                Allowance = 343,
                OtherAllowance = 33
            };
            _controller.ModelState.AddModelError("NetSalary", "Required");
            var badResponse = await _controller.GenerateResult(itemMissing);
           Assert.IsType<BadRequestObjectResult>(badResponse);
        }
       
        [Fact]
        public async Task Send_ValidObjectPassed_ReturnsCreatedResponse()
        {
            var items = new UserInputModel
            {
                NetSalary = 20000,
                Allowance = 200,
                OtherAllowance = 4
            };
           
            var send_reponse = await _controller.GenerateResult(items);
            Assert.IsAssignableFrom<OkObjectResult>(send_reponse);
        }
        [Fact]
        public async Task Send_ValidObjectPassed_ReturnedResponseHasGeneratedResult()
        {
            var itemList = new UserInputModel
            {
                NetSalary = 2000,
                Allowance = 33,
                OtherAllowance = 30,
                Bonus = 20
            };
            var createdResponse = await _controller.GenerateResult(itemList) as OkObjectResult;
            var items = createdResponse.Value as UserOutputModel;
            Assert.IsType<UserOutputModel>(items);
            Assert.Equal(1000, items.BasicSalary);
        }
    }
}
