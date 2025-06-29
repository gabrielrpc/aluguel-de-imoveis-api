using System.Net;

namespace aluguel_de_imoveis.Exceptions
{
    public class ConflictException : AluguelDeImoveisException
    {
        public ConflictException(string message) : base(message) { }

        public override string GetErrorMessage() => Message;

        public override HttpStatusCode GetHttpStatusCode() => HttpStatusCode.Conflict;
    }
}
