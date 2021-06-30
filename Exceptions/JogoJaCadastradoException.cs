using System;


namespace catalogoJogosAPI.Exceptions
{
    public class JogoJaCadastradoException : Exception
    {
        public JogoJaCadastradoException()
            : base("Este jogo já eciste no BD")
        {
            
        }
    }
}