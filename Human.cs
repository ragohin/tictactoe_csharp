using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe_windowsapp
{
    public class Human : IPlayer
    {
        public PlayerType Type { get; set; }

        public Human(PlayerType playerType)
        {
            Type = playerType;
        }
    }
}
