using System;
using System.Drawing;
using System.Windows.Forms;

namespace LotOfWindowsSpaceInvader
{
    public class GameOverWindow : Form
    {
        private Label _messageLabel;

        public GameOverWindow(string message)
        {
            InitializeComponent();
            SetupUI(message);
        }

        //assets de les fenetre
        private void InitializeComponent()
        {
            this.StartPosition = FormStartPosition.CenterScreen; 
            this.Size = new Size(300, 150); 
            this.FormBorderStyle = FormBorderStyle.FixedDialog; 
            this.MaximizeBox = false;
            this.MinimizeBox = false; 
            this.BackColor = Color.Black; 
        }

        //setup le ui
        private void SetupUI(string message)
        {
            _messageLabel = new Label
            {
                Text = message,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 16, FontStyle.Bold), 
                ForeColor = Color.White
            };

            this.Controls.Add(_messageLabel); //met le message 
        }
    }
}