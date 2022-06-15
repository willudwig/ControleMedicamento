using ControleMedicamentos.Dominio.Compartilhado;
using System.Collections.Generic;


namespace ControleMedicamentos.Dominio.ModuloMedicamento
{
    public interface IRepositorioMedicamento : IRepositorio<Medicamento>
    {
        List<Medicamento> ObterRemediosBaixaQtd();

        List<Medicamento> ObterRemediosMaisRequisitados(List<Medicamento> meds);

        public List<Medicamento> ObterRemediosFalta();
    }
}
