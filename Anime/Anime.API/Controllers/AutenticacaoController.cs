using Anime.Aplicacao.DTOs;
using Anime.Aplicacao.Interfaces.Servicos;
using Anime.Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Anime.API.Controllers
{
    public class AutenticacaoController : Controller
    {
        private readonly IServicoToken _servicoToken;
        private readonly IServicoDTO<Usuario, UsuarioDTO> _servicoUsuario;
        private readonly IConfiguration _configuracao;

        public AutenticacaoController(IServicoToken servicoToken, 
                                      IServicoDTO<Usuario, UsuarioDTO> servicoUsuario, 
                                      IConfiguration configuracao)
        {
            _servicoToken = servicoToken;
            _servicoUsuario = servicoUsuario;
            _configuracao = configuracao;
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

            // arquivo appsettings.json
            var chaveSecreta = _configuracao["ChaveSecreta:chave"];

            byte[] bytesChave = Encoding.UTF8.GetBytes(chaveSecreta);

            var chaveSecretaCodificada = new SymmetricSecurityKey(bytesChave);

            var token = _servicoToken.GerarToken(usuario, chaveSecretaCodificada);

            return new ContentResult
            {
                ContentType = "text/plain",
                Content = token,
                StatusCode = 200
            }; ;
        }
    }
}