

using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleMedicamentos.Dominio.Tests.ModuloRequisicao
{
    [TestClass]
    public class ValidadorRequisicaoTests
    {
        [TestMethod]
        public void NaoDeve_SerNulo_Medicamento()
        {
            //arrange
            Requisicao req = new();
            req.Medicamento = null;

            ValidadorRequisicao valreq = new();

            //action
            ValidationResult resultado = valreq.Validate(req);

            //assert
            Assert.AreEqual("'Medicamento' não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void NaoDeve_SerNulo_Paciente()
        {
            //arrange
            Requisicao req = new();
            req.Medicamento = InstanciarMedicamento();
            req.Paciente = null;

            ValidadorRequisicao valreq = new();

            //action
            ValidationResult resultado = valreq.Validate(req);

            //assert
            Assert.AreEqual("'Paciente' não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void NaoDeve_SerNulo_Funcionario()
        {
            //arrange
            Requisicao req = new();
            req.Medicamento = InstanciarMedicamento();
            req.Paciente = InstanciarPaciente();
            req.Funcionario = null;

            ValidadorRequisicao valreq = new();

            //action
            ValidationResult resultado = valreq.Validate(req);

            //assert
            Assert.AreEqual("'Funcionario' não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void NaoDeve_SerVazio_Quantidade()
        {
            //arrange
            Requisicao req = new();
            req.Medicamento = InstanciarMedicamento();
            req.Paciente = InstanciarPaciente();
            req.Funcionario = InstanciarFuncioinario();
            req.QtdMedicamento = 0;

            ValidadorRequisicao valreq = new();

            //action
            ValidationResult resultado = valreq.Validate(req);

            //assert
            Assert.AreEqual("'Quantidade Medicamento' não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void NaoDeve_SerVazio_Data()
        {
            //arrange
            Requisicao req = new();
            req.Medicamento = InstanciarMedicamento();
            req.Paciente = InstanciarPaciente();
            req.Funcionario = InstanciarFuncioinario();
            req.QtdMedicamento = 40;
            req.Data = DateTime.MinValue;

            ValidadorRequisicao valreq = new();

            //action
            ValidationResult resultado = valreq.Validate(req);

            //assert
            Assert.AreEqual("'Data' incorreto", resultado.Errors[0].ErrorMessage);
        }

        #region privados

        private Medicamento InstanciarMedicamento()
        {
            return new Medicamento("nome sdffsd", "decricao djkhdkjg", "lote 23746278", DateTime.Now)
            {
                Fornecedor = new Fornecedor("nome teste", "999 9999", "teste@gmail.com", "cidade teste", "estado teste")
                {

                },

                QuantidadeDisponivel = 35,
                Id = 100
            };
        }

        private Paciente InstanciarPaciente()
        {
            return new Paciente("nome paciente", "765945603457632")
            {
                Id = 1
            };
        }

        private Funcionario InstanciarFuncioinario()
        {
            return new Funcionario("nomefunc", "funlogin", "293742893")
            {

            };
        }

        #endregion
    }
}
