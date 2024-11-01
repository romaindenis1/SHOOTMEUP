using System;
using System.Drawing;
using System.Windows.Forms;

namespace LotOfWindowsSpaceInvader
{
    public class HUD : Form
    {
        private Label _hudLabel;                                    // label qui display
        private Timer _textMoveTimer;                               // Timer pour bouger le texte

        public int _scoreValue;                                     // la valeur du score
        private int _textMoveSpeed = 2;                             // vitesse
        public int _livesValue { get; private set; }                // valeurs des vies
       
        private bool _movingRight = true;                           //si ca bouge vers la droite au debut ou pas

        public HUD()
        {
            //bla bla bla
            this.FormBorderStyle = FormBorderStyle.None;

            this.BackColor = Color.Black;
            this.Size = new Size(225, 35);

            // couleurs ect
            _hudLabel = new Label();
            _hudLabel.ForeColor = Color.White; 
            _hudLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            _hudLabel.Dock = DockStyle.Fill; 
            _hudLabel.TextAlign = ContentAlignment.MiddleCenter;
            
            this.Controls.Add(_hudLabel);

            _scoreValue = 0;                                            //score de base
            _livesValue = 10;                                           //vie de base
            UpdateHUDDisplay();                                         //met a jour le hud

            //setup timer qui bouge le texte
            _textMoveTimer = new Timer();
            _textMoveTimer.Interval = 20; 
            _textMoveTimer.Tick += MoveWindow; 
            _textMoveTimer.Start();
        }

        /// <summary>
        /// Ajoute du score
        /// </summary>
        public void IncreaseScore()
        {
            _scoreValue += 1;
            UpdateHUDDisplay();
            
        }
        /// <summary>
        /// Enleve une vie
        /// </summary>
        public void DecreaseLives()
        {
            if (_livesValue > 0)
            {
                _livesValue -= 1;
                UpdateHUDDisplay();
            }
        }

        /// <summary>
        /// Donne le nombre de vies
        /// </summary>
        /// <returns>La vie</returns>
        public int GetLivesValue()
        {
            return _livesValue;
        }

        /// <summary>
        /// Update la hud
        /// </summary>
        public void UpdateHUDDisplay()
        {
            _hudLabel.Text = $"Score: {_scoreValue}   Lives: {_livesValue}"; //display
        }
        /// <summary>
        /// bouge la fenetre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Reset la hud
        /// </summary>
        public void ResetHUD()
        {
            _livesValue = 3; //Reset lives 
            _scoreValue = 0; //Reset score 
            UpdateHUDDisplay(); 
        }
        private void HUDControl_Load(object sender, EventArgs e)
        {

        }
    }
}
