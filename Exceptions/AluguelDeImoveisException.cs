using System.Net;

namespace aluguel_de_imoveis.Exceptions
{
    public abstract class AluguelDeImoveisException : SystemException
    {
        protected AluguelDeImoveisException(string message) : base(message) { }
        public virtual List<string> GetErrorMessages() => new List<string> { Message };
        public virtual string GetErrorMessage() => Message;
        public abstract HttpStatusCode GetHttpStatusCode();
    }
}
