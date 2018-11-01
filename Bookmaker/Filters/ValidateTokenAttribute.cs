using Bookmaker.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bookmaker.Filters
{
    public class ValidateTokenAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                var token = context.HttpContext.Request.Headers["Token"];
                if (token == string.Empty)
                {
                    context.Result = new NotFoundObjectResult(token);
                }
                else
                {
                    context.HttpContext.Items["playerId"] = JwtHelper.ValidateToken(token);
                }
            }
            catch
            {
                context.Result = new BadRequestObjectResult("Wrong token");
            }          
        }
    }
}
