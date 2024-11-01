/******************************************************************************
** PROGRAMME  Obstacle.cs                                                    **
**                                                                           **
** Lieu      : ETML - section informatique                                   **
** Auteur    : Romain Denis                                                  **
** Date      : 01.11.24                                                      **
**                                                                           **
** Modifications                                                             **
**   Auteur  :                                                               **
**   Version :                                                               **
**   Date    :                                                               **
**   Raisons :                                                               **
**                                                                           **
**                                                                           **
******************************************************************************/

/******************************************************************************
** DESCRIPTION                                                               **
** Voire en bas                                                              **     
**                                                                           **
**                                                                           **
******************************************************************************/
using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace LotOfWindowsSpaceInvader
{
    public class Obstacle : Form
    {
        private PictureBox _obstacleImage;  // Image de l'obstacle

        // Event to notify Form1 when the obstacle is destroyed
        public event EventHandler ObstacleDestroyed;

        public Obstacle(string imagePath, int positionIndex)
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.BackColor = Color.Black;

            _obstacleImage = new PictureBox
            {
                Image = Image.FromFile(imagePath),
                SizeMode = PictureBoxSizeMode.AutoSize
            };
            this.Controls.Add(_obstacleImage);

            // Set the obstacle's size to match the image size
            this.Size = _obstacleImage.Image.Size;
            this.MinimumSize = _obstacleImage.Size;
            this.MaximumSize = _obstacleImage.Size;

            // Place obstacle at initial position across the screen based on positionIndex
            SetInitialPosition(positionIndex);

            this.Show();
        }

        /// <summary>
        /// Sets the initial position of the obstacle based on positionIndex.
        /// </summary>
        /// <param name="positionIndex">Position index to space obstacles across the screen.</param>
        private void SetInitialPosition(int positionIndex)
        {
            int spacing = Screen.PrimaryScreen.WorkingArea.Width / 4;
            int xPosition = spacing * (positionIndex + 1);
            int yPosition = 800;  // Sets obstacle higher on the screen

            this.Location = new Point(xPosition, yPosition);
        }

        /// <summary>
        /// Checks if the obstacle blocks a player's bullet. If hit, removes the obstacle.
        /// </summary>
        /// <param name="bullet">The player's bullet.</param>
        /// <returns>True if hit; otherwise, false.</returns>
        public bool BlocksBullet(Bullet bullet)
        {
            if (this.Bounds.IntersectsWith(bullet.Bounds))
            {
                RemoveObstacle();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if the obstacle blocks an enemy's bullet. If hit, removes the obstacle.
        /// </summary>
        /// <param name="evilBullet">The enemy's bullet.</param>
        /// <returns>True if hit; otherwise, false.</returns>
        public bool BlocksEvilBullet(EvilBullet evilBullet)
        {
            if (this.Bounds.IntersectsWith(evilBullet.Bounds))
            {
                RemoveObstacle();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes the obstacle from the game, clears resources, and notifies Form1.
        /// </summary>
        private void RemoveObstacle()
        {
            ObstacleDestroyed?.Invoke(this, EventArgs.Empty);  // Notify Form1 to remove the obstacle
            this.Controls.Clear();  // Clear controls like PictureBox
            this.Hide();            // Hide obstacle from the screen
            this.Dispose();         // Dispose to release resources
        }

        /// <summary>
        /// Sets up initial component configurations.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Obstacle
            // 
            this.ClientSize = new System.Drawing.Size(120, 50);
            this.Name = "Obstacle";
            this.Load += new System.EventHandler(this.Obstacle_Load);
            this.ResumeLayout(false);

        }

        private void Obstacle_Load(object sender, EventArgs e)
        {

        }
    }
}
