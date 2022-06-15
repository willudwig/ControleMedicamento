using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFuncionario
{
    [TestClass]
    public class RepositorioFuncionarioEmBancoDadosTests
    {
        RepositorioFuncionarioEmBancoDados repoFunc;
        int count;

        public RepositorioFuncionarioEmBancoDadosTests()
        {
            repoFunc = new();
            ResetarBancoDados();
            count = 0;
        }

        [TestMethod]
        public void Deve_Inserir_Funcionario()
        {
            //arrange
            var novoFunc = InstanciarFuncionario();



            //action
            var acao = repoFunc.Inserir(novoFunc);



            //assert
            var funcSelecionado = repoFunc.SelecionarUnico(novoFunc.Id);

            Assert.IsNotNull(funcSelecionado);
            Assert.AreEqual(novoFunc.Id, funcSelecionado.Id);
            Assert.AreEqual(novoFunc.Nome, funcSelecionado.Nome);
            Assert.AreEqual(novoFunc.Login, funcSelecionado.Login);
            Assert.AreEqual(novoFunc.Senha, funcSelecionado.Senha);
        }

        [TestMethod]
        public void Deve_Editar_Funcionario()
        {
            //arrange
            var novoFunc = InstanciarFuncionario();

            repoFunc.Inserir(novoFunc);

            novoFunc.Nome = "Nome editado pelo teste";
            novoFunc.Login = "Login editado";


            //action
            var acao = repoFunc.Editar(novoFunc);


            //assert
            var funcSelecionado = repoFunc.SelecionarUnico(novoFunc.Id);

            Assert.IsNotNull(funcSelecionado);
            Assert.AreEqual(novoFunc.Nome, funcSelecionado.Nome);
            Assert.AreEqual(novoFunc.Login, funcSelecionado.Login);
        }

        [TestMethod]
        public void Deve_Excluir_Funcionario()
        {
            //arrange
            var novoFunc = InstanciarFuncionario();

            novoFunc.Nome = "este deve ser excluido";

            repoFunc.Inserir(novoFunc);


            //action
            var acao = repoFunc.Excluir(novoFunc);


            //assert
            var funcSelecionado = repoFunc.SelecionarUnico(novoFunc.Id);

            Assert.IsNull(funcSelecionado);
        }

        [TestMethod]
        public void Deve_SelecionarTodos_Funcionario()
        {
            //arrange
            var fornec01 = InstanciarFuncionario();

            fornec01.Nome = "Sfsdfs Asdfsd";

            repoFunc.Inserir(fornec01);

            //------------------------------------

            var fornec02 = InstanciarFuncionario();

            repoFunc.Inserir(fornec02);


            //action
            var resultado = repoFunc.SelecionarTodos();


            //assert 
            Assert.AreEqual(2, resultado.Count);
        }

        [TestMethod]
        public void Deve_SelecionarUnico_Funcionario()
        {
            //arrange
            var fornec01 = InstanciarFuncionario();

            fornec01.Nome = "Sfsdfs Asdfsd";

            repoFunc.Inserir(fornec01);

            //------------------------------------

            var fornec02 = InstanciarFuncionario();

            repoFunc.Inserir(fornec02);


            //action
            var resultado = repoFunc.SelecionarUnico(fornec02.Id);


            //assert 
            Assert.IsNotNull(resultado);
            Assert.AreEqual(2, resultado.Id);
        }

        #region privados

        private Funcionario InstanciarFuncionario()
        {
            count++;

            return new("nome do projeto teste ", "nomelogin", "#e4rws%67")
            {
                Id = count,
            };
        }

        private void ResetarBancoDados()
        {
            if (repoFunc != null)
                repoFunc.Formatar();
        }

        #endregion
    }
}
