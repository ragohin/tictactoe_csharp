using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe_windowsapp
{
    public enum PlayerType
    {
        X = 1, O = -1, None = 0
    }
    public enum TileLocation
    {
        None = -1,
        TopLeft = 0,
        TopMiddle,
        TopRight,
        MiddleLeft,
        Center,
        MiddleRight,
        BottomLeft,
        BottomMiddle,
        BottomRight
    }
    public class Game
    {
        private Board _board;//eventually create a Board class that contains member variables for the current forks
        private Human _human;
        private Computer _computer;

        public event EventHandler<PlayerType> WinnerFound;
        public event EventHandler<PlayerType> CatsGame;
        protected virtual void OnWinnerFound(EventArgs e, PlayerType playerType)
        {
            EventHandler<PlayerType> handler = WinnerFound;
            handler?.Invoke(this, playerType);
        }

        protected virtual void OnCatsGame(EventArgs e, PlayerType playerType)
        {
            EventHandler<PlayerType> handler = CatsGame;
            handler?.Invoke(this, playerType);
        }

        public Game(Human human)
        {
            _human = human;

            PlayerType computerType = (PlayerType)((int)_human.Type * -1);
            _computer = new Computer(computerType);

            _board = new Board();
        }

        //error here, need to repeatedly check for cats game/win
        public TileLocation PlayMove(PlayerType playerType, TileLocation tileLocation = TileLocation.None)
        {
            if (_board.CheckForCatsGame())
            {
                var handler = CatsGame;
                handler(this, playerType);
            }
            else if (_board.CheckForWin())
            {
                if (_board.CheckForWin(_human.Type))
                {
                    var handler = WinnerFound;
                    handler(this, _human.Type);
                }
                else
                {
                    var handler = WinnerFound;
                    handler(this, _computer.Type);
                }
            }
            else
            {
                if (playerType == _human.Type)
                {
                    PlayHumanMove(tileLocation);
                    return tileLocation;
                }
                else
                {
                    return PlayComputerMove();
                }
            }
            return TileLocation.None;
        }

        public void PlayHumanMove(TileLocation tileLocation)
        {
            if (_board.ValidMove((int)tileLocation))
            {
                UpdateBoard(_human.Type, (int)tileLocation);
                //PlayComputerMove();
            }
        }

        public TileLocation PlayComputerMove()
        {
            int computerMove = _computer.PlayTurn(_board);
            //if (_board.ValidMove(computerMove))
            //{
                UpdateBoard(_computer.Type, computerMove);
            //}
            return (TileLocation)computerMove;
        }

        /// <summary>
        /// Updates the board with a player's selected move.
        /// </summary>
        /// <param name="indexMove"></param>
        public void UpdateBoard(PlayerType playerType, int indexOfMove)
        {
            _board.SetTile(indexOfMove, playerType);
        }

        public void DisplayBoard()
        {
            string row1 = "";
            string row2 = "";
            string row3 = "";
            for (int i = 0; i < _board.GetBoard().Count(); ++i)
            {
                string space = _board.GetBoard()[i].ToString();
                if (_board.GetBoard()[i] == PlayerType.None)
                    space = " ";

                if (i < 3)
                    row1 = row1 + (space + ' ') + " |";
                else if (i > 2 && i < 6)
                    row2 = row2 + (space + ' ') + " |";
                else if (i > 5)
                    row3 = row3 + (space + ' ') + " |";
            }
            Console.WriteLine('|' + row1);
            Console.WriteLine('|' + row2);
            Console.WriteLine('|' + row3);
        }
    }
}
