using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace игра
{
    public class GameException : Exception
    {
        public GameException(): base("Game problem"){ }
        public GameException(string message) : base(message) { }
    }
}
