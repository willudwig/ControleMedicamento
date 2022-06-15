using ControleMedicamentos.Dominio.Compartilhado;

namespace ControleMedicamentos.Dominio.ModuloPaciente
{
    public class Paciente : EntidadeBase<Paciente>
    {
        public Paciente(string nome, string cartaoSUS)
        {
            Nome = nome;

            if (nome != null)
                nome.RemoverCaracteresEspeciais();

            CartaoSUS = cartaoSUS;
        }

        public string Nome { get; set; }
        public string CartaoSUS { get; set; }

    }
}
