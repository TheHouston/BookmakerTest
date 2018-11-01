using Bookmaker.Core;
using Bookmaker.Extentions;
using Bookmaker.Filters;
using Bookmaker.JsonModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace Bookmaker.Controllers
{
    [ValidateModel]
    public class BookmakerController : Controller
    {
        private readonly IBookmakerRepository _bookmakerRepository;
        private readonly ILogger _logger;
        public BookmakerController(IBookmakerRepository bookmakerRepository, ILogger<BookmakerController> logger)
        {
            _bookmakerRepository = bookmakerRepository;
            _logger = logger;
        }

        [HttpGet("GetSportMenu")]
        public IActionResult GetSportMenu()
        {
            try
            {
                return Json(
                      new
                      {
                          Success = true,
                          Data = _bookmakerRepository.GetActualSports()
                      });

            }
            catch (Exception exception)
            {
                _logger.LogInformation(exception.Message);
                return Json(new { Success = false, Message = exception.Message });
            }
        }


        [HttpPost("Login")]
        public IActionResult Login([FromBody]PlayerModel model)
        {
            try
            {
                return Json(
                    new
                    {
                        Success = true,
                        Token = _bookmakerRepository.Authorization(model.Login, model.Password)
                    });
            }
            catch (Exception exception)
            {
                _logger.LogInformation(exception.Message);
                return Json(new { Success = false, Message = exception.Message });
            }

        }

        [HttpGet("Champs/{id}/GetActualEvents")]
        public IActionResult GetActualEvents(int id)
        {
            try
            {
                return Json(
                    new
                    {
                        Success = true,
                        Events = _bookmakerRepository.GetActualEventsByChampId(id)
                    });
            }
            catch (Exception exception)
            {
                _logger.LogInformation(exception.Message);
                return Json(new { Success = false, Message = exception.Message });
            }
        }

        [HttpPost("PlaceBet")]
        [ValidateToken]
        public async Task<IActionResult> PlaceBet([FromBody]BetModel model)
        {
            try
            {
                var playerId = (int)HttpContext.Items["playerId"];
                var paymentId = _bookmakerRepository.GetPaymentId(playerId);
                var paymentSystemResult = JsonConvert.DeserializeObject<PaymentSystemResult>(
                    await RequestExecutor.ExecuteRequest(Scope.PaymentSystemUrl,
                           new RestRequest("/CheckFunds", Method.POST)
                               .AddHeader("Content-type", "application/json")
                               .AddJsonBody(new
                               {
                                   user_id = paymentId,
                                   bet_sum = model.BetSum
                               })));
                if (!paymentSystemResult.Success)
                    throw new Exception(paymentSystemResult.Message);
                _logger.LogInformation("Success result from payment system");
                var betId = _bookmakerRepository.PlaceBet(model.EventId, playerId, model.TeamId, model.Price, model.BetSum);
                return Json(new { Success = true, BetId = betId });
            }
            catch (Exception exception)
            {
                _logger.LogInformation(exception.Message);
                return Json(new { Success = false, Message = exception.Message });
            }
        }
    }
}
