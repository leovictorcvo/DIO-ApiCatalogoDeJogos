using ApiCatalogoJogos.Entities;
using ApiCatalogoJogos.Exceptions;
using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.Repositories;
using ApiCatalogoJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Services.Implementations
{
    public class GameService : IGameService
    {
        private readonly IGameRepository gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public async Task<GameViewModel> Create(GameInputModel game)
        {
            var gamesWithNameAndProducer = await gameRepository.Get(game.Name, game.Producer);

            if (gamesWithNameAndProducer.Count > 0)
                throw new GameAlreadyExistException();

            var newGame = new Game(game);

            await gameRepository.Create(newGame);

            return newGame.ToGameViewModel();
        }

        public async Task Delete(Guid id)
        {
            var savedGame = await gameRepository.Get(id);

            if (savedGame == null)
            {
                throw new GameNotFoundException();
            }

            await gameRepository.Delete(id);
        }

        public void Dispose()
        {
            gameRepository?.Dispose();
        }

        public async Task<List<GameViewModel>> Get(int page, int recordsPerPage)
        {
            var games = await gameRepository.Get(page, recordsPerPage);

            return games.Select(g => g.ToGameViewModel()).ToList();
        }

        public async Task<GameViewModel> Get(Guid id)
        {
            var game = await gameRepository.Get(id);

            return game?.ToGameViewModel();
        }

        public async Task Update(Guid id, GameInputModel game)
        {
            var savedGame = await gameRepository.Get(id);

            if (savedGame == null)
            {
                throw new GameNotFoundException();
            }

            savedGame.UpdateFromGameInputModel(game);

            await gameRepository.Update(savedGame);
        }

        public async Task Update(Guid id, double price)
        {
            var savedGame = await gameRepository.Get(id);

            if (savedGame == null)
            {
                throw new GameNotFoundException();
            }

            savedGame.Price = price;

            await gameRepository.Update(savedGame);
        }
    }
}
