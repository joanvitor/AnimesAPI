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

        public DiretorController(IServicoDTO<Diretor, DiretorDTO> servicoDiretor)
        {
            _servicoDiretor = servicoDiretor;
        }

        /// <summary>
        /// Teste
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<DiretorDTO>> Get()
        {
            var diretores = _servicoDiretor.BuscarTodos().ToList();

            return Ok(diretores);
        }

        [HttpGet]
        [Route("{codigo:int}")]
        public ActionResult<AnimeDTO> Get(int codigo)
        {
            var diretor = _servicoDiretor.Buscar(codigo);

            return Ok(diretor);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DiretorDTO diretorDto)
        {
            _servicoDiretor.Cadastrar(diretorDto);
            _servicoDiretor.Salvar();

            return new CreatedAtRouteResult("ObterDiretor", new { codigo = diretorDto.Codigo }, diretorDto);
        }

        [HttpPut("{codigo:int}")]
        public async Task<ActionResult> Put(int codigo, [FromBody] DiretorDTO diretorDto)
        {
            if (codigo != diretorDto.Codigo)
            {
                return BadRequest();
            }

            _servicoDiretor.Atualizar(diretorDto);
            _servicoDiretor.Salvar();

            return Ok(diretorDto);
        }

        [HttpDelete("{codigo:int}")]
        public async Task<ActionResult<AnimeDTO>> Delete(int codigo)
        {
            var diretorDto = _servicoDiretor.Buscar(codigo);

            _servicoDiretor.Excluir(diretorDto);
            _servicoDiretor.Salvar();

            return Ok(diretorDto);
        }
    }
}