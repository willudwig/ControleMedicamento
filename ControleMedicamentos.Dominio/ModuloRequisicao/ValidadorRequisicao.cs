using FluentValidation;


namespace ControleMedicamentos.Dominio.ModuloRequisicao
{
    public class ValidadorRequisicao : AbstractValidator<Requisicao>
    {
        public ValidadorRequisicao()
        {
            RuleFor(x => x.Medicamento)
                                .NotNull().WithMessage("'Medicamento' não pode ser nulo");

            RuleFor(x => x.Paciente)
                                .NotNull().WithMessage("'Paciente' não pode ser nulo");

            RuleFor(x => x.Funcionario)
                                .NotNull().WithMessage("'Funcionario' não pode ser nulo");

            RuleFor(x => x.QtdMedicamento)
                                .NotEmpty().WithMessage("'Quantidade Medicamento' não pode ser vazio");

            RuleFor(x => x.Data)
                                .GreaterThan(System.DateTime.MinValue).WithMessage("'Data' incorreto");
            ;
        }
    }
}
