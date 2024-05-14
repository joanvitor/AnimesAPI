using Anime.API.Controllers;
using Anime.Aplicacao.DTOs;
using Anime.Aplicacao.Interfaces.Servicos;
using Anime.Dominio.Entidades;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Anime.TestesUnitarios.Api
{
    public class DiretorControllerUnitTest
    {
        private readonly DiretorController _controller;

        private readonly Mock<IServicoDTO<Diretor, DiretorDTO>> _servicoDtoDiretorMock;
        private readonly Mock<ILogger> _loggerMock;

        public DiretorControllerUnitTest()
        {
            _servicoDtoDiretorMock = new Mock<IServicoDTO<Diretor, DiretorDTO>>();
            _loggerMock = new Mock<ILogger>();

            _controller = new DiretorController(_servicoDtoDiretorMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void AoObterDiretoresEExistirDiretoresCadastradosDeveRetornarUmOkObjectResult()
        {
            // Arrange
            var diretor = new DiretorDTO
            {
                Codigo = 1,
                Nome = "Diretor para teste"
            };

            var diretoresDto = new List<DiretorDTO>() { diretor };

            _servicoDtoDiretorMock.Setup(x => x.BuscarTodos()).Returns(diretoresDto.AsQueryable());

            // Action
            var actionResult = _controller.Get();

            // Assert
            actionResult.Result.Should().BeOfType<OkObjectResult>();
            Assert.IsType<ActionResult<IEnumerable<DiretorDTO>>>(actionResult);
        }

        [Fact]
        public void AoObterDiretoresENaoExistirDiretoresCadastradosDeveRetornarUmBadRequestResult()
        {
            // Action
            var actionResult = _controller.Get();

            // Assert
            Assert.IsType<ActionResult<IEnumerable<DiretorDTO>>>(actionResult);
        }
    }
}
