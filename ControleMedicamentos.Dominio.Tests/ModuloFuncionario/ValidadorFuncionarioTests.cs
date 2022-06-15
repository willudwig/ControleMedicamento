using ControleMedicamentos.Dominio.ModuloFuncionario;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ControleMedicamentos.Dominio.Tests.ModuloFuncionario
{
    [TestClass]
    public class ValidadorFuncionarioTests
    {
        [TestMethod]
        public void NaoDeve_SerNulo_Nome()
        {
            Funcionario funcionario = new(null, "login teste", "senha teste");

            ValidadorFuncionario valfun = new();

            //action
            ValidationResult resultado = valfun.Validate(funcionario);

            //assert
            Assert.AreEqual("'Nome' não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void NaoDeve_SerVazio_Nome()
        {
            Funcionario funcionario = new("", "login teste", "senha teste");

            ValidadorFuncionario valfun = new();

            //action
            ValidationResult resultado = valfun.Validate(funcionario);

            //assert
            Assert.AreEqual("'Nome' não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void NaoDeve_SerMenor_Dois_Nome()
        {
            Funcionario funcionario = new("Q", "login teste", "senha teste");

            ValidadorFuncionario valfun = new();

            //action
            ValidationResult resultado = valfun.Validate(funcionario);

            //assert
            Assert.AreEqual("'Nome' deve ter no mínimo 2 caracteres", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void NaoDeve_SerNulo_Login()
        {
            Funcionario funcionario = new("sdfhslkdfh", null, "senha teste");

            ValidadorFuncionario valfun = new();

            //action
            ValidationResult resultado = valfun.Validate(funcionario);

            //assert
            Assert.AreEqual("'Login' não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void NaoDeve_SerVazio_Login()
        {
            //arrange
            Funcionario funcionario = new("sjkhslkjf", "", "senha teste");

            ValidadorFuncionario valfun = new();

            //action
            ValidationResult resultado = valfun.Validate(funcionario);

            //assert
            Assert.AreEqual("'Login' não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void NaoDeve_SerMenor_5_Login()
        {
            Funcionario funcionario = new("Jô", "log2", "senha teste");

            ValidadorFuncionario valfun = new();

            //action
            ValidationResult resultado = valfun.Validate(funcionario);

            //assert
            Assert.AreEqual("'Login' deve ter no mínimo 5 caracteres", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void NaoDeve_SerNulo_Senha()
        {
            Funcionario funcionario = new("sssss", "login teste", null);

            ValidadorFuncionario valfun = new();

            //action
            ValidationResult resultado = valfun.Validate(funcionario);

            //assert
            Assert.AreEqual("'Senha' não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void NaoDeve_SerVazio_Senha()
        {
            Funcionario funcionario = new("asdasdas", "login teste", "");

            ValidadorFuncionario valfun = new();

            //action
            ValidationResult resultado = valfun.Validate(funcionario);

            //assert
            Assert.AreEqual("'Senha' não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void NaoDeve_SerMenor_6_Senha()
        {
            Funcionario funcionario = new("Jô", "log245", "senh3");

            ValidadorFuncionario valfun = new();

            //action
            ValidationResult resultado = valfun.Validate(funcionario);

            //assert
            Assert.AreEqual("'Senha' deve ter no mínimo 6 caracteres", resultado.Errors[0].ErrorMessage);
        }
    }
}
