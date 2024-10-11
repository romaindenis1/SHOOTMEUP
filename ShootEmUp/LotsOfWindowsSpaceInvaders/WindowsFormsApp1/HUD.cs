using System;
using System.Drawing;
using System.Windows.Forms;

namespace LotOfWindowsSpaceInvader
{
    public class HUD : Form
    {
        private Label _hudLabel;                                    // label qui display
        private int _scoreValue;                                    // la valeur du score
        private int _livesValue;                                    // valeurs des vies
        private Timer _textMoveTimer;                               // Timer pour bouger le texte
        private int _textMoveSpeed = 2;                             // vitesse
        private bool _movingRight = true;                           //si ca bouge vers la droite au debut ou pas

        public HUD()
        {
            this.FormBorderStyle = FormBorderStyle.None;

            this.BackColor = Color.Black;
            this.Size = new Size(225, 35); // taille

            // couleurs ect
            _hudLabel = new Label();
            _hudLabel.ForeColor = Color.White; 
            _hudLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            _hudLabel.Dock = DockStyle.Fill; 
            _hudLabel.TextAlign = ContentAlignment.MiddleCenter;
            

            // Add label to the user control
            this.Controls.Add(_hudLabel);

            _scoreValue = 0;                                            //score de base
            _livesValue = 3;                                            //vie de base
            UpdateHUDDisplay();

            //timer
            _textMoveTimer = new Timer();
            _textMoveTimer.Interval = 20; 
            _textMoveTimer.Tick += MoveWindow; 
            _textMoveTimer.Start(); 
        }

        //met un score de plus
        public void IncreaseScore()
        {
            _scoreValue += 1;
            UpdateHUDDisplay();
        }

        //enleve 1 vie
        public void DecreaseLives()
        {
            if (_livesValue > 0)
            {
                _livesValue -= 1;
                UpdateHUDDisplay();
            }
        }

        //update la hud
        private void UpdateHUDDisplay()
        {
            _hudLabel.Text = $"Score: {_scoreValue}   Lives: {_livesValue}"; //display
        }

        private void MoveWindow(object sender, EventArgs e)
        {
            // Move horizontally
            if (_movingRight)
            {
                this.Left += _textMoveSpeed;
                if (this.Right >= Screen.PrimaryScreen.WorkingArea.Width) //change de direction
                    _movingRight = false;
            }
            else
            {
                this.Left -= _textMoveSpeed;
                if (this.Left <= 0) // change de direction
                    _movingRight = true;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // HUDControl
            // 
            this.Name = "HUDControl";
            this.Load += new System.EventHandler(this.HUDControl_Load);
            this.ResumeLayout(false);

        }

        private void HUDControl_Load(object sender, EventArgs e)
        {

        }
    }
}
