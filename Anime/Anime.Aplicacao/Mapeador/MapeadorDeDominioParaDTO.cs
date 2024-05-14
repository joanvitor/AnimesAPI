using Anime.Aplicacao.DTOs;
using Anime.Dominio.Entidades;
using AutoMapper;

namespace Anime.Aplicacao.Mapeador
{
    public class MapeadorDeDominioParaDTO : Profile
    {
        public MapeadorDeDominioParaDTO()
        {
            CreateMap<Dominio.Entidades.Anime, AnimeDTO>().ReverseMap();
            CreateMap<Diretor, DiretorDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
        }
    }
}