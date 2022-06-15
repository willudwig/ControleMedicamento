using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFornecedor
{
    [TestClass]
    public class RepositorioFornecedorBancoDadosTests
    {
        RepositorioFornecedorEmBancoDados repoFornec;
        int count;

        public RepositorioFornecedorBancoDadosTests()
        {
            repoFornec = new();
            ResetarBancoDados();
            count = 0;
        }

        [TestMethod]
        public void Deve_Inserir_Fornecedor()
        {
            //arrange
            var novoFornec = InstanciarFornecedor();

            //action
            var acao = repoFornec.Inserir(novoFornec);

            //assert 
            var fornecSelecionado = repoFornec.SelecionarUnico(novoFornec.Id);

            Assert.IsNotNull(fornecSelecionado);
            Assert.AreEqual(novoFornec.Id, fornecSelecionado.Id);
            Assert.AreEqual(novoFornec.Nome, fornecSelecionado.Nome);
            Assert.AreEqual(novoFornec.Cidade, fornecSelecionado.Cidade);
            Assert.AreEqual(novoFornec.Estado, fornecSelecionado.Estado);
            Assert.AreEqual(novoFornec.Email, fornecSelecionado.Email);
            Assert.AreEqual(novoFornec.Telefone, fornecSelecionado.Telefone);
        }

        [TestMethod]
        public void Deve_Editar_Fornecedor()
        {
            //arrange
            var novoFornec = InstanciarFornecedor();

            repoFornec.Inserir(novoFornec);

            novoFornec.Nome = "Nome editado pelo teste";
            novoFornec.Cidade = "Cidade editada";


            //action
            var acao = repoFornec.Editar(novoFornec);


            //assert
            var fornecSelecionado = repoFornec.SelecionarUnico(novoFornec.Id);

            Assert.IsNotNull(fornecSelecionado);
            Assert.AreEqual(novoFornec.Nome, fornecSelecionado.Nome);
            Assert.AreEqual(novoFornec.Cidade, fornecSelecionado.Cidade);
        }

        [TestMethod]
        public void Deve_Excluir_Fornecedor()
        {
            //arrange
            var novoFornec = InstanciarFornecedor();

            novoFornec.Nome = "este deve ser excluido";

            repoFornec.Inserir(novoFornec);


            //action
            var acao = repoFornec.Excluir(novoFornec);


            //assert
            var fornecSelecionado = repoFornec.SelecionarUnico(novoFornec.Id);

            Assert.IsNull(fornecSelecionado);
        }

        [TestMethod]
        public void Deve_SelecionarTodos_Fornecedor()
        {
            //arrange
            var fornec01 = InstanciarFornecedor();

            fornec01.Nome = "Sfsdfs Asdfsd";

            repoFornec.Inserir(fornec01);

            //------------------------------------

            var fornec02 = InstanciarFornecedor();

            repoFornec.Inserir(fornec02);


            //action
            var resultado = repoFornec.SelecionarTodos();


            //assert 
            Assert.AreEqual(2, resultado.Count);
        }

        [TestMethod]
        public void Deve_SelecionarUnico_Fornecedor()
        {
            //arrange
            var fornec01 = InstanciarFornecedor();

            fornec01.Nome = "Sfsdfs Asdfsd";

            repoFornec.Inserir(fornec01);

            //------------------------------------

            var fornec02 = InstanciarFornecedor();

            repoFornec.Inserir(fornec02);


            //action
            var resultado = repoFornec.SelecionarUnico(fornec02.Id);


            //assert 
            Assert.IsNotNull(resultado);
            Assert.AreEqual(2, resultado.Id);
        }

        #region privados
        private Fornecedor InstanciarFornecedor()
        {
            count++;

            return new("nome do projeto teste ", "(49) 999 999 887766", "projetoteste@bol.com.br", "cidadela", "TE")
            {
                Id = count,

            };
        }
        private void ResetarBancoDados()
        {
            if(repoFornec != null)
                repoFornec.Formatar();
        }
        #endregion
    }
}
