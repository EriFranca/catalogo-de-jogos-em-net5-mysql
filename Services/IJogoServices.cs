using System.Collections.Generic;
using System.Threading.Tasks;
using catalogoJogosAPI.ViewModel;
using catalogoJogosAPI.InputModel;
using System;

namespace catalogoJogosAPI.Services
{
    public interface IJogoServices : IDisposable
    {
        public Task<List<JogoViewModel>> Obter(int pagina, int quantidade);
        public Task<JogoViewModel> Obter(Guid id);
        public Task<JogoViewModel> Inserir(JogoInputModel jogo);
        public Task Atualizar(Guid id, JogoInputModel jogo);
        public Task Atualizar(Guid id, double preco);
        public Task Remover(Guid id);

    }
}