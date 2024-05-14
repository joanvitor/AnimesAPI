using Anime.Aplicacao.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace Anime.Aplicacao.Interfaces.Servicos
{
    public interface IServicoToken
    {
        string GerarToken(UsuarioDTO usuario, SymmetricSecurityKey chave);
    }
}
