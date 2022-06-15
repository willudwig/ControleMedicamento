

using ControleMedicamentos.Dominio.Compartilhado;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloFuncionario
{
    public class RepositorioFuncionarioEmBancoDados : ConexaoBancoDados<Funcionario>, IRepositorio<Funcionario>
    {
        public ValidationResult Inserir(Funcionario entidade)
        {
            ValidationResult resultado = Validar(entidade);

            if (resultado.IsValid)
                InserirRegistroBancoDados(entidade);

            return resultado;
        }

        public ValidationResult Editar(Funcionario entidade)
        {
            ValidationResult resultado = Validar(entidade);

            if (resultado.IsValid)
                EditarRegistroBancoDados(entidade);

            return resultado;
        }

        public ValidationResult Excluir(Funcionario entidade)
        {
            ValidationResult resultado = Validar(entidade);

            if (resultado.IsValid)
                ExcluirRegistroBancoDados(entidade);

            return resultado;
        }

        public List<Funcionario> SelecionarTodos()
        {
            ConectarBancoDados();

            sql = @"SELECT * FROM TBFUNCIONARIO";

            SqlCommand cmd_Selecao = new(sql, conexao);

            SqlDataReader leitor = cmd_Selecao.ExecuteReader();

            List<Funcionario> funcionarios = LerTodos(leitor);

            DesconectarBancoDados();

            return funcionarios;
        }

        public Funcionario SelecionarUnico(int numero)
        {
            ConectarBancoDados();

            sql = @"SELECT * FROM TBFUNCIONARIO WHERE ID = @ID";

            SqlCommand cmdSelecao = new(sql, conexao);

            cmdSelecao.Parameters.AddWithValue("ID", numero);

            SqlDataReader leitor = cmdSelecao.ExecuteReader();

            Funcionario selecionado = LerUnico(leitor);

            DesconectarBancoDados();

            return selecionado;
        }

        public override void Formatar()
        {
            ConectarBancoDados();

            sql = @"DELETE FROM TBFUNCIONARIO;
                    DBCC CHECKIDENT (TBFUNCIONARIO, RESEED, 0);";

            SqlCommand cmd_Formatacao = new(sql, conexao);

            cmd_Formatacao.ExecuteNonQuery();

            DesconectarBancoDados();
        }


        #region metodos protected

        protected override void DefinirParametros(Funcionario entidade, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("NOME", entidade.Nome);
            cmd.Parameters.AddWithValue("LOGIN", entidade.Login);
            cmd.Parameters.AddWithValue("SENHA", entidade.Senha);
        }

        protected override void DefinirParametros(Funcionario entidade, SqlCommand cmd, int entidadeId)
        {
            cmd.Parameters.AddWithValue("NOME", entidade.Nome);
            cmd.Parameters.AddWithValue("LOGIN", entidade.Login);
            cmd.Parameters.AddWithValue("SENHA", entidade.Senha);
            cmd.Parameters.AddWithValue("ID", entidade.Id);
        }

        protected override void EditarRegistroBancoDados(Funcionario entidade)
        {
            ConectarBancoDados();

            sql = @"UPDATE [TBFUNCIONARIO] SET 

                        [NOME] = @NOME,    
	                    [LOGIN] = @LOGIN,
                        [SENHA] = @SENHA  

                   WHERE
		                 ID = @ID;";

            SqlCommand cmd_Edicao = new(sql, conexao);

            DefinirParametros(entidade, cmd_Edicao, entidade.Id);

            cmd_Edicao.ExecuteNonQuery();

            DesconectarBancoDados();
        }

        protected override void ExcluirRegistroBancoDados(Funcionario entidade)
        {
            ConectarBancoDados();

            sql = @"DELETE FROM TBFUNCIONARIO WHERE ID = @ID;";

            SqlCommand cmd_Exclusao = new(sql, conexao);

            cmd_Exclusao.Parameters.AddWithValue("ID", entidade.Id);

            cmd_Exclusao.ExecuteNonQuery();

            DesconectarBancoDados();
        }

        protected override void InserirRegistroBancoDados(Funcionario entidade)
        {
            ConectarBancoDados();

            sql = @"INSERT INTO TBFUNCIONARIO 
                           (
                                [NOME],    
                                [LOGIN],
                                [SENHA]   
                           )
                           VALUES
                           (
                                @NOME,
                                @LOGIN,
                                @SENHA
                           )";

            SqlCommand cmd_Insercao = new(sql, conexao);

            DefinirParametros(entidade, cmd_Insercao);

            cmd_Insercao.ExecuteNonQuery();

            //entidade.Id = Convert.ToInt32(cmd_Insercao.ExecuteScalar());

            DesconectarBancoDados();
        }

        protected override List<Funcionario> LerTodos(SqlDataReader leitor)
        {
            List<Funcionario> funcionarios = new();

            while (leitor.Read())
            {
                int id = Convert.ToInt32(leitor["ID"]);
                string nome = leitor["NOME"].ToString();
                string login = leitor["LOGIN"].ToString();
                string senha = leitor["SENHA"].ToString();

                Funcionario funcionario = new Funcionario(nome, login, senha)
                {
                    Id = id
                };

                funcionarios.Add(funcionario);
            }

            return funcionarios;
        }

        protected override Funcionario LerUnico(SqlDataReader leitor)
        {
            Funcionario funcionario = null;

            if (leitor.Read())
            {
                int id = Convert.ToInt32(leitor["ID"]);
                string nome = leitor["NOME"].ToString();
                string login = leitor["LOGIN"].ToString();
                string senha = leitor["SENHA"].ToString();

                funcionario = new Funcionario(nome, login, senha)
                {
                    Id = id
                };
            }

            return funcionario;
        }

        protected override ValidationResult Validar(Funcionario entidade)
        {
            return new ValidadorFuncionario().Validate(entidade);
        }

        protected override bool VerificarDuplicidade(string novoTexto)
        {
            var todos = SelecionarTodos();

            if (todos.Count != 0)
                return todos.Exists(x => x.Equals(novoTexto));

            return false;
        }

        #endregion
    }
}
