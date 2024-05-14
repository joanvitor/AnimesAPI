using Anime.Aplicacao.DTOs;
using Anime.Aplicacao.Interfaces.Servicos;
using Anime.Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Anime.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Anime")]
    public class AnimeController : Controller
    {
        private readonly IServicoAnimeDTO _servicoAnime;
        private readonly IServicoDTO<Diretor, DiretorDTO> _servicoDiretor;
        private readonly ILogger<AnimeController> _logger;

        public AnimeController(IServicoAnimeDTO servicoAnime,
                               IServicoDTO<Diretor, DiretorDTO> servicoDiretor, 
                               ILogger<AnimeController> logger)
        {
            _servicoAnime = servicoAnime;
            _servicoDiretor = servicoDiretor;
            _logger = logger;
        }

        /// <summary>
        /// Obter todos os Animes cadastrados
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Obter Anime a partir do código
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Obter Animes de forma paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="quantidade"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Obter Anime a partir do código do Diretor
        /// </summary>
        /// <param name="codigoDiretor"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Obter Anime a partir do nome do Diretor
        /// </summary>
        /// <param name="nomeDiretor"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Obter Anime a partir do resumo
        /// </summary>
        /// <param name="resumo"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Cadastrar um Anime
        /// </summary>
        /// <param name="animeDto"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Atualizar um Anime
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="animeDto"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Apagar o Anime logicamente
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
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
