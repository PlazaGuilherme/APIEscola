using APIEscola.Domain;

namespace APIEscola.DTO
{
    public class AlunoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; } = string.Empty;
        public ICollection<MatriculaDto> Matriculas { get; set; }

        public static AlunoDto FromEntity(Aluno aluno)
        {
            if (aluno == null) return null;

            return new AlunoDto
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Cpf = aluno.Cpf,
                DataNascimento = aluno.DataNascimento,
                Email = aluno.Email,
                Matriculas = aluno.Matriculas?.Select(m => MatriculaDto.FromEntity(m)).ToList()
            };
        }
    }
}



