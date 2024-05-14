using Anime.Dominio.Interfaces.Marcadores;

namespace Anime.Dominio.Entidades
{
    public class Usuario : IEntidadeDominio
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}