namespace Anime.Aplicacao.DTOs
{
    public class AnimeDTO
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Resumo { get; set; }
        public bool Apagado { get; set; }

        public int DiretorCodigo { get; set; }
    }
}