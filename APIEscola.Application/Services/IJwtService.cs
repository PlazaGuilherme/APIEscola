using APIEscola.Domain;

namespace APIEscola.Application.Services
{
    public interface IJwtService
    {
        string GenerateToken(Usuario usuario);
    }
}

