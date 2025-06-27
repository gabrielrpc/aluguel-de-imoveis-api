using aluguel_de_imoveis.Communication.Request;
using aluguel_de_imoveis.Utils;
using FluentValidation;

namespace aluguel_de_imoveis.Services.Validations
{
    public class UsuarioValidations : AbstractValidator<RequestUsuarioJson>
    {
        public UsuarioValidations()
        {
            RuleFor(request => request.Nome).NotEmpty().WithMessage("O nome é obrigatório.").Must(nome =>
            {
                var partes = nome?.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                return partes != null && partes.Length >= 2;
            }).WithMessage("Informe pelo menos nome e sobrenome.");

            RuleFor(request => request.Email).EmailAddress().WithMessage("O email informado não é válido.");

            RuleFor(request => request.Cpf).NotEmpty().WithMessage("O CPF é obrigatório.").Must(CpfUtils.IsCpfValido)
                .WithMessage("O CPF informado é inválido.");

            RuleFor(request => request.Telefone).NotEmpty().WithMessage("O telefone é obrigatório.").Matches(@"^\d{10,11}$")
                .WithMessage("O Telefone informado é inválido.");

            RuleFor(request => request.Senha).NotEmpty().WithMessage("A senha é obrigatória.");

            When(request => string.IsNullOrEmpty(request.Senha) == false, () =>
            {
                RuleFor(request => request.Senha.Length).GreaterThanOrEqualTo(6).WithMessage("A senha deve ter no mínimo 6 caracteres.");
            });
        }
    }
}
