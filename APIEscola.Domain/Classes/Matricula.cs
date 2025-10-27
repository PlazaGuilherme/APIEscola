using Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIEscola.Domain
{
    public class Matricula : Entity
    {

        [Required]
        [ForeignKey("Aluno")]
        public Guid AlunoId { get; set; }

        [Required]
        [ForeignKey("Turma")]
        public Guid TurmaId { get; set; }

        public DateTime DataMatricula { get; set; } = DateTime.Now;

        public Aluno? Aluno { get; set; }
        public Turma? Turma { get; set; }
    }
}
