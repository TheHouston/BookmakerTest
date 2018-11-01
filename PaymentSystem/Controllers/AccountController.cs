using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentSystem.Core;
using PaymentSystem.Filters;
using PaymentSystem.JsonModel;
using System;

namespace PaymentSystem.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [ValidateModel]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger _logger;
        public AccountController(IAccountRepository accountRepository, ILogger<AccountController> logger)
        {
            _accountRepository = accountRepository;
            _logger = logger;
        }
        [HttpPost("CheckFunds")]
        public IActionResult CheckFunds([FromBody] BetSumModel model)
        {           
            try
            {
                var checkingResult = _accountRepository.CheckFunds(model.UserId, model.BetSum);
                if (!checkingResult)
                    throw new Exception("No funds for bet");
                return Json(new { Success = true }); 
            }
            catch (Exception exception)
            {
                _logger.LogInformation(exception.Message);
                return Json(new { Success = false, Message = exception.Message });
            }
        }
    }
}
