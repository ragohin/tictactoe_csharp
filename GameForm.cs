using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tictactoe_windowsapp
{
    public partial class GameForm : Form
    {
        private Game _game;
        private PlayerType _humanType;
        private PlayerType _computerType;
        private bool _humanGoesFirst;

        private bool _running = false;

        public GameForm(PlayerType humanType, bool humanGoesFirst)
        {
            InitializeComponent();
            _humanType = humanType;
            _computerType = (PlayerType)((int)humanType * -1);

            _humanGoesFirst = humanGoesFirst;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                _game = new Game(new Human(_humanType));
                _game.WinnerFound += HandleWinnerFound;
                _game.CatsGame += HandleCatsGame;

                if (!_humanGoesFirst)
                {
                    ExecuteComputerMove(sender, e);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void HandleWinnerFound(object sender, PlayerType playerType)
        {
            string winner = (playerType == _humanType) ? "Human" : "Computer";
            label1.Text = "Winner is " + winner + "!";
        }
        private void HandleCatsGame(object sender, PlayerType playerType)
        {
            label1.Text = "Cat's game!";
        }

        private void ExecuteMoves(object sender, EventArgs e, TileLocation tileLocation)
        {
            if (!_running)
            {
                _running = true;

                _game.PlayMove(_humanType, tileLocation);
                GetButtomFromTileLocation(tileLocation).Text =  _humanType.ToString();

                ExecuteComputerMove(sender, e);

                _running = false;
            }

            _game.DisplayBoardToDebug();
        }

        private void ExecuteComputerMove(object sender, EventArgs e)
        {
            TileLocation computerMove = _game.PlayMove(_computerType);

            GetButtomFromTileLocation(computerMove).Text = _computerType.ToString();
        }

        private ref Button GetButtomFromTileLocation(TileLocation tileLocation)
        {
            if (tileLocation == TileLocation.TopLeft)
                return ref ButtonTopLeft;
            else if (tileLocation == TileLocation.TopMiddle)
                return ref ButtonTopMiddle;
            else if (tileLocation == TileLocation.TopRight)
                return ref ButtonTopRight;

            else if (tileLocation == TileLocation.MiddleLeft)
                return ref ButtonMiddleLeft;
            else if (tileLocation == TileLocation.Center)
                return ref ButtonCenter;
            else if (tileLocation == TileLocation.MiddleRight)
                return ref ButtonMiddleRight;

            else if (tileLocation == TileLocation.BottomLeft)
                return ref ButtonBottomLeft;
            else if (tileLocation == TileLocation.BottomMiddle)
                return ref ButtonBottomMiddle;
            else if (tileLocation == TileLocation.BottomRight)
                return ref ButtonBottomRight;

            return ref ButtonBottomRight;
        }

        private void ButtonTopLeft_Click(object sender, EventArgs e)
        {
            ExecuteMoves(sender, e, TileLocation.TopLeft);
        }

        private void ButtonTopMiddle_Click(object sender, EventArgs e)
        {
            ExecuteMoves(sender, e, TileLocation.TopMiddle);
        }

        private void ButtonTopRight_Click(object sender, EventArgs e)
        {
            ExecuteMoves(sender, e, TileLocation.TopRight);
        }

        private void ButtonMiddleLeft_Click(object sender, EventArgs e)
        {
            ExecuteMoves(sender, e, TileLocation.MiddleLeft);
        }

        private void ButtonCenter_Click(object sender, EventArgs e)
        {
            ExecuteMoves(sender, e, TileLocation.Center);
        }

        private void ButtonMiddleRight_Click(object sender, EventArgs e)
        {
            ExecuteMoves(sender, e, TileLocation.MiddleRight);
        }

        private void ButtonBottomLeft_Click(object sender, EventArgs e)
        {
            ExecuteMoves(sender, e, TileLocation.BottomLeft);
        }

        private void ButtonBottomMiddle_Click(object sender, EventArgs e)
        {
            ExecuteMoves(sender, e, TileLocation.BottomMiddle);
        }

        private void ButtonBottomRight_Click(object sender, EventArgs e)
        {
            ExecuteMoves(sender, e, TileLocation.BottomRight);
        }
    }
}
