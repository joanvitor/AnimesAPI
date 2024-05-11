using Anime.Aplicacao.DTOs;
using Anime.Aplicacao.Interfaces.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace Anime.API.Controllers
{
    [ApiController]
    [Route("api/anime")]
    [Route("api/Anime")]
    public class AnimeController : Controller
    {
        private readonly IServicoAnimeDTO _servicoAnime;

        public AnimeController(IServicoAnimeDTO servicoAnime)
        {
            _servicoAnime = servicoAnime;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AnimeDTO>> Get()
        {
            var animes = _servicoAnime.BuscarTodos();

            return Ok(animes);
        }
    }
}
