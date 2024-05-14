using Anime.Aplicacao.Interfaces.Marcadores;

namespace Anime.Aplicacao.DTOs
{
    public class UsuarioDTO : IEntidadeDTO
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}