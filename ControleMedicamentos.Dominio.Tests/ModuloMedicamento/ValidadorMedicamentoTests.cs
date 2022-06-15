using ControleMedicamentos.Dominio.ModuloMedicamento;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleMedicamentos.Dominio.Tests.ModuloMedicamento
{
    [TestClass]
    public class ValidadorMedicamentoTests
    {
        [TestMethod]
        public void NaoDeve_SerNulo_Nome()
        {
            //arrange
            Medicamento medicamento = new(null, "descrição teste", "lote teste", DateTime.Now);

            ValidadorMedicamento valmed = new();

            //action
            ValidationResult resultado = valmed.Validate(medicamento);

            //assert
            Assert.AreEqual("'Nome' não pode ser nulo", resultado.Errors[0].ErrorMessage); 
        }

        [TestMethod]
        public void NaoDeve_SerVazio_Nome()
        {
            //arrange
            Medicamento medicamento = new("", "descrição teste", "lote teste", DateTime.Now);

            ValidadorMedicamento valmed = new();

            //action
            ValidationResult resultado = valmed.Validate(medicamento);

            //assert
            Assert.AreEqual("'Nome' não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void NaoDeve_SerNulo_Descricao()
        {
            //arrange
            Medicamento medicamento = new("Nome Teste", null, "lote teste", DateTime.Now);

            ValidadorMedicamento valmed = new();

            //action
            ValidationResult resultado = valmed.Validate(medicamento);

            //assert
            Assert.AreEqual("'Descrição' não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void NaoDeve_SerVazio_Descricao()
        {
            //arrange
            Medicamento medicamento = new("Nome Teste", "", "lote teste", DateTime.Now);

            ValidadorMedicamento valmed = new();

            //action
            ValidationResult resultado = valmed.Validate(medicamento);

            //assert
            Assert.AreEqual("'Descrição' não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void NaoDeve_SerNulo_Lote()
        {
            //arrange
            Medicamento medicamento = new("nome teste", "descrição teste", null, DateTime.Now);

            ValidadorMedicamento valmed = new();

            //action
            ValidationResult resultado = valmed.Validate(medicamento);

            //assert
            Assert.AreEqual("'Lote' não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void NaoDeve_SerVazio_Lote()
        {
            //arrange
            Medicamento medicamento = new("nome teste", "descrição teste", "", DateTime.Now);

            ValidadorMedicamento valmed = new();

            //action
            ValidationResult resultado = valmed.Validate(medicamento);

            //assert
            Assert.AreEqual("'Lote' não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Deve_SerMaior_ValorMinimo_Validade()
        {
            //arrange
            Medicamento medicamento = new("nome teste", "descrição teste", "098F233346", DateTime.MinValue);

            ValidadorMedicamento valmed = new();

            //action
            ValidationResult resultado = valmed.Validate(medicamento);

            //assert
            Assert.AreEqual("'Validade' data incorreta", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Deve_SerMaior_Zero_QuantidadeDisponivel()
        {
            //arrange
            Medicamento medicamento = new("nome teste", "descrição teste", "erterrterterr", DateTime.Now);
            medicamento.QuantidadeDisponivel = 0;

            ValidadorMedicamento valmed = new();

            //action
            ValidationResult resultado = valmed.Validate(medicamento);

            //assert
            Assert.AreEqual("'Quantidade Disponível' não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void NaoDeve_SerNulo_Fornecedor()
        {
            //arrange
            Medicamento medicamento = new("nome teste", "descrição teste", "sgdgsdg4t43453", DateTime.Now);
            medicamento.QuantidadeDisponivel = 55;
            medicamento.Fornecedor = null;

            ValidadorMedicamento valmed = new();

            //action
            ValidationResult resultado = valmed.Validate(medicamento);

            //assert
            Assert.AreEqual("'Fornecedor' não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }

    }
}
