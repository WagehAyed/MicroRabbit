using MicroRabbit.Banking.Application.Interfaces;
using MicroRabbit.Banking.Application.Models;
using MicroRabbit.Banking.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankingController : ControllerBase
    { 
        private readonly IAccountService _accountService;
        public BankingController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        //Get Api Banking
        [HttpGet]
        public async Task<ActionResult> get()
        {
            return Ok(_accountService.GetAccounts());
        }

        //Account Transfer
        [HttpPost]
        public IActionResult post([FromBody] AccountTransfer accountTransfer) {
            _accountService.Transfer(accountTransfer);
            return Ok(accountTransfer);
        }


        
    }
}
