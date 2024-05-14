using Anime.Aplicacao.DTOs;
using Anime.Aplicacao.Interfaces.Servicos;
using Anime.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Anime.API.Controllers
{
    public class AutenticacaoController : Controller
    {
        private readonly IServicoToken _servicoToken;
        private readonly IServicoDTO<Usuario, UsuarioDTO> _servicoUsuario;

        public AutenticacaoController(IServicoToken servicoToken, 
                                      IServicoDTO<Usuario, UsuarioDTO> servicoUsuario)
        {
            _servicoToken = servicoToken;
            _servicoUsuario = servicoUsuario;
        }

        /// <summary>
        /// Logar na API
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioDTO usuario)
        {
            var usuarioBanco = _servicoUsuario.BuscarTodos()
                                                        .SingleOrDefault(x => x.Email == usuario.Email);

            if (usuarioBanco == null)
                return BadRequest("Usuário ou senha inválidos");

            if (usuarioBanco.Email != usuario.Email)
                return BadRequest("Usuário ou senha inválidos");

            if (usuarioBanco.Senha != usuario.Senha)
                return BadRequest("Usuário ou senha inválidos");

            var token = _servicoToken.GerarToken(usuario);

            return new ContentResult
            {
                ContentType = "text/plain",
                Content = token,
                StatusCode = 200
            }; ;
        }
    }
}