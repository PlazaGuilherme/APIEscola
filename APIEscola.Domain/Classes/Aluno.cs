using Domain;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APIEscola.Domain
{
    public class Aluno : Entity
    {

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(150)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [StringLength(14)]
        public string Cpf { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(100)]
        [JsonIgnore] 
        public string Senha { get; set; }
        public ICollection<Matricula> Matriculas { get; set; }
    }
}
