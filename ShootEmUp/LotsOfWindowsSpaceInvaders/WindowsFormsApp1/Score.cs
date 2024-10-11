using System;
using System.Drawing;
using System.Windows.Forms;

namespace LotOfWindowsSpaceInvader
{
    public class HUDControl : UserControl
    {
        private Label _hudLabel; // Label to display score and lives
        private int _scoreValue; // Holds the score value
        private int _livesValue; // Holds the lives value
        private Timer _textMoveTimer; // Timer to move the text
        private int _textMoveSpeed = 2; // Speed of text movement
        private bool _movingRight = true;

        public HUDControl()
        {
            this.BackColor = Color.Black;
            this.Size = new Size(300, 35); // Adjust width to fit both score and lives

            // Initialize and configure the HUD label
            _hudLabel = new Label();
            _hudLabel.ForeColor = Color.White; // Set text color to white
            _hudLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            _hudLabel.Dock = DockStyle.Fill; // Fill the entire user control
            _hudLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Add label to the user control
            this.Controls.Add(_hudLabel);

            // Initialize score and lives values
            _scoreValue = 0;
            _livesValue = 3; // Set initial lives
            UpdateHUDDisplay();

            _textMoveTimer = new Timer();
            _textMoveTimer.Interval = 20; // Interval to move the text
            _textMoveTimer.Tick += MoveWindow; // Method to call on each tick
            _textMoveTimer.Start(); // Start the timer
        }

        // Method to increment the score by 1
        public void IncreaseScore()
        {
            _scoreValue += 1;
            UpdateHUDDisplay();
        }

        // Method to decrease lives by 1
        public void DecreaseLives()
        {
            if (_livesValue > 0)
            {
                _livesValue -= 1;
                UpdateHUDDisplay();
            }
        }

        // Updates the HUD display
        private void UpdateHUDDisplay()
        {
            _hudLabel.Text = $"Score: {_scoreValue}   Lives: {_livesValue}"; // Display both score and lives in one line
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
    }
}
