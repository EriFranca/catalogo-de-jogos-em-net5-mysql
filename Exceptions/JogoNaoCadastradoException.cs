using System;

namespace catalogoJogosAPI.Exceptions
{
    public class JogoNaoCadastradoException : Exception
    {
        public JogoNaoCadastradoException()
            : base("Jogo nao está cadastrado no BD")
        {
            
        }
    }
}