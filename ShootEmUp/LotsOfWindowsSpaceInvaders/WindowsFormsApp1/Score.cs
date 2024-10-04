using System;
using System.Drawing;
using System.Windows.Forms;

namespace LotOfWindowsSpaceInvader
{
    public class Score : Form
    {
        private Label _scoreLabel; // Label to display the score
        private int _scoreValue; // Holds the score value
        private Timer _textMoveTimer; // Timer to move the text
        private int _textMoveSpeed = 2; // Speed of text movement
        private bool _movingRight = true;
        public Score()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.Black;
            this.Size = new Size(150, 35);
            this.StartPosition = FormStartPosition.Manual; 
            this.Location = new Point(0, 7); // en haut a gauche

            // Initialize and configure label
            _scoreLabel = new Label();
            _scoreLabel.ForeColor = Color.White; // Set text color to white
            _scoreLabel.Font = new Font("Arial",16, FontStyle.Bold); //police
            _scoreLabel.Dock = DockStyle.Fill; // rend le text aussi grand que la fenetre
            _scoreLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Add label to the form
            this.Controls.Add(_scoreLabel);

            // Initialize score value and display it
            _scoreValue = 0;
            UpdateScoreDisplay();

            _textMoveTimer = new Timer();
            _textMoveTimer.Interval = 20; // Interval to move the text
            _textMoveTimer.Tick += MoveWindow; // Method to call on each tick
            _textMoveTimer.Start(); // Start the timer
        }

        // Method to increment the score by 1
        public void IncreaseScore()
        {
            _scoreValue += 1;
            UpdateScoreDisplay();
        }

        // Updates the score display
        private void UpdateScoreDisplay()
        {
            _scoreLabel.Text = "Score: " + _scoreValue.ToString();
        }
        private void MoveWindow(object sender, EventArgs e)
        {
            // Move horizontally
            if (_movingRight)
            {
                this.Left += _textMoveSpeed;
                if (this.Right >= Screen.PrimaryScreen.WorkingArea.Width) // If window reaches the right edge, change direction
                    _movingRight = false;
            }
            else
            {
                this.Left -= _textMoveSpeed;
                if (this.Left <= 0) // If window reaches the left edge, change direction
                    _movingRight = true;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Score
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "Score";
            this.Load += new System.EventHandler(this.Score_Load);
            this.ResumeLayout(false);

        }

        private void Score_Load(object sender, EventArgs e)
        {


        }
    }
}
