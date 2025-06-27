using aluguel_de_imoveis.Exceptions;
using System.Net;

namespace TechLibrary.Exception
{
    public class NotFoundException : AluguelDeImoveisException
    {
        public NotFoundException(string message) : base(message) { }

        public override List<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetHttpStatusCode() => HttpStatusCode.NotFound;
    }
}
