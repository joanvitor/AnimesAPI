using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Anime.Aplicacao.Utils
{
    public static class ChaveSecreta
    {
        public static SymmetricSecurityKey Obter()
        {
            string chaveSecreta = "2a4c69c6-a4e1-42e5-b8bc-a94f451485c";

            byte[] bytesChave = Encoding.UTF8.GetBytes(chaveSecreta);

            var chaveSecretaCodificada = new SymmetricSecurityKey(bytesChave);

            return chaveSecretaCodificada;
        }
    }
}