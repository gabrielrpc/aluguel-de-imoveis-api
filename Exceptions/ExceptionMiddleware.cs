using System.Net;

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

                var result = new
                {
                    erros = ex.GetErrorMessages()
                };

                await context.Response.WriteAsJsonAsync(result);
            }
            catch (Exception)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var result = new
                {
                    erro = "Ocorreu um erro inesperado, tente novamente mais tarde!"
                };

                await context.Response.WriteAsJsonAsync(result);
            }
        }
    }
}
