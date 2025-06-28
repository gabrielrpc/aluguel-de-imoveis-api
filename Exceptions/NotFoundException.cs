using System.Net;

namespace aluguel_de_imoveis.Exceptions
{
    public class NotFoundException : AluguelDeImoveisException
    {
        public NotFoundException(string message) : base(message) { }

        public override string GetErrorMessage() => Message;

        public override HttpStatusCode GetHttpStatusCode() => HttpStatusCode.NotFound;
    }
}
