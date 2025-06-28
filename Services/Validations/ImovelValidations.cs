using aluguel_de_imoveis.Communication.Request;
using FluentValidation;

namespace aluguel_de_imoveis.Services.Validations
{
    public class ImovelValidations : AbstractValidator<RequestImovelJson>
    {
        public ImovelValidations()
        {
            RuleFor(request => request.Titulo).NotEmpty().WithMessage("O título é obrigatório.");
            RuleFor(request => request.Descricao).NotEmpty().WithMessage("A descrição é obrigatória.");
            RuleFor(request => request.ValorAluguel).GreaterThan(0).WithMessage("O valor do aluguel deve ser maior que zero.");
            RuleFor(request => request.Tipo).GreaterThanOrEqualTo(0).WithMessage("O tipo do imóvel é obrigatório.");
            RuleFor(request => request.Endereco).NotNull().WithMessage("O endereço é obrigatório.");
            When(request => request.Endereco != null, () =>
            {
                RuleFor(request => request.Endereco.Logradouro).NotEmpty().WithMessage("A rua é obrigatória.");
                RuleFor(request => request.Endereco.Numero).GreaterThanOrEqualTo(0).NotEmpty().WithMessage("O número é obrigatório.");
                RuleFor(request => request.Endereco.Bairro).NotEmpty().WithMessage("O bairro é obrigatório.");
                RuleFor(request => request.Endereco.Cidade).NotEmpty().WithMessage("A cidade é obrigatória.");
                RuleFor(request => request.Endereco.Uf).NotEmpty().Length(2).WithMessage("O estado (UF) deve conter apenas as siglas.");
                RuleFor(request => request.Endereco.Cep).Matches(@"^\d{8}$").NotEmpty().WithMessage("O CEP é obrigatório.");
            });
        }
    }
}
