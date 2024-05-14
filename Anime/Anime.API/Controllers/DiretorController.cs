using Anime.Aplicacao.DTOs;
using Anime.Aplicacao.Interfaces.Servicos;
using Anime.Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Anime.API.Controllers
{
    //PS E:\Anime.API> dotnet user-jwts create
    //New JWT saved with ID 'd64ba2ac'.
    //Name: Joan Vitor

    //Token: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IkpvYW4gVml0b3IiLCJzdWIiOiJKb2FuIFZpdG9yIiwianRpIjoiZDY0YmEyYWMiLCJhdWQiOlsiaHR0cDovL2xvY2FsaG9zdDo3NjI2IiwiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzNzYiLCJodHRwOi8vbG9jYWxob3N0OjUyNzIiLCJodHRwczovL2xvY2FsaG9zdDo3MjU4Il0sIm5iZiI6MTcxNTY5MjEwMSwiZXhwIjoxNzIzNjQwOTAxLCJpYXQiOjE3MTU2OTIxMDIsImlzcyI6ImRvdG5ldC11c2VyLWp3dHMifQ.hxmGa7Lg0hFPaCEXG9hPoFoJRDG1uPFhZYfQJkXhIiI

    [Authorize]
    [ApiController]
    [Route("api/Diretor")]
    public class DiretorController : Controller
    {
        private readonly IServicoDTO<Diretor, DiretorDTO> _servicoDiretor;
        private readonly ILogger<DiretorController> _logger;

        public DiretorController(IServicoDTO<Diretor, DiretorDTO> servicoDiretor, 
                                 ILogger<DiretorController> logger)
        {
            _servicoDiretor = servicoDiretor;
            _logger = logger;
        }

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