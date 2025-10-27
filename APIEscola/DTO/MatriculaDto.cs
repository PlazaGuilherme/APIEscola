using APIEscola.Domain;

namespace APIEscola.DTO
{
    public class MatriculaDto
    {
        public Guid Id { get; set; }
        public Guid AlunoId { get; set; }
        public Guid TurmaId { get; set; }
        public DateTime DataMatricula { get; set; }

        public static MatriculaDto FromEntity(Matricula matricula)
        {
            if (matricula == null) return null;

            return new MatriculaDto
            {
                Id = matricula.Id,
                AlunoId = matricula.AlunoId,
                TurmaId = matricula.TurmaId,
                DataMatricula = matricula.DataMatricula
            };
        }
    }
}
