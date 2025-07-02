using System.Net;

namespace aluguel_de_imoveis.Exceptions
{
    public class BadRequestException : AluguelDeImoveisException
    {
        public BadRequestException(string message) : base(message) { }

        public override string GetErrorMessage() => Message;

        public override HttpStatusCode GetHttpStatusCode() => HttpStatusCode.BadRequest;

    }
}
