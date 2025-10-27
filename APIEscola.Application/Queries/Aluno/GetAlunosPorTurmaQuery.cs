using APIEscola.Domain;
using MediatR;


namespace APIEscola.Application
{
    public class GetAlunosPorTurmaQuery : IRequest<IEnumerable<Aluno>>
    {
        public Guid TurmaId { get; set; }

        public GetAlunosPorTurmaQuery(Guid turmaId)
        {
            TurmaId = turmaId;
        }
    }
}