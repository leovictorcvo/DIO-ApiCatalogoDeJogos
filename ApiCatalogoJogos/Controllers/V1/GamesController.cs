using ApiCatalogoJogos.Exceptions;
using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.Services;
using ApiCatalogoJogos.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService gameService;

        public GamesController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        /// <summary>
        /// Retorna uma lista contendo todos os jogos cadastrados de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os jogos sem paginação
        /// </remarks>
        /// <param name="pagina">Número da página desejada</param>
        /// <param name="quantidade">Quantidade de jogos para serem exibidos em cada página</param>
        /// <response code="200">Retorna a lista de jogos</response>
        /// <response code="204">Não há jogos cadastrados</response>
        [HttpGet]
        public async Task<ActionResult<List<GameViewModel>>> Get([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var games = await gameService.Get(pagina, quantidade);
            if (games.Count == 0)
            {
                return NoContent();
            }

            return Ok(games);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<GameViewModel>> Get(Guid id) 
        {
            var game = await gameService.Get(id);
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<GameViewModel>> Create([FromBody] GameInputModel game)
        {
            try
            {
                var savedGame = await gameService.Create(game);
                return Ok(savedGame);
            }
            catch (GameAlreadyExistException)
            {
                return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
            }
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] GameInputModel game)
        {
            try
            {
                await gameService.Update(id, game);
                return Ok();
            }
            catch (GameNotFoundException)
            {
                return NotFound("Jogo não encontrado");
            }
        }

        [HttpPatch("{id:Guid}/preco/{price:double}")]
        public async Task<ActionResult> UpdatePrice([FromRoute] Guid id, [FromRoute] double price)
        {
            try
            {
                await gameService.Update(id, price);
                return Ok();
            }
            catch (GameNotFoundException)
            {
                return NotFound("Jogo não encontrado");
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                await gameService.Delete(id);
                return Ok();
            }
            catch (GameNotFoundException)
            {
                return NotFound("Jogo não encontrado");
            }
        }

    }
}
