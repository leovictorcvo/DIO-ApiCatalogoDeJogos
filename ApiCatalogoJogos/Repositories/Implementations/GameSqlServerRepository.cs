using ApiCatalogoJogos.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Repositories.Implementations
{
    public class GameSqlServerRepository : IGameRepository
    {
        private readonly SqlConnection sqlConnection;

        public GameSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task Create(Game game)
        {
            var sql = $"INSERT Games (id, name, producer, price) VALUES ('{game.Id}', '{game.Name}', '{game.Producer}', '{game.Price.ToString().Replace(",", ".")}')";

            await sqlConnection.OpenAsync();
            var sqlCommand = new SqlCommand(sql, sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();
            await sqlConnection.CloseAsync();
        }

        public async Task Delete(Guid id)
        {
            var sql = $"DELETE Games WHERE id = '{id}'";

            await sqlConnection.OpenAsync();
            var sqlCommand = new SqlCommand(sql, sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }

        public async Task<List<Game>> Get(int page, int recordsPerPage)
        {
            var sql = $"SELECT id, name, producer, price FROM Games ORDER BY id OFFSET {(page - 1) * recordsPerPage} ROWS FETCH NEXT {recordsPerPage} ROWS ONLY";

            return await GetListOfGameFromQueryAsync(sql);
        }

        public async Task<Game> Get(Guid id)
        {
            var sql = $"SELECT id, name, producer, price FROM Games WHERE id = '{id}'";

            var games = await GetListOfGameFromQueryAsync(sql);

            return games.FirstOrDefault();
        }

        public async Task<List<Game>> Get(string name, string producer)
        {
            var sql = $"SELECT id, name, producer, price FROM Games WHERE name = '{name}' AND producer = '{producer}'";

            return await GetListOfGameFromQueryAsync(sql);
        }

        public async Task Update(Game game)
        {
            var sql = $"UPDATE Games SET id = '{game.Id}', name = '{game.Name}', producer = '{game.Producer}', price = '{game.Price.ToString().Replace(",", ".")}' WHERE id  = '{game.Id}'";

            await sqlConnection.OpenAsync();
            var sqlCommand = new SqlCommand(sql, sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();
            await sqlConnection.CloseAsync();
        }

        private async Task<List<Game>> GetListOfGameFromQueryAsync(string sql)
        {
            await sqlConnection.OpenAsync();
            var sqlCommand = new SqlCommand(sql, sqlConnection);
            var sqlReader = await sqlCommand.ExecuteReaderAsync();

            var games = new List<Game>();
            while (sqlReader.Read())
            {
                games.Add(new Game
                {
                    Id = (Guid)sqlReader["id"],
                    Name = (string)sqlReader["name"],
                    Producer = (string)sqlReader["producer"],
                    Price = double.Parse(sqlReader["price"].ToString())
                });
            }

            await sqlConnection.CloseAsync();

            return games;
        }
    }
}
