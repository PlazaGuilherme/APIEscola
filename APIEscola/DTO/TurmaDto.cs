using APIEscola.Domain;

namespace APIEscola.DTO
{
    public class TurmaDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int QuantidadeAlunos { get; set; }

        public static TurmaDto FromEntity(Turma turma)
        {
            if (turma == null) return null;

            return new TurmaDto
            {
                Id = turma.Id,
                Nome = turma.Nome,
                Descricao = turma.Descricao,
                QuantidadeAlunos = turma.QuantidadeAlunos
            };
        }
    }
}
