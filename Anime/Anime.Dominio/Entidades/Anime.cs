namespace Anime.Dominio.Entidades
{
    public class Anime
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Resumo { get; set; }
        public bool Apagado { get; set; }

        public int DiretorCodigo { get; set; }
        public virtual Diretor Diretor { get; set; }
    }
}