using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace OrderProd.Exception
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError; // По умолчанию 500
            var errorMessage = context.Exception.Message;

            if (context.Exception is ArgumentException)
            {
                statusCode = HttpStatusCode.BadRequest; // 400
                errorMessage = "Некорректный запрос";
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                statusCode = HttpStatusCode.Unauthorized; // 401
                errorMessage = "Доступ запрещен";
            }
            else if (context.Exception is KeyNotFoundException)
            {
                statusCode = HttpStatusCode.NotFound; // 404
                errorMessage = "Ресурс не найден";
            }
            if (context.Exception is ValidationException validationException)
            {
                statusCode = HttpStatusCode.BadRequest;
                errorMessage = "Ошибка валидации: " + validationException.Message;
            }

            context.Result = new ObjectResult(new
            {
                error = errorMessage,
                exceptionType = context.Exception.GetType().Name,
                path = context.HttpContext.Request.Path
            })
            {
                StatusCode = (int)statusCode
            };

            context.ExceptionHandled = true; // Помечаем исключение как обработанное
        }
    }
}
