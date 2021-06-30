using System.Data.Common;
using catalogoJogosAPI.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace catalogoJogosAPI.Repositories
{
    public class JogoSqlServerRepository : IJogoRepository
    {
        private readonly MySqlConnection mySqlConnection;

        public JogoSqlServerRepository(IConfiguration configuration)
        {
            mySqlConnection = new MySqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Jogo>> Obter(int pagina, int quantidade)
        {
            var jogos = new List<Jogo>();

            var comando = $"select * from Jogos order by id offset {((pagina - 1) * quantidade)} rows fetch next {quantidade} rows only";

            await mySqlConnection.OpenAsync();
            MySqlCommand mySqlCommand = new MySqlCommand(comando, mySqlConnection);
            DbDataReader mySqlDataReader = await mySqlCommand.ExecuteReaderAsync();

            while (mySqlDataReader.Read())
            {
                jogos.Add(new Jogo
                {
                    Id = (Guid)mySqlDataReader["Id"],
                    Nome = (string)mySqlDataReader["Nome"],
                    Produtora = (string)mySqlDataReader["Produtora"],
                    Preco = (double)mySqlDataReader["Preco"]
                });
            }

            await mySqlConnection.CloseAsync();

            return jogos;
        }

        public async Task<Jogo> Obter(Guid id)
        {
            Jogo jogo = null;

            var comando = $"select * from Jogos where Id = '{id}'";

            await mySqlConnection.OpenAsync();
            MySqlCommand mySqlCommand = new MySqlCommand(comando, mySqlConnection);
            DbDataReader mySqlDataReader = await mySqlCommand.ExecuteReaderAsync();

            while (mySqlDataReader.Read())
            {
                jogo = new Jogo
                {
                    Id = (Guid)mySqlDataReader["Id"],
                    Nome = (string)mySqlDataReader["Nome"],
                    Produtora = (string)mySqlDataReader["Produtora"],
                    Preco = (double)mySqlDataReader["Preco"]
                };
            }

            await mySqlConnection.CloseAsync();

            return jogo;
        }

        public async Task<List<Jogo>> Obter(string nome, string produtora)
        {
            var jogos = new List<Jogo>();

            var comando = $"select * from Jogos where Nome = '{nome}' and Produtora = '{produtora}'";

            await mySqlConnection.OpenAsync();
            MySqlCommand mySqlCommand = new MySqlCommand(comando, mySqlConnection);
            DbDataReader mySqlDataReader = await mySqlCommand.ExecuteReaderAsync();

            while (mySqlDataReader.Read())
            {
                jogos.Add(new Jogo
                {
                    Id = (Guid)mySqlDataReader["Id"],
                    Nome = (string)mySqlDataReader["Nome"],
                    Produtora = (string)mySqlDataReader["Produtora"],
                    Preco = (double)mySqlDataReader["Preco"]
                });
            }

            await mySqlConnection.CloseAsync();

            return jogos;
        }

        public async Task Inserir(Jogo jogo)
        {
            var comando = $"insert Jogos (Id, Nome, Produtora, Preco) values ('{jogo.Id}', '{jogo.Nome}', '{jogo.Produtora}', {jogo.Preco.ToString().Replace(",", ".")})";

            await mySqlConnection.OpenAsync();
            MySqlCommand mySqlCommand = new MySqlCommand(comando, mySqlConnection);
            mySqlCommand.ExecuteNonQuery();
            await mySqlConnection.CloseAsync();
        }

        public async Task Atualizar(Jogo jogo)
        {
            var comando = $"update Jogos set Nome = '{jogo.Nome}', Produtora = '{jogo.Produtora}', Preco = {jogo.Preco.ToString().Replace(",", ".")} where Id = '{jogo.Id}'";

            await mySqlConnection.OpenAsync();
            MySqlCommand mySqlCommand = new MySqlCommand(comando, mySqlConnection);
            mySqlCommand.ExecuteNonQuery();
            await mySqlConnection.CloseAsync();
        }

        public async Task Remover(Guid id)
        {
            var comando = $"delete from Jogos where Id = '{id}'";

            await mySqlConnection.OpenAsync();
            MySqlCommand mySqlCommand = new MySqlCommand(comando, mySqlConnection);
            mySqlCommand.ExecuteNonQuery();
            await mySqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            mySqlConnection?.Close();
            mySqlConnection?.Dispose();
        }
    }
}
