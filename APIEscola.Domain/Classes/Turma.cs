using Domain;
using System.ComponentModel.DataAnnotations;

namespace APIEscola.Domain
{
    public class Turma : Entity
    {
        [Required(ErrorMessage = "O nome da turma é obrigatório")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(300)]
        public string Descricao { get; set; } = string.Empty;

        public ICollection<Aluno?> Alunos { get; set; } = new List<Aluno?>();

        public int QuantidadeAlunos { get; set; }

        public ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
    }
}
