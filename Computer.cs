using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe_windowsapp
{
    class Computer : IPlayer
    {
        public PlayerType Type { get; set; }
        private PlayerType _opponentType { get; set; }

        public Computer(PlayerType playerType)
        {
            Type = playerType;
            _opponentType = (PlayerType)((int)Type * -1);
        }

        public int PlayTurn()
        {
            return 1;
        }

        // ranking of moves are from 1-9 (8 being the best, 1 being the worst possible move)
        //use priority queue instead of tuple
        public int PlayTurn(Board board)
        {
            // There seems to be a bit of a debate to whether starting with a blank tile or corner tile on the opening move it optimal.
            // I'm gonna start the computer off with a corner on the first move.
            if (board.GetNumBlankTiles() == 9)
            {
                List<int> corners = new List<int>() { 0, 2, 6, 8 };
                Random r = new Random();
                return corners[r.Next(corners.Count())]; // get random corner from list
            }

            //def compMove(self):
            Tuple<int, int> bestLocation = new Tuple<int, int>(9, 0); //this best move must be overwritten (best location of move, ranking of move in terms of best strategy)
            List<int> forks = new List<int>();
            for (int location = 0; location < board.GetBoard().Count(); ++location) //loop through matrix and check if any spot would result in one of these
            {
                if (board.ValidMove(location)) //if spot is available
                {
                    if (board.TryWin(location, Type)) //play space if it will result in win (3 in a row)
                        bestLocation = new Tuple<int, int>(location, 9);
                    else if (board.TryWin_Block(location, _opponentType)) //play space if you have to block opponent's win
                    {
                        if (bestLocation.Item2 < 8)
                            bestLocation = new Tuple<int, int>(location, 8);
                    }
                    else if (board.TryFork(location, Type)) //must be after at least 3 total game moves
                    {
                        if (bestLocation.Item2 < 7)
                            bestLocation = new Tuple<int, int>(location, 7);
                    }
                    if (board.TryFork(location, _opponentType)) //if there is only one fork to block, return true
                    {
                        forks.Add(location);
                        if (bestLocation.Item2 < 6)
                            bestLocation = new Tuple<int, int>(location, 6);
                    }
                    else if (board.TryCenter(location))
                    {
                        if (bestLocation.Item2 < 5)
                            bestLocation = new Tuple<int, int>(location, 5);
                    }
                    if (location != 5) //location isnt the center piece
                    {
                        if (board.TryCorner_Opp(location, Type)) //if opposite corner is taken by opponent, go there
                        {
                            if (bestLocation.Item2 < 4)
                                bestLocation = new Tuple<int, int>(location, 4);
                        }
                        else if (board.TryCorner(location)) //location must be empty corner
                        {
                            if (bestLocation.Item2 < 3)
                                bestLocation = new Tuple<int, int>(location, 3);
                        }
                        else if (board.TrySide(location)) //location must be an open middle side space MAYBE THIS SHOULD JUST BE ELSE
                        {
                            if (bestLocation.Item2 < 2)
                                bestLocation = new Tuple<int, int>(location, 2);
                        }
                    }

                    if (bestLocation.Item1 < 1)
                        bestLocation = new Tuple<int, int>(location, 1);
                }
            }
            if (bestLocation.Item2 == 6)
            {
                if (forks.Count() == 1) //if there is only one fork, simply block it (so that tryFork will become false)
                {
                    foreach (var win in PossibleWins.GetPossibleWins())
                    {
                        if (win.Contains(forks[0]))
                        {
                            foreach (var space in win) //try to play each space and see if one changes tryFork to false
                            {
                                if (board.ValidMove(space))
                                {
                                    var tilesCopy = new Board(board);
                                    tilesCopy.SetTile(space, Type);
                                    if (!tilesCopy.TryFork(bestLocation.Item1, _opponentType))
                                    {
                                        bestLocation = new Tuple<int, int>(space, 7);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                //this works better than instructions on Wikipedia because not only does it create a 2 in a row that forces the oppoent to defend,
                //but it also block the max amount of human forks
                else if (forks.Count() > 1) //if there is one or more fork, block a fork that gives comp 2 in a row that forces the opponent to block, but doesnt 
                {
                    SortedDictionary<int, int> numForksRemaining = new SortedDictionary<int, int>(); //stores dictionary where key is a location that gives comp two in a row and value is the number of forks blocked
                    for (int location = 0; location < board.GetBoard().Count(); ++location)
                    {
                        var tilesCopy = new List<PlayerType>(board.GetBoard());
                        var TwoInRow = board.Is2InRow(location, Type);
                        if (TwoInRow != -1)
                        {
                            numForksRemaining[location] = 0;
                            numForksRemaining[location] = board.CountHumForks(forks, _opponentType);
                        }
                    }
                    bestLocation = new Tuple<int, int>(numForksRemaining[0], 7);
                }
            }

            if (bestLocation.Item2 > 0 && bestLocation.Item2 < 10)
                return bestLocation.Item1;

            Console.WriteLine("ERROR");
            return -1;
        }
    }
}

