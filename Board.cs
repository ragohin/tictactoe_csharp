using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe_windowsapp
{
    public class Board
    {
        private List<PlayerType> _tiles;
        private int _numBlankTiles;

        public Board()
        {
            _numBlankTiles = 9;
            _tiles = new List<PlayerType>();
            for (int i = 0; i < 9; ++i)
                _tiles.Add(PlayerType.None);
        }
        public Board(Board board)
        {
            _tiles = new List<PlayerType>(board.GetBoard());
            _numBlankTiles = board.GetNumBlankTiles();
        }

        public int GetNumBlankTiles()
        {
            return _numBlankTiles;
        }

        public List<PlayerType> GetBoard()
        {
            return _tiles;
        }
        public void SetTile(int location, PlayerType playerType)
        {
            _tiles[location] = playerType;
            ++_numBlankTiles;
        }

        public bool ValidMove(int index)
        {
            if (index < 0 || index > _tiles.Count() - 1)
                return false;
            if (_tiles[index] == PlayerType.None)
                return true;
            return false;
        }

        /// <summary>
        /// Checks board for any winner.
        /// </summary>
        /// <returns></returns>
        public bool CheckForWin()
        {
            return CheckForWinOrLossHelper() != PlayerType.None;
        }

        /// <summary>
        /// Checks board to see if "playerType" has won.
        /// </summary>
        /// <returns></returns>
        public bool CheckForWin(PlayerType playerType)
        {
            return CheckForWinOrLossHelper() == playerType;
        }
        public bool CheckForWin(PlayerType playerType, List<PlayerType> tilesCopy)
        {
            return CheckForWinOrLossHelper(tilesCopy) == playerType;
        }

        public PlayerType CheckForWinOrLossHelper()
        {
            int sum = 0;
            // check rows
            int tempSum = (int)_tiles[0] + (int)_tiles[1] + (int)_tiles[2];
            if (Math.Abs(tempSum) > Math.Abs(sum))
                sum = tempSum;
            tempSum = (int)_tiles[3] + (int)_tiles[4] + (int)_tiles[5];
            if (Math.Abs(tempSum) > Math.Abs(sum))
                sum = tempSum;
            tempSum = (int)_tiles[6] + (int)_tiles[7] + (int)_tiles[8];
            if (Math.Abs(tempSum) > Math.Abs(sum))
                sum = tempSum;

            // check columns
            tempSum = (int)_tiles[0] + (int)_tiles[3] + (int)_tiles[6];
            if (Math.Abs(tempSum) > Math.Abs(sum))
                sum = tempSum;
            tempSum = (int)_tiles[1] + (int)_tiles[4] + (int)_tiles[7];
            if (Math.Abs(tempSum) > Math.Abs(sum))
                sum = tempSum;
            tempSum = (int)_tiles[2] + (int)_tiles[5] + (int)_tiles[8];
            if (Math.Abs(tempSum) > Math.Abs(sum))
                sum = tempSum;

            // check diagonals
            tempSum = (int)_tiles[0] + (int)_tiles[4] + (int)_tiles[8];
            if (Math.Abs(tempSum) > Math.Abs(sum))
                sum = tempSum;
            tempSum = (int)_tiles[2] + (int)_tiles[4] + (int)_tiles[6];
            if (Math.Abs(tempSum) > Math.Abs(sum))
                sum = tempSum;

            if (sum == 3)
                return PlayerType.X;
            if (sum == -3)
                return PlayerType.O;

            return PlayerType.None;
        }

        public PlayerType CheckForWinOrLossHelper(List<PlayerType> tilesCopy)
        {
            int sum = 0;
            // check rows
            int tempSum = (int)tilesCopy[0] + (int)tilesCopy[1] + (int)tilesCopy[2];
            if (Math.Abs(tempSum) > Math.Abs(sum))
                sum = tempSum;
            tempSum = (int)tilesCopy[3] + (int)tilesCopy[4] + (int)tilesCopy[5];
            if (Math.Abs(tempSum) > Math.Abs(sum))
                sum = tempSum;
            tempSum = (int)tilesCopy[6] + (int)tilesCopy[7] + (int)tilesCopy[8];
            if (Math.Abs(tempSum) > Math.Abs(sum))
                sum = tempSum;

            // check columns
            tempSum = (int)tilesCopy[0] + (int)tilesCopy[3] + (int)tilesCopy[6];
            if (Math.Abs(tempSum) > Math.Abs(sum))
                sum = tempSum;
            tempSum = (int)tilesCopy[1] + (int)tilesCopy[4] + (int)tilesCopy[7];
            if (Math.Abs(tempSum) > Math.Abs(sum))
                sum = tempSum;
            tempSum = (int)tilesCopy[2] + (int)tilesCopy[5] + (int)tilesCopy[8];
            if (Math.Abs(tempSum) > Math.Abs(sum))
                sum = tempSum;

            // check diagonals
            tempSum = (int)tilesCopy[0] + (int)tilesCopy[4] + (int)tilesCopy[8];
            if (Math.Abs(tempSum) > Math.Abs(sum))
                sum = tempSum;
            tempSum = (int)tilesCopy[2] + (int)tilesCopy[4] + (int)tilesCopy[6];
            if (Math.Abs(tempSum) > Math.Abs(sum))
                sum = tempSum;

            if (sum == 3)
                return PlayerType.X;
            if (sum == -3)
                return PlayerType.O;

            return PlayerType.None;
        }

        /// <summary>
        /// Returns true if there are no more blank spaces on the board. 
        /// </summary>
        /// <returns></returns>
        public bool CheckForCatsGame()
        {
            return !_tiles.Contains(PlayerType.None);
        }

        public int Is2InRow(int location, PlayerType playerType)
        {
            //def is2inRow(self, matrixCopy, location): //if playing the current location gives the comp a 2 in a row (NOT NECESSARILY TWO NEXT TO EACHOTHER), it 
            if (ValidMove(location))
            {
                var tilesCopy = new List<PlayerType>(_tiles);
                tilesCopy[location] = playerType;
                foreach (var win in PossibleWins.GetPossibleWins())
                {
                    if (win.Contains(location))
                    {
                        int sum = 0;
                        int space2play = -1;
                        foreach (var space in win)
                        {
                            if (_tiles[space] == 0)
                                space2play = space;

                            sum += (int)_tiles[space];
                        }
                        //sum is equal to 2 bc in the Python version, computer tiles are each 1 (change this so comp can be +1 or -1)
                        if (sum == 2 && space2play != -1)
                            return space2play;
                    }
                }
            }
            return -1;
        }

        public int CountHumForks(List<int> forks, PlayerType playerType)
        {
            //def countHumForks(self, matrixCopy, forks): //this will count the number of forks human can make
            int counter = 0;
            foreach (var fork in forks)
            {
                var tilesCopy = new List<PlayerType>(_tiles);
                if (TryFork(fork, playerType))
                    ++counter;
            }
            return counter;
        }

        /*
         * Create templates "Try()" function that serves as a helper for all Try... methods
         * This will check if its a valid move before executing any code
         * Try(try type)
         * {
         *      check if valid
         *      TryCorner
         *      TryWin
         * }
         */
        public bool TryWin(int location, PlayerType playerType)
        {
            //def tryWin(self, matrixCopy, location, CorH):
            if (ValidMove(location))
            {
                var tilesCopy = new List<PlayerType>(_tiles);// instead of copying entire board, just change one square, then change it back
                tilesCopy[location] = playerType;
                return CheckForWin(playerType);
            }
            return false;
        }

        public bool TryWin(List<PlayerType> tilesCopy, int location, PlayerType playerType)
        {
            //def tryWin(self, matrixCopy, location, CorH):
            if (ValidMove(location))
            {
                tilesCopy[location] = playerType;
                return CheckForWin(playerType);
            }
            return false;
        }

        public bool TryWin_Block(int location, PlayerType playerType)
        {
            //def tryWin_Block(self, matrixCopy, location, CorH):
            if (ValidMove(location))
            {
                var tilesCopy = new List<PlayerType>(_tiles);
                tilesCopy[location] = playerType;
                return CheckForWin(playerType);
            }
            return false;
        }

        public bool TryFork(int location, PlayerType playerType)
        {
            //def tryFork(self, matrixCopy, location, CorH)://CorH means try to find fork for the computer or the human (allows tryFork to be used in 
            if (ValidMove(location))
            {
                var tilesCopy = new List<PlayerType>(_tiles);
                tilesCopy[location] = playerType;

                if (!TryWin(tilesCopy, location, playerType))
                {
                    int winCounter = 0;
                    for (int loc = 0; loc < tilesCopy.Count(); ++loc) //loop through every possible space
                    {
                        if (ValidMove(loc))
                        {
                            var tilesCopy2 = new List<PlayerType>(tilesCopy);
                            tilesCopy2[location] = playerType;
                            if (CheckForWin(playerType, tilesCopy2))
                                winCounter += 1;
                        }
                    }
                    if (winCounter == 2) //should this be more than 2?
                        return true;
                }
            }
            return false;
        }

        public bool TryCenter(int location)
        {
            //def tryCenter(self, location):
            if (ValidMove(location))
            {
                return location == 4;
            }
            return false;
        }

        public bool TryCorner_Opp(int location, PlayerType playerType)
        {
            //def tryCorner_Opp(self, matrixCopy, location): 
            if (ValidMove(location))
            {
                if (location == 0 || location == 2 || location == 6 || location == 8) //if its a corner
                {
                    if (location == 0) //check to see if opponent is in each opposite corner
                        return _tiles[8] == (PlayerType)((int)playerType * -1);
                    else if (location == 2)
                        return _tiles[6] == (PlayerType)((int)playerType * -1);
                    else if (location == 6)
                        return _tiles[2] == (PlayerType)((int)playerType * -1);
                    else
                        return _tiles[0] == (PlayerType)((int)playerType * -1);
                }
            }
            return false;
        }

        public bool TryCorner(int location)
        {
            if (ValidMove(location))
            {
                return (location == 0 || location == 2 || location == 6 || location == 8);//if its a corner
            }
            return false;
        }

        public bool TrySide(int location)
        {
            if (ValidMove(location))
            {
                return (location == 1 || location == 3 || location == 5 || location == 7);
            }
            return false;
        }
    }
}

