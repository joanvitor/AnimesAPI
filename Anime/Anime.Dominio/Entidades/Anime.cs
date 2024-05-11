using Anime.Dominio.Interfaces.Entidades;

namespace Anime.Dominio.Entidades
{
    public class Anime : IEntidade
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Resumo { get; set; }
        public bool Apagado { get; set; }

        public int DiretorCodigo { get; set; }
        public virtual Diretor Diretor { get; set; }

        public void DefinirApagado(bool apagado)
            => Apagado = apagado;
    }
}