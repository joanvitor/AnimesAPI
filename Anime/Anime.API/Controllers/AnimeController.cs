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
        private readonly ILogger _logger;

        public AnimeController(IServicoAnimeDTO servicoAnime,
                               IServicoDTO<Diretor, DiretorDTO> servicoDiretor, 
                               ILogger logger)
        {
            _servicoAnime = servicoAnime;
            _servicoDiretor = servicoDiretor;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AnimeDTO>> Get()
        {
            try
            {
                var animes = _servicoAnime.ObterAtivos().ToList();

                return Ok(animes);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return BadRequest();
            }
        }

        [HttpGet]
        [Route("{codigo:int}")]
        public ActionResult<AnimeDTO> Get(int codigo)
        {
            try
            {
                var animeDto = _servicoAnime.Buscar(codigo);

                if (!animeDto.Apagado)
                    return Ok(animeDto);

                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return BadRequest();
            }
        }

        [HttpGet]
        [Route("{pagina:int}/{quantidade:int}")]
        public ActionResult<IEnumerable<AnimeDTO>> GetPaginado(int pagina, int quantidade)
        {
            try
            {
                var animeDto = _servicoAnime.BuscarPaginado(pagina, quantidade);

                return Ok(animeDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return BadRequest();
            }
        }

        [HttpGet]
        [Route("/diretor/{codigoDiretor:int}")]
        public ActionResult<AnimeDTO> GetByCodigoDiretor(int codigoDiretor)
        {
            try
            {
                var animeDto = _servicoAnime.ObterAtivos().Where(x => x.DiretorCodigo == codigoDiretor);

                return Ok(animeDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return BadRequest();
            }
        }

        [HttpGet]
        [Route("/diretor/{nomeDiretor}")]
        public ActionResult<AnimeDTO> GetByCodigoDiretor(string nomeDiretor)
        {
            try
            {
                var codigosDiretor = _servicoDiretor.BuscarTodos()
                    .Where(x => x.Nome.Contains(nomeDiretor))
                    .Select(x => x.Codigo);

                var animesDto = _servicoAnime.ObterAtivos().Where(x => codigosDiretor.Contains(x.DiretorCodigo));

                return Ok(animesDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return BadRequest();
            }
        }

        [HttpGet]
        [Route("resumo/{resumo}")]
        public ActionResult<AnimeDTO> GetByResumo(string resumo)
        {
            try
            {
                var animeDto = _servicoAnime.ObterAtivos().Where(x => x.Resumo.Contains(resumo));

                return Ok(animeDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AnimeDTO animeDto)
        {
            try
            {
                _servicoAnime.Cadastrar(animeDto);
                _servicoAnime.Salvar();

                return new CreatedAtRouteResult("ObterAnime", new { codigo = animeDto.Codigo }, animeDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return BadRequest();
            }
        }


        [HttpPut("{codigo:int}")]
        public async Task<ActionResult> Put(int codigo, [FromBody] AnimeDTO animeDto)
        {
            try
            {
                if (codigo != animeDto.Codigo)
                {
                    return BadRequest();
                }

                _servicoAnime.Atualizar(animeDto);
                _servicoAnime.Salvar();

                return Ok(animeDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return BadRequest();
            }
        }

        [HttpDelete("{codigo:int}")]
        public async Task<ActionResult<AnimeDTO>> Delete(int codigo)
        {
            try
            {
                var animeDto = _servicoAnime.Buscar(codigo);

                _servicoAnime.RemoverLogicamente(animeDto);
                _servicoAnime.Salvar();

                return Ok(animeDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return BadRequest();
            }
        }
    }
}
