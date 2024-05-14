using Anime.Aplicacao.Interfaces.OperacoesBancoDeDados;
using Anime.Aplicacao.Interfaces.Marcadores;
using Anime.Dominio.Interfaces.Marcadores;

namespace Anime.Aplicacao.Interfaces.Servicos
{
    public interface IServicoDTO<TEntidadeDominio, TEntidadeDTO> : ICRUDDTO<TEntidadeDTO> 
                                                                        where TEntidadeDTO : IEntidadeDTO 
                                                                        where TEntidadeDominio : IEntidadeDominio
    {
    }
}