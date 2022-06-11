

using ControleMedicamentos.Dominio;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.Compartilhado
{
    public abstract class ConexaoBancoDados<T> where T : EntidadeBase<T>
    {
        public SqlConnection conexao;
        public string sql;

        public void ConectarBancoDados()
        {
            conexao = new();

            conexao.ConnectionString = @"Data Source=(localDB)\MSSqlLocalDB;Initial Catalog=ControleMedicamentoDB;Integrated Security=True";

            conexao.Open();
        }

        public void DesconectarBancoDados()
        {
            conexao.Close();
        }

        #region metodos abstratos

        protected abstract void InserirRegistroBancoDados(T entidade);
        protected abstract void EditarRegistroBancoDados(T entidade);
        protected abstract void ExcluirRegistroBancoDados(T entidade);
        protected abstract void DefinirParametros(T entidade, SqlCommand cmd_Insercao);
        protected abstract void DefinirParametros(T entidade, SqlCommand cmd_Insercao, int entidadeId);
        protected abstract ValidationResult Validar(T entidade);
        protected abstract List<T> LerTodos(SqlDataReader leitor);
        protected abstract T LerUnico(SqlDataReader leitor);

        #endregion

    }
}
