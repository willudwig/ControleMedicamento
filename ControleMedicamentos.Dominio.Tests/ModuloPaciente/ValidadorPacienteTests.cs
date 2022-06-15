using ControleMedicamentos.Dominio.ModuloPaciente;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ControleMedicamentos.Dominio.Tests.ModuloPaciente
{
    [TestClass]
    public class ValidadorPacienteTests
    {
        [TestMethod]
        public void NaoDeve_SerNulo_Nome()
        {
            //arrange
            Paciente paciente = new(null, "797979797987979797987979");

            ValidadorPaciente valpac = new();

            //action
            ValidationResult resultado = valpac.Validate(paciente);

            //assert
            Assert.AreEqual("'Nome' não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void NaoDeve_SerVazio_Nome()
        {
            //arrange
            Paciente paciente = new("", "797979797987979797987979");

            ValidadorPaciente valpac = new();

            //action
            ValidationResult resultado = valpac.Validate(paciente);

            //assert
            Assert.AreEqual("'Nome' não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void NaoDeve_SerNulo_CartaoSus()
        {
            //arrange
            Paciente paciente = new("nome teste", null);

            ValidadorPaciente valpac = new();

            //action
            ValidationResult resultado = valpac.Validate(paciente);

            //assert
            Assert.AreEqual("'Cartão SUS' não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void NaoDeve_SerVazio_CartaoSus()
        {
            //arrange
            Paciente paciente = new("nome teste", "");

            ValidadorPaciente valpac = new();

            //action
            ValidationResult resultado = valpac.Validate(paciente);

            //assert
            Assert.AreEqual("'Cartão SUS' não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Deve_TerMinimoQuinze_CartaoSus()
        {
            //arrange
            Paciente paciente = new("nome teste", "6876");

            ValidadorPaciente valpac = new();

            //action
            ValidationResult resultado = valpac.Validate(paciente);

            //assert
            Assert.AreEqual("'Cartão SUS' formato incorreto", resultado.Errors[0].ErrorMessage);
            //}

        }

        [TestMethod]
        public void Deve_TerMaximoQuinze_CartaoSus()
        {
            //arrange
            Paciente paciente = new("nome teste", "68763434345345345345345345345345");

            ValidadorPaciente valpac = new();

            //action
            ValidationResult resultado = valpac.Validate(paciente);

            //assert
            Assert.AreEqual("'Cartão SUS' formato incorreto", resultado.Errors[0].ErrorMessage);
            //}

        }
    }
}
