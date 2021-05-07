using ApiCatalogoJogos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Repositories.Implementations
{
    public class GameMemoryRepository : IGameRepository
    {
        private static readonly Dictionary<Guid, Game> games = new()
        {
            { Guid.Parse("3FB75E88-0ABA-47BF-B16F-2FDDD79441F4"), new Game { Id = Guid.Parse("3FB75E88-0ABA-47BF-B16F-2FDDD79441F4"), Name = "Fifa 21", Producer = "EA", Price = 200 } },
            { Guid.Parse("041B6184-3FB5-4296-AC31-87392DA3D8D7"), new Game { Id = Guid.Parse("041B6184-3FB5-4296-AC31-87392DA3D8D7"), Name = "Fifa 20", Producer = "EA", Price = 199 } },
            { Guid.Parse("28775BDD-954A-4F79-AE56-C7D663793E73"), new Game { Id = Guid.Parse("28775BDD-954A-4F79-AE56-C7D663793E73"), Name = "Fifa 19", Producer = "EA", Price = 198 } },
            { Guid.Parse("5190ABFA-E544-4056-8A83-A33CD941DBB8"), new Game { Id = Guid.Parse("5190ABFA-E544-4056-8A83-A33CD941DBB8"), Name = "Fifa 18", Producer = "EA", Price = 197 } },
            { Guid.Parse("EAD2F2B3-E4A9-42FA-A10B-6961FDD0C4A1"), new Game { Id = Guid.Parse("EAD2F2B3-E4A9-42FA-A10B-6961FDD0C4A1"), Name = "Fifa 17", Producer = "EA", Price = 196 } },
            { Guid.Parse("B56AF006-0858-4E48-B717-6E889D599C65"), new Game { Id = Guid.Parse("B56AF006-0858-4E48-B717-6E889D599C65"), Name = "Street Fighter V", Producer = "Capcom", Price = 80 } },
            { Guid.Parse("FC1432AD-80D3-423C-ADE9-2E040D4072BE"), new Game { Id = Guid.Parse("FC1432AD-80D3-423C-ADE9-2E040D4072BE"), Name = "Grand Theft Auto V", Producer = "Rockstar", Price = 106 } }
        };

        public Task Create(Game game)
        {
            games.Add(game.Id, game);
            return Task.CompletedTask;
        }

        public Task Delete(Guid id)
        {
            games.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //Close database connection
        }

        public Task<List<Game>> Get(int page, int recordsPerPage)
        {
            return Task.FromResult(games.Values.Skip((page - 1) * recordsPerPage).Take(recordsPerPage).ToList());
        }

        public Task<Game> Get(Guid id)
        {
            if (!games.ContainsKey(id))
                return Task.FromResult<Game>(null);

            return Task.FromResult(games[id]);
        }

        public Task<List<Game>> Get(string name, string producer)
        {
            return Task.FromResult(games.Values.Where(g => g.Name.Equals(name) && g.Producer.Equals(producer)).ToList());
        }

        public Task Update(Game game)
        {
            games[game.Id] = game;
            return Task.CompletedTask;
        }
    }
}
