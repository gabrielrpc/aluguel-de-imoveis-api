﻿using System.Net;

namespace aluguel_de_imoveis.Exceptions
{
    public class ErrorOnValidationException : AluguelDeImoveisException
    {
        private readonly List<string> _errors;

        public ErrorOnValidationException(List<string> errorMessages) : base(string.Empty)
        {
            _errors = errorMessages;
        }

        public override List<string> GetErrorMessages() => _errors;

        public override HttpStatusCode GetHttpStatusCode() => HttpStatusCode.BadRequest;
    }
}
