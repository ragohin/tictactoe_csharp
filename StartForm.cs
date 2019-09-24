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
    public partial class StartForm : Form
    {
        struct GameSettings
        {
            public PlayerType HumanType;
            public bool HumanGoesFirst;
        }

        private GameSettings _gameSettings;

        public StartForm()
        {
            InitializeComponent();
            _gameSettings = new GameSettings();
            _gameSettings.HumanType = (PlayerType)Enum.Parse(typeof(PlayerType), this.button2.Text);
            _gameSettings.HumanGoesFirst = true;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (this.button2.Text == "O")
                this.button2.Text = "X";
            else
                this.button2.Text = "O";

            _gameSettings.HumanType = (PlayerType)Enum.Parse(typeof(PlayerType), this.button2.Text);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            _gameSettings.HumanGoesFirst = true;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            _gameSettings.HumanGoesFirst = false;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            GameForm gameForm = new GameForm(_gameSettings.HumanType, _gameSettings.HumanGoesFirst);
            gameForm.ShowDialog();
            this.Close();
        }
    }
}
