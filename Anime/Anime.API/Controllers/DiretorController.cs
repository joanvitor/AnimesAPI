using Anime.Aplicacao.DTOs;
using Anime.Aplicacao.Interfaces.Servicos;
using Anime.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Anime.API.Controllers
{
    [ApiController]
    [Route("api/Diretor")]
    public class DiretorController : Controller
    {
        private readonly IServicoDTO<Diretor, DiretorDTO> _servicoDiretor;
        private readonly ILogger _logger;

        public DiretorController(IServicoDTO<Diretor, DiretorDTO> servicoDiretor, 
                                 ILogger logger)
        {
            _servicoDiretor = servicoDiretor;
            _logger = logger;
        }

        /// <summary>
        /// Teste
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<DiretorDTO>> Get()
        {
            try
            {
                var diretores = _servicoDiretor.BuscarTodos().ToList();

                if (diretores.Count == 0)
                    throw new Exception("Ainda não há diretor cadastrado!");

                return Ok(diretores);
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
                var diretor = _servicoDiretor.Buscar(codigo);

                return Ok(diretor);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DiretorDTO diretorDto)
        {
            try
            {
                _servicoDiretor.Cadastrar(diretorDto);
                _servicoDiretor.Salvar();

                return new CreatedAtRouteResult("ObterDiretor", new { codigo = diretorDto.Codigo }, diretorDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return BadRequest();
            }
        }

        [HttpPut("{codigo:int}")]
        public async Task<ActionResult> Put(int codigo, [FromBody] DiretorDTO diretorDto)
        {
            try
            {
                if (codigo != diretorDto.Codigo)
                {
                    return BadRequest();
                }

                _servicoDiretor.Atualizar(diretorDto);
                _servicoDiretor.Salvar();

                return Ok(diretorDto);
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
                var diretorDto = _servicoDiretor.Buscar(codigo);

                _servicoDiretor.Excluir(diretorDto);
                _servicoDiretor.Salvar();

                return Ok(diretorDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return BadRequest();
            }
        }
    }
}