using APIEscola.Domain;
using MediatR;

namespace APIEscola.Application
{
    public class GetAlunoPorNomeQuery : IRequest<IEnumerable<Aluno>>
    {
        public string Nome { get; set; }

        public GetAlunoPorNomeQuery(string nome)
        {
            Nome = nome;
        }
    }
}


