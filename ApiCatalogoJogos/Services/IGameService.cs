using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Services
{
    public interface IGameService: IDisposable
    {
        Task<List<GameViewModel>> Get(int page, int recordsPerPage);
        Task<GameViewModel> Get(Guid id);
        Task<GameViewModel> Create(GameInputModel game);
        Task Update(Guid id, GameInputModel game);
        Task Update(Guid id, double price);
        Task Delete(Guid id);
    }
}
