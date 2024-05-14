using Anime.Aplicacao.DTOs;
using Anime.Aplicacao.Interfaces.Servicos;
using Anime.Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Anime.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/Usuario")]
    public class UsuarioController : Controller
    {
        private readonly IServicoDTO<Usuario, UsuarioDTO> _servicoUsuario;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IServicoDTO<Usuario, UsuarioDTO> servicoUsuario, 
                                 ILogger<UsuarioController> logger)
        {
            _servicoUsuario = servicoUsuario;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UsuarioDTO>> Get()
        {
            try
            {
                var usuarios = _servicoUsuario.BuscarTodos().ToList();

                if (usuarios.Count == 0)
                    throw new Exception("Ainda não há usuário cadastrado!");

                return Ok(usuarios);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return BadRequest();
            }
        }

        [HttpGet]
        [Route("{codigo:int}")]
        public ActionResult<UsuarioDTO> Get(int codigo)
        {
            try
            {
                var usuario = _servicoUsuario.Buscar(codigo);

                if (usuario == null)
                    return NotFound();

                return Ok(usuario);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UsuarioDTO usuarioDto)
        {
            var usuarioBanco = _servicoUsuario.BuscarTodos()
                                                        .SingleOrDefault(x => x.Email == usuarioDto.Email);

            var jaExisteUsuarioComEsteEmail = usuarioBanco != null;

            if (jaExisteUsuarioComEsteEmail)
                return Conflict();

            try
            {
                _servicoUsuario.Cadastrar(usuarioDto);
                _servicoUsuario.Salvar();

                return Created();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return BadRequest();
            }
        }

        [HttpPut("{codigo:int}")]
        public async Task<ActionResult> Put(int codigo, [FromBody] UsuarioDTO usuarioDto)
        {
            try
            {
                if (codigo != usuarioDto.Codigo)
                {
                    return BadRequest();
                }

                _servicoUsuario.Atualizar(usuarioDto);
                _servicoUsuario.Salvar();

                return Ok(usuarioDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return BadRequest();
            }
        }

        [HttpDelete("{codigo:int}")]
        public async Task<ActionResult<UsuarioDTO>> Delete(int codigo)
        {
            try
            {
                var usuarioDto = _servicoUsuario.Buscar(codigo);

                _servicoUsuario.Excluir(usuarioDto);
                _servicoUsuario.Salvar();

                return Ok(usuarioDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                return BadRequest();
            }
        }
    }
}