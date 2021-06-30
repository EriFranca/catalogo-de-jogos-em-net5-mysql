
using catalogoJogosAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;


namespace catalogoJogosAPI.Repositories
{
    public interface IJogoRepository : IDisposable
    {
        Task<List<Jogo>> Obter(int pagina, int quantidade);
        Task<List<Jogo>> Obter(string nome, string produtora);
        Task<Jogo> Obter(Guid id);
        Task Inserir(Jogo jogo);
        Task Atualizar(Jogo jogo);
        Task Remover(Guid id);
    }
}