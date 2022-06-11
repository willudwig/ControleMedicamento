using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloPaciente
{
    public class RepositorioPacienteEmBancoDados : ConexaoBancoDados<Paciente>, IRepositorio<Paciente>
    {
        public ValidationResult Inserir(Paciente entidade)
        {
            ValidationResult resultado = Validar(entidade);

            if (resultado.IsValid)
                InserirRegistroBancoDados(entidade);

            return resultado;
        }

        public ValidationResult Editar(Paciente entidade)
        {
            ValidationResult resultado = Validar(entidade);

            if (resultado.IsValid)
                EditarRegistroBancoDados(entidade);

            return resultado;
        }

        public ValidationResult Excluir(Paciente entidade)
        {
            ValidationResult resultado = Validar(entidade);

            if (resultado.IsValid)
                ExcluirRegistroBancoDados(entidade);

            return resultado;
        }

        public List<Paciente> SelecionarTodos()
        {
            ConectarBancoDados();

            sql = @"SELECT * FROM TBPACIENTE";

            SqlCommand cmd_Selecao = new(sql, conexao);

            SqlDataReader leitor = cmd_Selecao.ExecuteReader();

            List<Paciente> pacientes = LerTodos(leitor);

            DesconectarBancoDados();

            return pacientes;
        }

        public Paciente SelecionarUnico(int numero)
        {
            ConectarBancoDados();

            sql = @"SELECT * FROM TBPACIENTE WHERE ID = @ID";

            SqlCommand cmdSelecao = new(sql, conexao);

            cmdSelecao.Parameters.AddWithValue("ID", numero);

            SqlDataReader leitor = cmdSelecao.ExecuteReader();

            Paciente selecionado = LerUnico(leitor);

            DesconectarBancoDados();

            return selecionado;
        }

        #region metodos protected
        protected override void DefinirParametros(Paciente entidade, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("NOME_PACIENTE", entidade.Nome);
            cmd.Parameters.AddWithValue("CARTAOSUS", entidade.CartaoSUS);
        }

        protected override void DefinirParametros(Paciente entidade, SqlCommand cmd, int entidadeId)
        {
            cmd.Parameters.AddWithValue("NOME_PACIENTE", entidade.Nome);
            cmd.Parameters.AddWithValue("CARTAOSUS", entidade.CartaoSUS);
            cmd.Parameters.AddWithValue("ID", entidade.Id);
        }

        protected override void EditarRegistroBancoDados(Paciente entidade)
        {
            ConectarBancoDados();

            sql = @"UPDATE [TBPACIENTE] SET 

                        [NOME] = @NOME,    
	                    [CARTAOSUS] = @LOGIN

                   WHERE
		                 ID = @ID;";

            SqlCommand cmd_Edicao = new(sql, conexao);

            DefinirParametros(entidade, cmd_Edicao, entidade.Id);

            cmd_Edicao.ExecuteNonQuery();

            DesconectarBancoDados();
        }

        protected override void ExcluirRegistroBancoDados(Paciente entidade)
        {
            ConectarBancoDados();

            sql = @"DELETE FROM TBPACIENTE WHERE ID = @ID;";

            SqlCommand cmd_Exclusao = new(sql, conexao);

            cmd_Exclusao.Parameters.AddWithValue("ID", entidade.Id);

            cmd_Exclusao.ExecuteNonQuery();

            DesconectarBancoDados();
        }

        protected override void InserirRegistroBancoDados(Paciente entidade)
        {
            ConectarBancoDados();

            sql = @"INSERT INTO TBPACIENTE
                           (
                                [NOME],    
                                [CARTAOSUS]
                           )
                           VALUES
                           (
                                @NOME_PACIENTE,
                                @CARTAOSUS
                           )";

            SqlCommand cmd_Insercao = new(sql, conexao);

            DefinirParametros(entidade, cmd_Insercao);

            entidade.Id = Convert.ToInt32(cmd_Insercao.ExecuteScalar());

            DesconectarBancoDados();
        }

        protected override List<Paciente> LerTodos(SqlDataReader leitor)
        {
            List<Paciente> pacientes = new();

            while (leitor.Read())
            {
                int id = Convert.ToInt32(leitor["ID"]);
                string nome = leitor["NOME_PACIENTE"].ToString();
                string cartaoSus = leitor["CARTAOSUS"].ToString();

                Paciente paciente = new Paciente(nome, cartaoSus)
                {
                    Id = id
                };

                pacientes.Add(paciente);
            }

            return pacientes;
        }

        protected override Paciente LerUnico(SqlDataReader leitor)
        {
            Paciente paciente = null;

            if (leitor.Read())
            {
                int id = Convert.ToInt32(leitor["ID"]);
                string nome = leitor["NOME_PACIENTE"].ToString();
                string cartaoSus = leitor["CARTAOSUS"].ToString();

                paciente = new Paciente(nome, cartaoSus)
                {
                    Id = id
                };
            }

            return paciente;
        }

        protected override ValidationResult Validar(Paciente entidade)
        {
            return new ValidadorPaciente().Validate(entidade);
        }
        #endregion
    }
}
