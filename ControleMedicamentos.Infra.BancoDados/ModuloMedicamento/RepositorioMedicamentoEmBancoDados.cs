using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamento.Infra.BancoDados.ModuloMedicamento
{
    public class RepositorioMedicamentoEmBancoDados : ConexaoBancoDados<Medicamento>, IRepositorio<Medicamento>
    {
        RepositorioFornecedorEmBancoDados repoFornecedor;

        public ValidationResult Inserir(Medicamento entidade)
        {
            ValidationResult resultado = Validar(entidade);

            if (resultado.IsValid)
                InserirRegistroBancoDados(entidade);

            return resultado;
        }

        public ValidationResult Editar(Medicamento entidade)
        {
            ValidationResult resultado = Validar(entidade);

            if (resultado.IsValid)
                EditarRegistroBancoDados(entidade);

            return resultado;
        }

        public ValidationResult Excluir(Medicamento entidade)
        {
            ValidationResult resultado = Validar(entidade);

            if (resultado.IsValid)
                ExcluirRegistroBancoDados(entidade);

            return resultado;
        }

        public List<Medicamento> SelecionarTodos()
        {
            ConectarBancoDados();

            sql = @"SELECT  
                        
                        M.[ID],
                        M.[NOME], 
                        M.[DESCRICAO], 
                        M.[LOTE], 
                        M.[VALIDADE],
                        M.[QUANTIDADEDISPONIVEL],
                        M.[FORNECEDOR_ID],

                    FROM TBMEDICAMENTO AS M
                    INNER JOIN TBFORNECEDOR AS F

                        ON M.FORNECEDOR_ID = F.ID";

            SqlCommand cmd_Selecao = new(sql, conexao);

            SqlDataReader leitor = cmd_Selecao.ExecuteReader();

            List<Medicamento> medicamentos = LerTodos(leitor);

            DesconectarBancoDados();

            LerFornecedores(medicamentos);

            return medicamentos;
        }

        public Medicamento SelecionarUnico(int numero)
        {
            ConectarBancoDados();

            sql = @"SELECT  

                        M.[ID],
                        M.[NOME], 
                        M.[DESCRICAO], 
                        M.[LOTE], 
                        M.[VALIDADE],
                        M.[QUANTIDADEDISPONIVEL],
                        M.[FORNECEDOR_ID],

                    FROM TBMEDICAMENTO AS M
                    INNER JOIN TBFORNECEDOR AS F

                        ON M.FORNECEDOR_ID = F.ID
                        WHERE ID = @ID";

            SqlCommand cmdSelecao = new(sql, conexao);

            cmdSelecao.Parameters.AddWithValue("ID", numero);

            SqlDataReader leitor = cmdSelecao.ExecuteReader();

            Medicamento selecionado = LerUnico(leitor);

            DesconectarBancoDados();

            selecionado.Fornecedor = LerFornecedor(selecionado.Id);

            return selecionado;
        }

        #region metodos protected
        protected override void DefinirParametros(Medicamento entidade, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("NOME_MEDICAMENTO", entidade.Nome);
            cmd.Parameters.AddWithValue("DESCRICAO", entidade.Descricao);
            cmd.Parameters.AddWithValue("LOTE", entidade.Lote);
            cmd.Parameters.AddWithValue("VALIDADE", entidade.Validade);
            cmd.Parameters.AddWithValue("QTD", entidade.QuantidadeDisponivel);
            cmd.Parameters.AddWithValue("FORNECEDOR_ID", entidade.Fornecedor.Id);
        }

        protected override void DefinirParametros(Medicamento entidade, SqlCommand cmd, int entidadeId)
        {
            cmd.Parameters.AddWithValue("NOME_MEDICAMENTO", entidade.Nome);
            cmd.Parameters.AddWithValue("DESCRICAO", entidade.Descricao);
            cmd.Parameters.AddWithValue("LOTE", entidade.Lote);
            cmd.Parameters.AddWithValue("VALIDADE", entidade.Validade);
            cmd.Parameters.AddWithValue("QTD", entidade.QuantidadeDisponivel);
            cmd.Parameters.AddWithValue("FORNECEDOR_ID", entidade.Fornecedor.Id);
            cmd.Parameters.AddWithValue("ID", entidade.Id);
        }

        protected override void EditarRegistroBancoDados(Medicamento entidade)
        {
            ConectarBancoDados();

            sql = @"UPDATE [TBMEDICAMENTO] SET 

                        [NOME] = @NOME,    
                        [DESCRICAO] @DESCRICAO,
                        [LOTE] = @LOTE,
                        [VALIDADE] = @VALIDADE,
                        [QUANTIDADEDISPONIVEL] = @QTD,
                        [FORNECEDOR_ID] = @FORNECEDOR_ID

                   WHERE
		                 ID = @ID;";

            SqlCommand cmd_Edicao = new(sql, conexao);

            DefinirParametros(entidade, cmd_Edicao, entidade.Id);

            cmd_Edicao.ExecuteNonQuery();

            DesconectarBancoDados();
        }

        protected override void ExcluirRegistroBancoDados(Medicamento entidade)
        {
            ConectarBancoDados();

            sql = @"DELETE FROM TBMEDICAMENTO WHERE ID = @ID;";

            SqlCommand cmd_Exclusao = new(sql, conexao);

            cmd_Exclusao.Parameters.AddWithValue("ID", entidade.Id);

            try
            {
                cmd_Exclusao.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                return;
            }

            DesconectarBancoDados();
        }

        protected override void InserirRegistroBancoDados(Medicamento entidade)
        {
            ConectarBancoDados();

            sql = @"INSERT INTO TBMEDICAMENTO
                           (
                                [NOME],    
                                [DESCRICAO],
                                [LOTE],
                                [VALIDADE],
                                [QUANTIDADEDISPONIVEL],
                                [FORNECEDOR_ID]
                           )
                           VALUES
                           (
                                @NOME_MEDICAMENTO,
                                @DESCRICAO,
                                @LOTE,
                                @VALIDADE,
                                @QTD,
                                @FORNECEDOR_ID
                           )";

            SqlCommand cmd_Insercao = new(sql, conexao);

            DefinirParametros(entidade, cmd_Insercao);

            entidade.Id = Convert.ToInt32(cmd_Insercao.ExecuteScalar());

            DesconectarBancoDados();
        }

        protected override List<Medicamento> LerTodos(SqlDataReader leitor)
        {
            List<Medicamento> medicamentos = new();

            while (leitor.Read())
            {
                int id = Convert.ToInt32(leitor["ID"]);
                string nome = leitor["NOME_MEDICAMENTO"].ToString();
                string descricao = leitor["DESCRICAO"].ToString();
                string lote = leitor["LOTE"].ToString();
                DateTime validade = Convert.ToDateTime(leitor["VALIDADE"]);
                int quantidadeDisponivel = Convert.ToInt32(leitor["QTD"]);
                
                int fornecedorId = Convert.ToInt32(leitor["FORNECEDOR_ID"]);

                Medicamento medicamento = new Medicamento(nome, descricao, lote, validade)
                {
                    Id = id,
                    QuantidadeDisponivel = quantidadeDisponivel,

                    Fornecedor = new(" ", " ", " ", " ", " ")
                    {
                        Id = fornecedorId,
                    }
                };

                medicamentos.Add(medicamento);
            }

            return medicamentos;
        }

        protected override Medicamento LerUnico(SqlDataReader leitor)
        {
            Medicamento medicamento = null;

            if (leitor.Read())
            {
                int id = Convert.ToInt32(leitor["ID"]);
                string nome = leitor["NOME_MEDICAMENTO"].ToString();
                string descricao = leitor["DESCRICAO"].ToString();
                string lote = leitor["LOTE"].ToString();
                DateTime validade = Convert.ToDateTime(leitor["VALIDADE"]);
                int quantidadeDisponivel = Convert.ToInt32(leitor["QTD"]);

                int fornecedorId = Convert.ToInt32(leitor["FORNECEDOR_ID"]);

                medicamento = new Medicamento(nome, descricao, lote, validade)
                {
                    Id = id,
                    QuantidadeDisponivel = quantidadeDisponivel,

                    Fornecedor = new(" ", " ", " ", " ", " ")
                    {
                        Id = fornecedorId,
                    }
                };
            }
            
            return medicamento;
        }

        protected override ValidationResult Validar(Medicamento entidade)
        {
            return new ValidadorMedicamento().Validate(entidade);
        }
        #endregion

        #region metodos privados
        private void LerFornecedores(List<Medicamento> medicamentos)
        {
            repoFornecedor = new();

            foreach (Medicamento m in medicamentos)
            {
                m.Fornecedor = repoFornecedor.SelecionarUnico(m.Fornecedor.Id);
            }
        }

        private Fornecedor LerFornecedor(int numero)
        {
            repoFornecedor = new();

            return repoFornecedor.SelecionarUnico(numero);
        }

        #endregion
    }
}
