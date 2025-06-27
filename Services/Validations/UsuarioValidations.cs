using aluguel_de_imoveis.Communication.Request;
using FluentValidation;

namespace aluguel_de_imoveis.Services.Validations
{
    public class UsuarioValidations : AbstractValidator<RequestUsuarioJson>
    {
        public UsuarioValidations()
        {
            RuleFor(request => request.Nome).NotEmpty().WithMessage("O nome é obrigatório.");
            RuleFor(request => request.Email).EmailAddress().WithMessage("O email informado não é válido.");
            RuleFor(request => request.Senha).NotEmpty().WithMessage("A senha é obrigatória.");
            When(request => string.IsNullOrEmpty(request.Senha) == false, () =>
            {
                RuleFor(request => request.Senha.Length).GreaterThanOrEqualTo(6).WithMessage("A senha deve ter no mínimo 6 caracteres.");
            });
            RuleFor(request => request.Tipo).NotEmpty().WithMessage("O tipo é obrigatório.");

        }
    }
}
