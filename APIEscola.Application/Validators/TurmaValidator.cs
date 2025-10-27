namespace APIEscola.Application
{
    public static class TurmaValidator
    {
        public static bool ValidarNome(string nome, int minLength = 3, int maxLength = 100)
        {
            return !string.IsNullOrWhiteSpace(nome) && nome.Length >= minLength && nome.Length <= maxLength;
        }

        public static bool ValidarDescricao(string descricao, int minLength = 10, int maxLength = 250)
        {
            return !string.IsNullOrWhiteSpace(descricao) && descricao.Length >= minLength && descricao.Length <= maxLength;
        }
    }
}
