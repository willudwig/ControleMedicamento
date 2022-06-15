
namespace ControleMedicamentos.Dominio.Compartilhado
{
    public static class StringExtension
    {
        public static string RemoverCaracteresEspeciais(this string texto)
        {
            texto = string.Join("", texto.Split('@', ',', '.', ';', '\'', '#', '$', '%', '&', '*', '(', ')', '§', '!', '?'));

            return texto;
        }
    }
}
