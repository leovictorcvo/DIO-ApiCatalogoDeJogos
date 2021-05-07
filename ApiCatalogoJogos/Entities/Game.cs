using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.ViewModel;
using System;

namespace ApiCatalogoJogos.Entities
{
    public class Game
    {
        public Game()
        {

        }

        public Game(GameInputModel model)
        {
            this.Id = Guid.NewGuid();
            this.Name = model.Name;
            this.Producer = model.Producer;
            this.Price = model.Price;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public double Price { get; set; }

        public GameViewModel ToGameViewModel() => new()
        {
            Id = this.Id,
            Name = this.Name,
            Producer = this.Producer,
            Price = this.Price
        };

        public void UpdateFromGameInputModel(GameInputModel game)
        {
            this.Id = Guid.NewGuid();
            this.Name = game.Name;
            this.Producer = game.Producer;
            this.Price = game.Price;
        }
    }
}
