using catalogoJogosAPI.Entities;
using catalogoJogosAPI.Exceptions;
using catalogoJogosAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using catalogoJogosAPI.ViewModel;
using catalogoJogosAPI.InputModel;
using System.Linq;
using System;

namespace catalogoJogosAPI.Services
{
    // we access the DB Repository from this Service class 
    public class JogoServices : IJogoServices
    {
        private readonly IJogoRepository _jogoRepository;

        public JogoServices(IJogoRepository jogoRepo) {
            this._jogoRepository = jogoRepo;
        }

        public async Task<List<JogoViewModel>> Obter(int pagina, int quantidade) {
            var jogos = await _jogoRepository.Obter(pagina, quantidade);

            return jogos.Select(jogo => new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            }).ToList();
        }

        public async Task<JogoViewModel> Obter(Guid id) {

            var jogo = await _jogoRepository.Obter(id);

            if (jogo == null) throw new JogoNaoCadastradoException();

            return new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }

        public async Task<JogoViewModel> Inserir(JogoInputModel jogo) {
            var entidadeJogo = await _jogoRepository.Obter(jogo.Nome, jogo.Produtora);

            if (entidadeJogo.Count > 0) throw new JogoJaCadastradoException();

            var newJogo = new Jogo {
                Id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };

            await _jogoRepository.Inserir(newJogo);

            return new JogoViewModel
            {
                Id = newJogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }

        public async Task Atualizar(Guid id, JogoInputModel jogoInputModel) {

            var jogo = await _jogoRepository.Obter(id);
            if (jogo == null)   throw new JogoNaoCadastradoException();

            jogo.Nome = jogoInputModel.Nome;
            jogo.Produtora = jogoInputModel.Produtora;
            jogo.Preco = jogoInputModel.Preco;


            await _jogoRepository.Atualizar(jogo);
        }

        public async Task Atualizar(Guid id, double preco) {
            var jogo = await _jogoRepository.Obter(id);

            if (jogo == null) throw new JogoNaoCadastradoException();

            jogo.Preco = preco;
            await _jogoRepository.Atualizar(jogo);
        }

        public async Task Remover(Guid id) {

            var jogo = await _jogoRepository.Obter(id);
            if (jogo == null) throw new JogoNaoCadastradoException();

            await _jogoRepository.Remover(id);
        }

        public void Dispose()
        {
            _jogoRepository?.Dispose();
        }
    }
}