using Anime.Dominio.Interfaces.Marcadores;

namespace Anime.Dominio.Entidades
{
    public class Diretor : IEntidadeDominio
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }

        public IEnumerable<Anime> Animes { get; set; }
    }
}