using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace OrderProd.Exception
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new ObjectResult(new
            {
                error = context.Exception.Message
            })
            {
                StatusCode = 500
            };
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is NotFoundResult || context.Result is NotFoundObjectResult)
            {
                context.Result = new NotFoundObjectResult(new
                {
                    error = "Resource not found",
                    path = context.HttpContext.Request.Path
                });
            }
        }

        public void OnActionExecuting(ActionExecutingContext context) { }
    }
}
