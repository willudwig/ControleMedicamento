using FluentValidation;
using System;

namespace ControleMedicamentos.Dominio.ModuloMedicamento
{
    public class ValidadorMedicamento : AbstractValidator<Medicamento>
    {
        public ValidadorMedicamento()
        {
            RuleFor(x => x.Nome).NotNull().WithMessage("'Nome' não pode ser nulo")
                                .NotEmpty().WithMessage("'Nome' não pode ser vazio"); 

            RuleFor(x => x.Descricao)
                                .NotNull().WithMessage("'Descrição' não pode ser nulo")
                                .NotEmpty().WithMessage("'Descrição' não pode ser vazio");

            RuleFor(x => x.Lote)
                                .NotNull().WithMessage("'Lote' não pode ser nulo")
                                .NotEmpty().WithMessage("'Lote' não pode ser vazio");

            RuleFor(x => x.Validade)
                                .NotNull().WithMessage("'Validade' data incorreta")
                                .NotEmpty().WithMessage("'Validade' data incorreta")
                                .GreaterThan(DateTime.MinValue).WithMessage("'Validade' data incorreta");

            RuleFor(x => x.QuantidadeDisponivel)
                                .NotNull()
                                .NotEmpty().WithMessage("'Quantidade Disponível' não pode ser vazio");

            RuleFor(x => x.Fornecedor)
                                .NotNull().WithMessage("'Fornecedor' não pode ser nulo");
        }
    }
}
