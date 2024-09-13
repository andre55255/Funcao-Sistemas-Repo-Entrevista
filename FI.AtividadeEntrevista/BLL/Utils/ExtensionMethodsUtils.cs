namespace FI.AtividadeEntrevista.BLL.Utils
{
    public static class ExtensionMethodsUtils
    {
        public static string RemoveFormattingCPF(this string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return cpf;

            return cpf.Replace(".", "").Replace("-", "").Trim();
        }

        public static string AddFormattingCPF(this string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11)
                return cpf;

            return $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9, 2)}";
        }
    }
}
