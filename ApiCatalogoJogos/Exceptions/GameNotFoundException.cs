using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Exceptions
{
    public class GameNotFoundException:Exception
    {
        public GameNotFoundException():base("Esse jogo não está cadastrado")
        {

        }
    }
}
