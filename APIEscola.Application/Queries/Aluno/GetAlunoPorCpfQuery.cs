using APIEscola.Domain;
using MediatR;

namespace APIEscola.Application
{
    public class GetAlunoPorCpfQuery : IRequest<Aluno>
    {
        public string Cpf { get; set; }

        public GetAlunoPorCpfQuery(string cpf)
        {
            Cpf = cpf;
        }
    }
}


