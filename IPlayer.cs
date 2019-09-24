using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe_windowsapp
{
    interface IPlayer
    {
        PlayerType Type { get; set; }
    }
}
