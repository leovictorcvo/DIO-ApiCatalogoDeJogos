using System;

namespace ApiCatalogoJogos.Exceptions
{
    public class GameAlreadyExistException: Exception
    {
        public GameAlreadyExistException(): base("Esse jogo já está cadastrado")
        {

        }
    }
}
