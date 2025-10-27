using APIEscola.Domain;
using System.Text.RegularExpressions;

namespace APIEscola.Application.Validators
{
    public static class AlunoValidator
    {
        public static bool ValidarCamposObrigatorios(Aluno aluno)
        {
            return !string.IsNullOrWhiteSpace(aluno.Nome) &&
                   !string.IsNullOrWhiteSpace(aluno.Cpf) &&
                   !string.IsNullOrWhiteSpace(aluno.Email) &&
                   aluno.DataNascimento != default;
        }

        public static bool ValidarCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11 || !Regex.IsMatch(cpf, @"^\d{11}$"))
                return false;

            return true;
        }

        public static bool ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailRegex);
        }

        public static bool ValidarDataNascimento(DateTime dataNascimento)
        {
            var dataAtual = DateTime.Now;
            var dataLimite = new DateTime(1900, 1, 1);

            return dataNascimento <= dataAtual && dataNascimento >= dataLimite;
        }
    }
}
