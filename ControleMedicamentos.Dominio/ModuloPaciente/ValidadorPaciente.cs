using FluentValidation;


namespace ControleMedicamentos.Dominio.ModuloPaciente
{
    public class ValidadorPaciente : AbstractValidator<Paciente>
    {
        public ValidadorPaciente()
        {
            RuleFor(x => x.Nome)
                            .NotNull().WithMessage("'Nome' não pode ser nulo")
                            .NotEmpty().WithMessage("'Nome' não pode ser vazio");

            RuleFor(x => x.CartaoSUS)
                            .NotNull().WithMessage("'Cartão SUS' não pode ser nulo")
                            .NotEmpty().WithMessage("'Cartão SUS' não pode ser vazio")
                            .MinimumLength(15).WithMessage("'Cartão SUS' formato incorreto")
                            .MaximumLength(15).WithMessage("'Cartão SUS' formato incorreto");
        }
    }
}
