using System;
using System.Drawing;
using System.Windows.Forms;

namespace LotOfWindowsSpaceInvader
{
    public class GameOverWindow : Form
    {
        private Label _messageLabel;                                        //le label avec le message

        public GameOverWindow(string message)
        {
            InitializeComponent();
            SetupUI(message);                                              //met le message
        }

        //assets de les fenetre
        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            //setup GameOverWindow
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(284, 111);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameOverWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.GameOverWindow_Load);
            this.ResumeLayout(false);

        }

        /// <summary>
        /// Setup le UI
        /// </summary>
        /// <param name="message">Le message de fin du jeu</param>
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

        private void GameOverWindow_Load(object sender, EventArgs e)
        {

        }
    }
}