using Anime.Dominio.Interfaces.Entidades;

namespace Anime.Dominio.Entidades
{
    public class Diretor : IEntidade
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }

        public IEnumerable<Anime> Animes { get; set; }
    }
}