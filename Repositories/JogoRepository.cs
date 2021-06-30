using catalogoJogosAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace catalogoJogosAPI.Repositories
{
    public class JogoRepository : IJogoRepository
    {

        private static Dictionary<Guid, Jogo> jogos = new Dictionary<Guid, Jogo>()
        {
            {Guid.Parse("fbb98c84-d53b-469d-a8f7-d953d9cf891f"), new Jogo{Id = Guid.Parse("fbb98c84-d53b-469d-a8f7-d953d9cf891f"), Nome = "MGS4", Produtora = "Kojima Productions", Preco = 120.50}},
            {Guid.Parse("fd4eb25c-bb92-4968-9e7f-3afbd03cdde6"), new Jogo{Id = Guid.Parse("fd4eb25c-bb92-4968-9e7f-3afbd03cdde6"), Nome = "Far Cry 2", Produtora = "Ubisoft montreal", Preco = 76.50}},
            {Guid.Parse("5dab940d-3fa3-4ff6-a568-ce9a922a81e8"), new Jogo{Id = Guid.Parse("5dab940d-3fa3-4ff6-a568-ce9a922a81e8"), Nome = "Gran Turismo 7", Produtora = "Poliphony Digital", Preco = 88.50}},
            {Guid.Parse("a2c1c7e0-f291-4141-a268-18b78f12ed7a"), new Jogo{Id = Guid.Parse("a2c1c7e0-f291-4141-a268-18b78f12ed7a"), Nome = "Battlefield 4", Produtora = "DICE", Preco = 28.50}},
            {Guid.Parse("9e779a44-1a28-40c5-b9e4-55df1080fe59"), new Jogo{Id = Guid.Parse("9e779a44-1a28-40c5-b9e4-55df1080fe59"), Nome = "The Witcher 3", Produtora = "CD Project Red", Preco = 56.50}},
            {Guid.Parse("cb63e3ce-f6c4-476b-a38d-a58a1fab6c3b"), new Jogo{Id = Guid.Parse("cb63e3ce-f6c4-476b-a38d-a58a1fab6c3b"), Nome = "GTA IV", Produtora = "Rockstar Games", Preco = 37.20}}
        };

        public Task<List<Jogo>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(jogos.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList() );
        }
        public Task<List<Jogo>> Obter(string nome, string produtora)
        {
            return Task.FromResult(jogos.Values.Where(jogo => jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora)).ToList());
        }
        public Task<Jogo> Obter(Guid id)
        {
            if (!jogos.ContainsKey(id)) return null;
            return Task.FromResult(jogos[id]);
        }
        public Task Inserir(Jogo jogo)
        {
            jogos.Add(jogo.Id, jogo);
            return Task.CompletedTask;
        }
        public Task Atualizar(Jogo jogo)
        {
            jogos[jogo.Id] = jogo;
            return Task.CompletedTask;
        }
        public Task Remover(Guid id)
        {
            jogos.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {

        }
    }
}
