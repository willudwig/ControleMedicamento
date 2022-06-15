using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloPaciente
{
    [TestClass]
    public  class RepositorioPacienteEmBancoDadosTest
    {
        RepositorioPacienteEmBancoDados repoPac;
        int count;

        public RepositorioPacienteEmBancoDadosTest()
        {
            repoPac = new();
            ResetarBancoDados();
            count = 0;
        }

        [TestMethod]
        public void Deve_Inserir_Paciente()
        {
            //arrange
            var novoPac = InstanciarPaciente();

            //action
            var acao = repoPac.Inserir(novoPac);

            //assert
            var pacSelecionado = repoPac.SelecionarUnico(novoPac.Id);

            Assert.IsNotNull(pacSelecionado);
            Assert.AreEqual(novoPac.Id       , pacSelecionado.Id);
            Assert.AreEqual(novoPac.Nome     , pacSelecionado.Nome);
            Assert.AreEqual(novoPac.CartaoSUS, pacSelecionado.CartaoSUS);
        }

        [TestMethod]
        public void Deve_Editar_Paciente()
        {
            //arrange
            var novoPac = InstanciarPaciente();

            repoPac.Inserir(novoPac);

            novoPac.Nome = "Nome editado pelo teste";
            novoPac.CartaoSUS = "789456123489794";


            //action
            var acao = repoPac.Editar(novoPac);


            //assert
            var pacSelecionado = repoPac.SelecionarUnico(novoPac.Id);

            Assert.IsNotNull(pacSelecionado);
            Assert.AreEqual(novoPac.Nome, pacSelecionado.Nome);
            Assert.AreEqual(novoPac.CartaoSUS, pacSelecionado.CartaoSUS);
        }

        [TestMethod]
        public void Deve_Excluir_Paciente()
        {
            //arrange
            var novoPac = InstanciarPaciente();

            novoPac.Nome = "este deve ser excluido";

            repoPac.Inserir(novoPac);


            //action
            var acao = repoPac.Excluir(novoPac);


            //assert
            var pacSelecionado = repoPac.SelecionarUnico(novoPac.Id);

            Assert.IsNull(pacSelecionado);
        }

        [TestMethod]
        public void Deve_SelecionarTodos_Paciente()
        {
            //arrange
            var fornec01 = InstanciarPaciente();

            fornec01.Nome = "Sfsdfs Asdfsd";
            fornec01.CartaoSUS = "456789456789458";

            repoPac.Inserir(fornec01);

            //------------------------------------

            var fornec02 = InstanciarPaciente();

            repoPac.Inserir(fornec02);


            //action
            var resultado = repoPac.SelecionarTodos();


            //assert 
            Assert.AreEqual(2, resultado.Count);
        }

        [TestMethod]
        public void Deve_SelecionarUnico_Paciente()
        {
            //arrange
            var fornec01 = InstanciarPaciente();

            fornec01.Nome = "Sfsdfs Asdfsd";
            fornec01.CartaoSUS = "045678945678950";

            repoPac.Inserir(fornec01);

            //------------------------------------

            var fornec02 = InstanciarPaciente();

            repoPac.Inserir(fornec02);


            //action
            var resultado = repoPac.SelecionarUnico(fornec02.Id);


            //assert 
            Assert.IsNotNull(resultado);
            Assert.AreEqual(2, resultado.Id);
        }

        #region privados
        private Paciente InstanciarPaciente()
        {
            count++;

            return new("nome do projeto teste ", "123456789101213")
            {
                Id = count,
            };
        }

        private void ResetarBancoDados()
        {
            if (repoPac != null)
                repoPac.Formatar();
        }
        #endregion
    }
}
