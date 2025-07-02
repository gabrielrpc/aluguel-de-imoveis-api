using System.Net;
using System.Text.Json;

namespace aluguel_de_imoveis.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AluguelDeImoveisException ex)
            {
                context.Response.StatusCode = (int)ex.GetHttpStatusCode();
                context.Response.ContentType = "application/json";

                var errorMessages = ex.GetErrorMessages();

                object response;

                if (errorMessages != null && errorMessages.Count() > 1)
                {
                    response = new { erros = errorMessages };
                }
                else
                {
                    response = new { erro = ex.GetErrorMessage() };
                }

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (Exception)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var result = new
                {
                    erro = "Ocorreu um erro inesperado, tente novamente mais tarde!"
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(result));
            }
        }
    }
}
