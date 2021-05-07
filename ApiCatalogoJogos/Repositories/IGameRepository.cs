using ApiCatalogoJogos.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Repositories
{
    public interface IGameRepository: IDisposable
    {
        Task<List<Game>> Get(int page, int recordsPerPage);
        Task<Game> Get(Guid id);
        Task<List<Game>> Get(string name, string producer);
        Task Create(Game game);
        Task Update(Game game);
        Task Delete(Guid id);

    }
}
