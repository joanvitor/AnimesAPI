namespace Anime.Dominio.Entidades
{
    public class Diretor
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }

        public IEnumerable<Anime> Animes { get; set; }
    }
}