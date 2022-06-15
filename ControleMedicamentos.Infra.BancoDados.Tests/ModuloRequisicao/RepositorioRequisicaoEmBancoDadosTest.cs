using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.ModuloRequisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloRequisicao
{
    [TestClass]
    public class RepositorioRequisicaoEmBancoDadosTest
    {
        RepositorioRequisicaoEmBancoDados repoReq;
        RepositorioFuncionarioEmBancoDados repoFunc;
        RepositorioPacienteEmBancoDados repoPac;
        RepositorioFornecedorEmBancoDados repoFornec;
        RepositorioMedicamentoEmBancoDados repoMed;

        public RepositorioRequisicaoEmBancoDadosTest()
        {
            repoReq = new();
            repoFunc = new();
            repoPac = new();
            repoFornec = new();
            repoMed = new();

            ResetarBancoDadosReq();
            ResetarBancoDadosFuncionario();
            ResetarBancoDadosPaciente();
            ResetarBancoDadosMedicamento();
            ResetarBancoDadosFornecedor();
        }

        [TestMethod]
        public void Deve_Inserir_requisicao()
        {
            //arrange
            var novaReq = InstanciarRequisicao();

            repoFornec.Inserir(novaReq.Medicamento.Fornecedor);
            repoMed.Inserir(novaReq.Medicamento);
            repoFunc.Inserir(novaReq.Funcionario);
            repoPac.Inserir(novaReq.Paciente);

            //action
            var acao = repoReq.Inserir(novaReq);

            //assert
            var reqSelecionada = repoReq.SelecionarUnico(novaReq.Id);

            Assert.IsNotNull(reqSelecionada);
        }

        [TestMethod]
        public void Deve_Editar_requisicao()
        {
            //arrange
            var novaReq = InstanciarRequisicao();

            repoFornec.Inserir(novaReq.Medicamento.Fornecedor);
            repoMed.Inserir(novaReq.Medicamento);
            repoFunc.Inserir(novaReq.Funcionario);
            repoPac.Inserir(novaReq.Paciente);
            repoReq.Inserir(novaReq);

            novaReq.QtdMedicamento = 2000;

            //action
            var acao = repoReq.Editar(novaReq);

            //assert
            var reqSelecionada = repoReq.SelecionarUnico(novaReq.Id);

            Assert.AreEqual(2000, reqSelecionada.QtdMedicamento);
        }

        [TestMethod]
        public void Deve_Excluir_requisicao()
        {
            //arrange
            var novaReq = InstanciarRequisicao();

            repoFornec.Inserir(novaReq.Medicamento.Fornecedor);
            repoMed.Inserir(novaReq.Medicamento);
            repoFunc.Inserir(novaReq.Funcionario);
            repoPac.Inserir(novaReq.Paciente);
            repoReq.Inserir(novaReq);

            //action
            var acao = repoReq.Excluir(novaReq);

            //assert
            var reqSelecionada = repoReq.SelecionarTodos();

            Assert.AreEqual(0, reqSelecionada.Count);
        }

        [TestMethod]
        public void Deve_SelecionarTodos_requisicao()
        {
            //arrange
            var novaReq = InstanciarRequisicao();

            repoFornec.Inserir(novaReq.Medicamento.Fornecedor);
            repoMed.Inserir(novaReq.Medicamento);
            repoFunc.Inserir(novaReq.Funcionario);
            repoPac.Inserir(novaReq.Paciente);
            repoReq.Inserir(novaReq);

            //action
            var resultado = repoReq.SelecionarTodos();

            //assert
            Assert.IsNotNull(resultado);
            Assert.AreNotEqual(0, resultado.Count);
        }

        [TestMethod]
        public void Deve_SelecionarUnico_requisicao()
        {
            //arrange
            var novaReq = InstanciarRequisicao();

            repoFornec.Inserir(novaReq.Medicamento.Fornecedor);
            repoMed.Inserir(novaReq.Medicamento);
            repoFunc.Inserir(novaReq.Funcionario);
            repoPac.Inserir(novaReq.Paciente);
            repoReq.Inserir(novaReq);

            //action
            var resultado = repoReq.SelecionarUnico(novaReq.Id);

            //assert
            Assert.IsNotNull(resultado);
        }


        #region privados

        private Requisicao InstanciarRequisicao()
        {
            return new Requisicao()
            {
                Medicamento = InstanciarMedicamento(),
                Paciente = InstanciarPaciente(),
                Funcionario = InstanciarFuncionario(),
                QtdMedicamento = 100,
                Data = DateTime.Now,
                Id = 1
                
            };
        }

        private Medicamento InstanciarMedicamento()
        {
            return new("nome medicamento projeto teste ", "descrição teste", "SDF#$%SDFws%67", DateTime.Now)
            {
                QuantidadeDisponivel = 50,

                Id = 1,

                Fornecedor = new("nome fornecedor teste", "999 123456", "email@gmail.com", "Lages", "SC")
                {
                    Id = 1
                }

            };
        }

        private Funcionario InstanciarFuncionario()
        {
            return new("nome do projeto teste ", "nomelogin", "#e4rws%67")
            {
                Id = 1
            };

        }

        private Paciente InstanciarPaciente()
        {
            return new("nome do projeto teste ", "123456789101213")
            {
                Id = 1
            };

        }

        private void ResetarBancoDadosReq()
        {
            if (repoReq != null)
                repoReq.Formatar();
        }

        private void ResetarBancoDadosFuncionario()
        {
            if (repoFunc != null)
                repoFunc.Formatar();
        }

        private void ResetarBancoDadosPaciente()
        {
            if (repoPac != null)
                repoPac.Formatar();
        }

        private void ResetarBancoDadosFornecedor()
        {
            if (repoFornec != null)
                repoFornec.Formatar();
        }

        private void ResetarBancoDadosMedicamento()
        {
            if (repoMed != null)
                repoMed.Formatar();
        }

        #endregion
    }
}

