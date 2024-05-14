using System.IdentityModel.Tokens.Jwt;
using Anime.Aplicacao.Interfaces.Servicos;
using Anime.Aplicacao.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Anime.Aplicacao.Servicos
{
    public class ServicoToken : IServicoToken
    {
        public string GerarToken(UsuarioDTO usuario, SymmetricSecurityKey chave)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var descricaoToken = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Name, usuario.Nome)
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateJwtSecurityToken(descricaoToken);

            return tokenHandler.WriteToken(token);
        }
    }
}