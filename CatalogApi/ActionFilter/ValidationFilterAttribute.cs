using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Catalog.Service.Exceptions;

namespace Catalog.Entity.ActionFilter
{
    public class ValidationFilterAttribute : ActionFilterAttribute
    {//validate aspect validation interception
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.RouteData.Values["controller"];
            var action = context.RouteData.Values["action"];

            // Dto
            var param = context.ActionArguments.
                SingleOrDefault(p => p.Value.ToString().Contains("Dto")).Value;

            if (param is null)
            {
                throw new BadRequestException($"Object is null. " +
                    $"Controller : {controller} " +
                    $"Action: {action}" +
                    $"Object : {param}");

            }
            if (!context.ModelState.IsValid)
                context.Result = new UnprocessableEntityObjectResult(context.ModelState); // 422
        }
    }
}
