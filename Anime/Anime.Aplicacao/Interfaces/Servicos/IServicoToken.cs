using Anime.Aplicacao.DTOs;

namespace Anime.Aplicacao.Interfaces.Servicos
{
    public interface IServicoToken
    {
        string GerarToken(UsuarioDTO usuario);
    }
}
