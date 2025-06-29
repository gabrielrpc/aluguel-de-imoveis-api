using aluguel_de_imoveis.Communication.Request;
using aluguel_de_imoveis.Models;
using FluentValidation;

namespace aluguel_de_imoveis.Services.Validations
{
    public class LocacaoValidations : AbstractValidator<RequestRegistrarLocacaoJson>
    {
        public LocacaoValidations()
        {
            RuleFor(locacao => locacao.DataInicio)
                .Must(data => data.Date >= DateTime.UtcNow.Date)
                .WithMessage("A data de início não pode ser anterior ao dia atual.");

            RuleFor(locacao => locacao)
                .Must(locacao => (locacao.DataFim.Date - locacao.DataInicio.Date).TotalDays >= 365)
                .WithMessage("O período da locação deve ser de pelo menos 1 ano.");
        }
    }
}
