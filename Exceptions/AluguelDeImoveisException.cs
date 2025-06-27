using System.Net;

namespace aluguel_de_imoveis.Exceptions
{
    public abstract class AluguelDeImoveisException : SystemException
    {
        protected AluguelDeImoveisException(string message) : base(message) { }
        public abstract List<string> GetErrorMessages();
        public abstract HttpStatusCode GetHttpStatusCode();
    }
}
