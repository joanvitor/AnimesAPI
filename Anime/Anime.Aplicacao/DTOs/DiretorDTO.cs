using Anime.Aplicacao.Interfaces.Marcadores;

namespace Anime.Aplicacao.DTOs
{
    public class DiretorDTO : IEntidadeDTO
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
    }
}