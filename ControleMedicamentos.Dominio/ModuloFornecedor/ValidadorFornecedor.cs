using FluentValidation;


namespace ControleMedicamentos.Dominio.ModuloFornecedor
{
    public class ValidadorFornecedor : AbstractValidator<Fornecedor>
    {
        public ValidadorFornecedor()
        {
            RuleFor(x => x.Nome)
                            .NotNull().WithMessage("'Nome' não pode ser nulo")
                            .NotEmpty().WithMessage("'Nome' não pode ser vazio");

            RuleFor(x => x.Telefone)
                            .NotNull().WithMessage("'Telefone' não pode ser nulo")
                            .NotEmpty().WithMessage("'Telefone' não pode ser vazio");
            
            RuleFor(x => x.Email)
                            .EmailAddress().WithMessage("'E-mail' em formato incorreto");

            RuleFor(x => x.Estado)
                            .MinimumLength(2).WithMessage("'Estado' somente duas letras")
                            .MaximumLength(2).WithMessage("'Estado' somente duas letras");

        }
    }
}
