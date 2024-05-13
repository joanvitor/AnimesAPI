using Anime.Aplicacao.DTOs;
using Anime.Aplicacao.Interfaces.Servicos;
using Anime.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Anime.API.Controllers
{
    //TODO dar uma olhada sobre FLuentResult

    [ApiController]
    [Route("api/Anime")]
    public class AnimeController : Controller
    {
        private readonly IServicoAnimeDTO _servicoAnime;
        private readonly IServicoDTO<Diretor, DiretorDTO> _servicoDiretor;

        public AnimeController(IServicoAnimeDTO servicoAnime,
                               IServicoDTO<Diretor, DiretorDTO> servicoDiretor)
        {
            _servicoAnime = servicoAnime;
            _servicoDiretor = servicoDiretor;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AnimeDTO>> Get()
        {
            var animes = _servicoAnime.ObterAtivos().ToList();

            return Ok(animes);
        }

        [HttpGet]
        [Route("{codigo:int}")]
        public ActionResult<AnimeDTO> Get(int codigo)
        {
            var animeDto = _servicoAnime.Buscar(codigo);

            if (!animeDto.Apagado)
                return Ok(animeDto);

            return NotFound();
        }

        [HttpGet]
        [Route("{pagina:int}/{quantidade:int}")]
        public ActionResult<IEnumerable<AnimeDTO>> GetPaginado(int pagina, int quantidade)
        {
            var animeDto = _servicoAnime.BuscarPaginado(pagina, quantidade);

            return Ok(animeDto);
        }

        [HttpGet]
        [Route("/diretor/{codigoDiretor:int}")]
        public ActionResult<AnimeDTO> GetByCodigoDiretor(int codigoDiretor)
        {
            var animeDto = _servicoAnime.ObterAtivos().Where(x => x.DiretorCodigo == codigoDiretor);

            return Ok(animeDto);
        }

        [HttpGet]
        [Route("/diretor/{nomeDiretor}")]
        public ActionResult<AnimeDTO> GetByCodigoDiretor(string nomeDiretor)
        {
            var codigosDiretor = _servicoDiretor.BuscarTodos()
                                                              .Where(x => x.Nome.Contains(nomeDiretor))
                                                              .Select(x => x.Codigo);

            var animesDto = _servicoAnime.ObterAtivos().Where(x => codigosDiretor.Contains(x.DiretorCodigo));

            return Ok(animesDto);
        }

        [HttpGet]
        [Route("resumo/{resumo}")]
        public ActionResult<AnimeDTO> GetByResumo(string resumo)
        {
            var animeDto = _servicoAnime.ObterAtivos().Where(x => x.Resumo.Contains(resumo));

            return Ok(animeDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AnimeDTO animeDto)
        {
            _servicoAnime.Cadastrar(animeDto);
            _servicoAnime.Salvar();

            return new CreatedAtRouteResult("ObterAnime", new { codigo = animeDto.Codigo }, animeDto);
        }


        [HttpPut("{codigo:int}")]
        public async Task<ActionResult> Put(int codigo, [FromBody] AnimeDTO animeDto)
        {
            if (codigo != animeDto.Codigo)
            {
                return BadRequest();
            }

            _servicoAnime.Atualizar(animeDto);
            _servicoAnime.Salvar();

            return Ok(animeDto);
        }

        [HttpDelete("{codigo:int}")]
        public async Task<ActionResult<AnimeDTO>> Delete(int codigo)
        {
            var animeDto = _servicoAnime.Buscar(codigo);

            _servicoAnime.RemoverLogicamente(animeDto);
            _servicoAnime.Salvar();

            return Ok(animeDto);
        }
    }
}
