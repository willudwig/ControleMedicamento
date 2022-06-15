using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ControleMedicamento.Infra.BancoDados.Tests.ModuloMedicamento
{
    [TestClass]
    public class RepositorioMedicamentoEmBancoDadosTest
    {

        RepositorioMedicamentoEmBancoDados repoMed;
        RepositorioFornecedorEmBancoDados repoFornec;
        int count;
        List<Medicamento> meds = new();

        public RepositorioMedicamentoEmBancoDadosTest()
        {
            repoMed = new();
            repoFornec = new();
            ResetarBancoDadosMedic();
            ResetarBancoDadosFornec();
            count = 0;
        }

        [TestMethod]
        public void Deve_Inserir_medicamento()
        {
            //arrange
            var novoMed = InstanciarMedicamento();

            repoFornec.Inserir(novoMed.Fornecedor);

            novoMed.Fornecedor.Id = 1;

            //action
            var acao = repoMed.Inserir(novoMed);


            //assert
            var medSelecionado = repoMed.SelecionarUnico(novoMed.Id);

            Assert.IsNotNull(medSelecionado);
            Assert.AreEqual(novoMed.Id, medSelecionado.Id);
            Assert.AreEqual(novoMed.Nome, medSelecionado.Nome);
            Assert.AreEqual(novoMed.Descricao, medSelecionado.Descricao);
            Assert.AreEqual(novoMed.Lote, medSelecionado.Lote);
            Assert.AreEqual(novoMed.Validade.ToShortDateString(), medSelecionado.Validade.ToShortDateString());

            Assert.AreEqual(novoMed.Fornecedor.Nome    , medSelecionado.Fornecedor.Nome    );
            Assert.AreEqual(novoMed.Fornecedor.Cidade  , medSelecionado.Fornecedor.Cidade  );
            Assert.AreEqual(novoMed.Fornecedor.Id      , medSelecionado.Fornecedor.Id      );
            Assert.AreEqual(novoMed.Fornecedor.Email   , medSelecionado.Fornecedor.Email   );
            Assert.AreEqual(novoMed.Fornecedor.Estado  , medSelecionado.Fornecedor.Estado  );
            Assert.AreEqual(novoMed.Fornecedor.Telefone, medSelecionado.Fornecedor.Telefone);
        }

        [TestMethod]
        public void Deve_Editar_medicamento()
        {
            //arrange
            var novoMed = InstanciarMedicamento();

            repoFornec.Inserir(novoMed.Fornecedor);

            novoMed.Fornecedor.Id = 1;

            repoMed.Inserir(novoMed);

            novoMed.Nome = "nome editado pelo teste";
            novoMed.QuantidadeDisponivel = 215;

            //action
            var acao = repoMed.Editar(novoMed);


            //assert
            var medSelecionado = repoMed.SelecionarUnico(novoMed.Id);

            Assert.IsNotNull(medSelecionado);
            Assert.AreEqual(novoMed.Id, medSelecionado.Id);
            Assert.AreEqual(novoMed.Nome, medSelecionado.Nome);
            Assert.AreEqual(novoMed.Descricao, medSelecionado.Descricao);
            Assert.AreEqual(novoMed.Lote, medSelecionado.Lote);
            Assert.AreEqual(novoMed.Validade.ToShortDateString(), medSelecionado.Validade.ToShortDateString());
        }

        [TestMethod]
        public void Deve_Excluir_medicamento()
        {
            //arrange
            var novoMed = InstanciarMedicamento();

            repoFornec.Inserir(novoMed.Fornecedor);

            novoMed.Fornecedor.Id = 1;

            repoMed.Inserir(novoMed);

            //action
            var acao = repoMed.Excluir(novoMed);

            //assert
            var medSelecionado = repoMed.SelecionarUnico(novoMed.Id);

            Assert.IsNull(medSelecionado);
        }

        [TestMethod]
        public void Deve_SelecionarTodos_medicamento()
        {
            //arrange
            var medic01 = InstanciarMedicamento();

            medic01.Nome = "Sfsdfs Asdfsd";
            medic01.Descricao = "djksfhslkdfhskejfhkwe";

            repoFornec.Inserir(medic01.Fornecedor);

            medic01.Fornecedor.Id = 1;

            repoMed.Inserir(medic01);


            //------------------------------------

            var medic02 = InstanciarMedicamento();

            medic02.Id = 2;

            medic02.Fornecedor.Id = 1;

            repoMed.Inserir(medic02);



            //action
            var resultado = repoMed.SelecionarTodos();


            //assert 
            Assert.AreEqual(2, resultado.Count);
        }

        [TestMethod]
        public void Deve_SelecionarUnico_medicamento()
        {
            //arrange
            var medic01 = InstanciarMedicamento();

            medic01.Nome = "Sfsdfs Asdfsd";
            medic01.Descricao = "djksfhslkdfhskejfhkwe";

            repoFornec.Inserir(medic01.Fornecedor);

            medic01.Fornecedor.Id = 1;

            repoMed.Inserir(medic01);


            //------------------------------------

            var medic02 = InstanciarMedicamento();

            medic02.Id = 2;

            medic02.Fornecedor.Id = 1;

            repoMed.Inserir(medic02);


            //action
            var resultado = repoMed.SelecionarUnico(medic02.Id);


            //assert 
            Assert.IsNotNull(resultado);
            Assert.AreEqual(2, resultado.Id);
        }

        [TestMethod]
        public void Deve_ObterRemediosMaisRequisitados()
        {
            //arrange

            #region 6 novos medicamentos

            Medicamento medicamento1 = new("nome teste 01", "descrição teste", "sgdgsdg4t43453", DateTime.Now);
            medicamento1.QuantidadeDisponivel = 55;
            medicamento1.Fornecedor = new("nome teste", "999 224455", "teste@gmail.com", "ssdgsdg", "DD");

            repoFornec.Inserir(medicamento1.Fornecedor);

            medicamento1.Fornecedor.Id = 1;
            medicamento1.Id = 0;

            Medicamento medicamento2 = new("nome teste 02", "descrição teste", "sgdgsdg4t43453", DateTime.Now);
            medicamento2.QuantidadeDisponivel = 55;
            medicamento2.Fornecedor = new("sdsdf", "sdgsgsd", "sdgsdg", "ssdgsdg", "sdgsdgsd");
            medicamento2.Fornecedor.Id = 1;
            medicamento2.Id = 0;

            Medicamento medicamento3 = new("nome teste 03", "descrição teste", "sgdgsdg4t43453", DateTime.Now);
            medicamento3.QuantidadeDisponivel = 55;
            medicamento3.Fornecedor = new("sdsdf", "sdgsgsd", "sdgsdg", "ssdgsdg", "sdgsdgsd");
            medicamento3.Fornecedor.Id = 1;
            medicamento3.Id = 0;

            Medicamento medicamento4 = new("nome teste 04", "descrição teste", "sgdgsdg4t43453", DateTime.Now);
            medicamento4.QuantidadeDisponivel = 55;
            medicamento4.Fornecedor = new("sdsdf", "sdgsgsd", "sdgsdg", "ssdgsdg", "sdgsdgsd");
            medicamento4.Fornecedor.Id = 1;
            medicamento4.Id = 0;

            Medicamento medicamento5 = new("nome teste 05", "descrição teste", "sgdgsdg4t43453", DateTime.Now);
            medicamento5.QuantidadeDisponivel = 55;
            medicamento5.Fornecedor = new("sdsdf", "sdgsgsd", "sdgsdg", "ssdgsdg", "sdgsdgsd");
            medicamento5.Fornecedor.Id = 1;
            medicamento5.Id = 0;

            Medicamento medicamento6 = new("nome teste 06", "descrição teste", "sgdgsdg4t43453", DateTime.Now);
            medicamento6.QuantidadeDisponivel = 55;
            medicamento6.Fornecedor = new("sdsdf", "sdgsgsd", "sdgsdg", "ssdgsdg", "sdgsdgsd");
            medicamento6.Fornecedor.Id = 1;
            medicamento6.Id = 0;

            #endregion

            meds.Add(medicamento1);
            meds.Add(medicamento2);
            meds.Add(medicamento3);
            meds.Add(medicamento4);
            meds.Add(medicamento5);
            meds.Add(medicamento6);

            foreach (Medicamento m in meds)
            {
                repoMed.Inserir(m);
            }

            //action
            var resultado = repoMed.ObterRemediosMaisRequisitados(meds);

            //assert
            Assert.AreNotEqual(0, resultado.Count);
            Assert.AreNotEqual(1, resultado.Count);
        }

        [TestMethod]
        public void Deve_ObterRemediosBaixaQtd()
        {
            //arrange

            #region 6 novos medicamentos

            Medicamento medicamento1 = new("nome teste 01", "descrição teste", "sgdgsdg4t43453", DateTime.Now);
            medicamento1.QuantidadeDisponivel = 21;
            medicamento1.Fornecedor = new("nome teste", "999 224455", "teste@gmail.com", "ssdgsdg", "DD");

            repoFornec.Inserir(medicamento1.Fornecedor);

            medicamento1.Fornecedor.Id = 1;
            medicamento1.Id = 0;

            Medicamento medicamento2 = new("nome teste 02", "descrição teste", "sgdgsdg4t43453", DateTime.Now);
            medicamento2.QuantidadeDisponivel = 5;
            medicamento2.Fornecedor = new("sdsdf", "sdgsgsd", "sdgsdg", "ssdgsdg", "sdgsdgsd");
            medicamento2.Fornecedor.Id = 1;
            medicamento2.Id = 0;

            Medicamento medicamento3 = new("nome teste 03", "descrição teste", "sgdgsdg4t43453", DateTime.Now);
            medicamento3.QuantidadeDisponivel = 4;
            medicamento3.Fornecedor = new("sdsdf", "sdgsgsd", "sdgsdg", "ssdgsdg", "sdgsdgsd");
            medicamento3.Fornecedor.Id = 1;
            medicamento3.Id = 0;

            Medicamento medicamento4 = new("nome teste 04", "descrição teste", "sgdgsdg4t43453", DateTime.Now);
            medicamento4.QuantidadeDisponivel = 3;
            medicamento4.Fornecedor = new("sdsdf", "sdgsgsd", "sdgsdg", "ssdgsdg", "sdgsdgsd");
            medicamento4.Fornecedor.Id = 1;
            medicamento4.Id = 0;

            Medicamento medicamento5 = new("nome teste 05", "descrição teste", "sgdgsdg4t43453", DateTime.Now);
            medicamento5.QuantidadeDisponivel = 55;
            medicamento5.Fornecedor = new("sdsdf", "sdgsgsd", "sdgsdg", "ssdgsdg", "sdgsdgsd");
            medicamento5.Fornecedor.Id = 1;
            medicamento5.Id = 0;

            Medicamento medicamento6 = new("nome teste 06", "descrição teste", "sgdgsdg4t43453", DateTime.Now);
            medicamento6.QuantidadeDisponivel = 75;
            medicamento6.Fornecedor = new("sdsdf", "sdgsgsd", "sdgsdg", "ssdgsdg", "sdgsdgsd");
            medicamento6.Fornecedor.Id = 1;
            medicamento6.Id = 0;

            #endregion

            meds.Add(medicamento1);
            meds.Add(medicamento2);
            meds.Add(medicamento3);
            meds.Add(medicamento4);
            meds.Add(medicamento5);
            meds.Add(medicamento6);

            foreach (Medicamento m in meds)
            {
                repoMed.Inserir(m);
            }

            //action
            var resultado = repoMed.ObterRemediosBaixaQtd();

            //assert
            Assert.AreNotEqual(0, resultado.Count);
        }

        [TestMethod]
        public void Deve_ObterRemediosFalta()
        {
            //arrange

            #region 6 novos medicamentos

            Medicamento medicamento1 = new("nome teste 01", "descrição teste", "sgdgsdg4t43453", DateTime.Now);
            medicamento1.QuantidadeDisponivel = 1;
            medicamento1.Fornecedor = new("nome teste", "999 224455", "teste@gmail.com", "ssdgsdg", "DD");

            repoFornec.Inserir(medicamento1.Fornecedor);

            medicamento1.Fornecedor.Id = 1;
            medicamento1.Id = 0;

            Medicamento medicamento2 = new("nome teste 02", "descrição teste", "sgdgsdg4t43453", DateTime.Now);
            medicamento2.QuantidadeDisponivel = 0;
            medicamento2.Fornecedor = new("sdsdf", "sdgsgsd", "sdgsdg", "ssdgsdg", "sdgsdgsd");
            medicamento2.Fornecedor.Id = 1;
            medicamento2.Id = 0;

            Medicamento medicamento3 = new("nome teste 03", "descrição teste", "sgdgsdg4t43453", DateTime.Now);
            medicamento3.QuantidadeDisponivel = 10;
            medicamento3.Fornecedor = new("sdsdf", "sdgsgsd", "sdgsdg", "ssdgsdg", "sdgsdgsd");
            medicamento3.Fornecedor.Id = 1;
            medicamento3.Id = 0;

            Medicamento medicamento4 = new("nome teste 04", "descrição teste", "sgdgsdg4t43453", DateTime.Now);
            medicamento4.QuantidadeDisponivel = 3;
            medicamento4.Fornecedor = new("sdsdf", "sdgsgsd", "sdgsdg", "ssdgsdg", "sdgsdgsd");
            medicamento4.Fornecedor.Id = 1;
            medicamento4.Id = 0;

            Medicamento medicamento5 = new("nome teste 05", "descrição teste", "sgdgsdg4t43453", DateTime.Now);
            medicamento5.QuantidadeDisponivel = 55;
            medicamento5.Fornecedor = new("sdsdf", "sdgsgsd", "sdgsdg", "ssdgsdg", "sdgsdgsd");
            medicamento5.Fornecedor.Id = 1;
            medicamento5.Id = 0;

            Medicamento medicamento6 = new("nome teste 06", "descrição teste", "sgdgsdg4t43453", DateTime.Now);
            medicamento6.QuantidadeDisponivel = 75;
            medicamento6.Fornecedor = new("sdsdf", "sdgsgsd", "sdgsdg", "ssdgsdg", "sdgsdgsd");
            medicamento6.Fornecedor.Id = 1;
            medicamento6.Id = 0;

            #endregion

            meds.Add(medicamento1);
            meds.Add(medicamento2);
            meds.Add(medicamento3);
            meds.Add(medicamento4);
            meds.Add(medicamento5);
            meds.Add(medicamento6);

            foreach (Medicamento m in meds)
            {
                repoMed.Inserir(m);
            }

            //action
            var resultado = repoMed.ObterRemediosFalta();

            //assert
            Assert.AreNotEqual(0, resultado.Count);
        }


        #region privados

        private Medicamento InstanciarMedicamento()
        {
            count++;

            return new("nome medicamento projeto teste ", "descrição teste", "SDF#$%SDFws%67", DateTime.Now)
            {
                Id = count,
                QuantidadeDisponivel = 50,
                

                Fornecedor = new("nome fornecedor teste", "999 123456", "email@gmail.com", "Lages", "SC")
                {
                    Id = count
                }
            };
        }

        private void ResetarBancoDadosMedic()
        {
            if (repoMed != null)
                repoMed.Formatar();
        }

        private void ResetarBancoDadosFornec()
        {
            if (repoFornec != null)
                repoFornec.Formatar();
        }

        #endregion
    }
}
