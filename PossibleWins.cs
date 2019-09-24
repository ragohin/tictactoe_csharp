using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe_windowsapp
{
    static class PossibleWins
    {
        static List<List<int>> _possibleWinsList;
        static PossibleWins()
        {
            _possibleWinsList = new List<List<int>>()
            {
                new List<int>() { 0, 1, 2 },
                new List<int>() { 3, 4, 5 },
                new List<int>() { 6, 7, 8 },
                new List<int>() { 0, 3, 6 },
                new List<int>() { 1, 4, 7 },
                new List<int>() { 2, 5, 8 },
                new List<int>() { 0, 4, 8 },
                new List<int>() { 2, 4, 6 },
            };
        }
        public static List<List<int>> GetPossibleWins()
        {
            return _possibleWinsList;
        }
    }
}
