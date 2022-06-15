using ControleMedicamentos.Dominio.Compartilhado;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
 
namespace ControleMedicamentos.Infra.BancoDados.ModuloFornecedor
{
    public class RepositorioFornecedorEmBancoDados : ConexaoBancoDados<Fornecedor>, IRepositorio<Fornecedor>
    {
        public ValidationResult Inserir(Fornecedor entidade)
        {
            ValidationResult resultado = Validar(entidade);

            if (resultado.IsValid)
                InserirRegistroBancoDados(entidade);

            return resultado;
        }

        public ValidationResult Editar(Fornecedor entidade)
        {
            ValidationResult resultado = Validar(entidade);

            if (resultado.IsValid)
                EditarRegistroBancoDados(entidade);

            return resultado;
        }

        public ValidationResult Excluir(Fornecedor entidade)
        {
            ValidationResult resultado = Validar(entidade);

            if (resultado.IsValid)
                ExcluirRegistroBancoDados(entidade);

            return resultado;
        }

        public List<Fornecedor> SelecionarTodos()
        {
            ConectarBancoDados();

            sql = @"SELECT * FROM TBFORNECEDOR";

            SqlCommand cmd_Selecao = new(sql, conexao);

            SqlDataReader leitor = cmd_Selecao.ExecuteReader();

            List<Fornecedor> fornecedores = LerTodos(leitor);

            DesconectarBancoDados();

            return fornecedores;
        }

        public Fornecedor SelecionarUnico(int numero)
        {
            ConectarBancoDados();

            sql = @"SELECT * FROM TBFORNECEDOR WHERE ID = @ID";

            SqlCommand cmdSelecao = new(sql, conexao);

            cmdSelecao.Parameters.AddWithValue("ID", numero);

            SqlDataReader leitor = cmdSelecao.ExecuteReader();

            Fornecedor selecionado = LerUnico(leitor);

            DesconectarBancoDados();

            return selecionado;
        }

        public override void Formatar()
        {
            ConectarBancoDados();

            sql = @"DELETE FROM TBFORNECEDOR;
                    DBCC CHECKIDENT (TBFORNECEDOR, RESEED, 0);";

            SqlCommand cmd_Formatacao = new(sql, conexao);

            cmd_Formatacao.ExecuteNonQuery();

            DesconectarBancoDados();
        }

        #region metodos protected

        protected override void InserirRegistroBancoDados(Fornecedor entidade)
        {
            ConectarBancoDados();

            sql = @"INSERT INTO TBFORNECEDOR 
                           (
                                [NOME],    
                                [TELEFONE],
                                [EMAIL],   
                                [CIDADE],  
                                [ESTADO]  
                           )
                           VALUES
                           (
                                @NOME,
                                @TELEFONE,
                                @EMAIL,
                                @CIDADE,
                                @ESTADO
                           )";

            SqlCommand cmd_Insercao = new(sql, conexao);

            DefinirParametros(entidade, cmd_Insercao);

            cmd_Insercao.ExecuteNonQuery();

            //entidade.Id = Convert.ToInt32(cmd_Insercao.ExecuteScalar());

            DesconectarBancoDados();
        }
        protected override void EditarRegistroBancoDados(Fornecedor entidade)
        {
            ConectarBancoDados();

            sql = @"UPDATE [TBFORNECEDOR] SET 

                        [NOME] = @NOME,    
	                    [TELEFONE] = @TELEFONE,
                        [EMAIL] = @EMAIL,   
                        [CIDADE] = @CIDADE,  
		                [ESTADO] = @ESTADO 

                   WHERE
		                 ID = @ID;";

            SqlCommand cmd_Edicao = new(sql, conexao);

            DefinirParametros(entidade, cmd_Edicao, entidade.Id);

            cmd_Edicao.ExecuteNonQuery();

            DesconectarBancoDados();
        }

        protected override void ExcluirRegistroBancoDados(Fornecedor entidade)
        {
            ConectarBancoDados();

            sql = @"DELETE FROM TBFORNECEDOR WHERE ID = @ID;";

            SqlCommand cmd_Exclusao = new(sql, conexao);

            cmd_Exclusao.Parameters.AddWithValue("ID", entidade.Id);

            cmd_Exclusao.ExecuteNonQuery();

            DesconectarBancoDados();
        }

        protected override void DefinirParametros(Fornecedor entidade, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("NOME", entidade.Nome);
            cmd.Parameters.AddWithValue("CIDADE", entidade.Cidade);
            cmd.Parameters.AddWithValue("ESTADO", entidade.Estado);
            cmd.Parameters.AddWithValue("EMAIL", entidade.Email);
            cmd.Parameters.AddWithValue("TELEFONE", entidade.Telefone);
        }

        protected override void DefinirParametros(Fornecedor entidade, SqlCommand cmd, int entidadeId)
        {
            cmd.Parameters.AddWithValue("NOME", entidade.Nome);
            cmd.Parameters.AddWithValue("CIDADE", entidade.Cidade);
            cmd.Parameters.AddWithValue("ESTADO", entidade.Estado);
            cmd.Parameters.AddWithValue("EMAIL", entidade.Email);
            cmd.Parameters.AddWithValue("TELEFONE", entidade.Telefone);
            cmd.Parameters.AddWithValue("ID", entidadeId);
        }

        protected override ValidationResult Validar(Fornecedor entidade)
        {
            return new ValidadorFornecedor().Validate(entidade);
        }

        protected override List<Fornecedor> LerTodos(SqlDataReader leitor)
        {
            List<Fornecedor> fornecedores = new();

            while (leitor.Read())
            {
                int id = Convert.ToInt32(leitor["ID"]);
                string nome = leitor["NOME"].ToString();
                string cidade = leitor["CIDADE"].ToString();
                string estado = leitor["ESTADO"].ToString();
                string email = leitor["EMAIL"].ToString();
                string telefone = leitor["TELEFONE"].ToString();

                Fornecedor fornecedor = new Fornecedor(nome, telefone, email, cidade, estado)
                {
                    Id = id
                };

                fornecedores.Add(fornecedor);
            }

            return fornecedores;
        }

        protected override Fornecedor LerUnico(SqlDataReader leitor)
        {
           Fornecedor fornecedor = null;

           if (leitor.Read())
            {
                int id = Convert.ToInt32(leitor["ID"]);
                string nome = leitor["NOME"].ToString();
                string cidade = leitor["CIDADE"].ToString();
                string estado = leitor["ESTADO"].ToString();
                string email = leitor["EMAIL"].ToString();
                string telefone = leitor["TELEFONE"].ToString();

                fornecedor = new Fornecedor(nome, telefone, email, cidade, estado)
                {
                    Id = id
                };
            }

            return fornecedor;
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
