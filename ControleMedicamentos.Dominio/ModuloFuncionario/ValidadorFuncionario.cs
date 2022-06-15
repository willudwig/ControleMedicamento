using FluentValidation;


namespace ControleMedicamentos.Dominio.ModuloFuncionario
{
    public class ValidadorFuncionario : AbstractValidator<Funcionario>
    {
        public ValidadorFuncionario()
        {
            RuleFor(x => x.Nome)
                            .NotNull().WithMessage("'Nome' não pode ser nulo")
                            .NotEmpty().WithMessage("'Nome' não pode ser vazio")
                            .MinimumLength(2).WithMessage("'Nome' deve ter no mínimo 2 caracteres");

            RuleFor(x => x.Login)
                            .NotNull().WithMessage("'Login' não pode ser nulo")
                            .NotEmpty().WithMessage("'Login' não pode ser vazio")
                            .MinimumLength(5).WithMessage("'Login' deve ter no mínimo 5 caracteres");

            RuleFor(x => x.Senha)
                            .NotNull().WithMessage("'Senha' não pode ser nulo")
                            .NotEmpty().WithMessage("'Senha' não pode ser vazio")
                            .MinimumLength(6).WithMessage("'Senha' deve ter no mínimo 6 caracteres");
        }
    }
}
