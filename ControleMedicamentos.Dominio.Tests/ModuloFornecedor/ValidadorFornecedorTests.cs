using ControleMedicamentos.Dominio.ModuloFornecedor;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ControleMedicamentos.Dominio.Tests.ModuloFornecedor
{
    [TestClass]
    public class ValidadorFornecedorTests
    {
        [TestMethod]
        public void NaoDeve_SerNulo_Nome()
        {
            Fornecedor fornecedor = new(null, "telefone teste", "email@gmail.com", "cidade teste", "ES");

            ValidadorFornecedor valfor = new();

            //action
            ValidationResult resultado = valfor.Validate(fornecedor);

            //assert
            Assert.AreEqual("'Nome' não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void NaoDeve_SerVazio_Nome()
        {
            Fornecedor fornecedor = new("", "telefone teste", "email teste", "cidade teste", "ee");

            ValidadorFornecedor valfor = new();

            //action
            ValidationResult resultado = valfor.Validate(fornecedor);

            //assert
            Assert.AreEqual("'Nome' não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void NaoDeve_SerNulo_Telefone()
        {
            Fornecedor fornecedor = new("nome teste", null, "email teste", "cidade teste", "ee");

            ValidadorFornecedor valfor = new();

            //action
            ValidationResult resultado = valfor.Validate(fornecedor);

            //assert
            Assert.AreEqual("'Telefone' não pode ser nulo", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void NaoDeve_SerVazio_Telefone()
        {
            Fornecedor fornecedor = new("nome teste", "", "email teste", "cidade teste", "ee");

            ValidadorFornecedor valfor = new();

            //action
            ValidationResult resultado = valfor.Validate(fornecedor);

            //assert
            Assert.AreEqual("'Telefone' não pode ser vazio", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Deve_SerFormatoCorreto_Email()
        {
            Fornecedor fornecedor = new("nome teste", "telefone teste", "email teste", "cidade teste", "et");

            ValidadorFornecedor valfor = new();

            //action
            ValidationResult resultado = valfor.Validate(fornecedor);

            //assert
            Assert.AreEqual("'E-mail' em formato incorreto", resultado.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Deve_TerSomenteDuasLetras_Estado()
        {
            Fornecedor fornecedor = new("nome teste", "999 9999", "emailteste@bol.ocm.br", "cidade teste", "estado teste");

            ValidadorFornecedor valfor = new();

            //action
            ValidationResult resultado = valfor.Validate(fornecedor);

            //assert
            Assert.AreEqual("'Estado' somente duas letras", resultado.Errors[0].ErrorMessage);
        }

    }
}
