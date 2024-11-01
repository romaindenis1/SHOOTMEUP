/******************************************************************************
** PROGRAMME  Bullet.cs                                                      **
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

namespace LotOfWindowsSpaceInvader
{
    public class Bullet : Form
    {
        private Timer _bulletTimer; //timer pour bouger
        private int _speed = 20;  //vitesse de la balle

        public Bullet(string imagePath, Point initialPosition)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = initialPosition;
            this.BackColor = Color.Black;

            // pour la picturebox
            PictureBox _bulletImage = new PictureBox();
            _bulletImage.Image = Image.FromFile(imagePath);
            _bulletImage.SizeMode = PictureBoxSizeMode.AutoSize;
            this.Controls.Add(_bulletImage);

            _bulletImage.Padding = new Padding(0);
            _bulletImage.Margin = new Padding(0);

            // pour la taille
            this.ClientSize = _bulletImage.Size;

            this.MinimumSize = _bulletImage.Size;
            this.MaximumSize = _bulletImage.Size;

            //timer de balles 
            _bulletTimer = new Timer();
            _bulletTimer.Interval = 3;
            _bulletTimer.Tick += MoveBullet;
            _bulletTimer.Start();
        }

        // bouger la balle
        private void MoveBullet(object sender, EventArgs e)
        {
            this.Top -= _speed; 
            if (this.Top + this.Height < 0) 
            {
                _bulletTimer.Stop();
                this.Close(); 
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Bullet
            // 
            this.ClientSize = new System.Drawing.Size(1,1);
            this.Name = "Bullet";
            this.Load += new System.EventHandler(this.Bullet_Load);
            this.ResumeLayout(false);

        }

        private void Bullet_Load(object sender, EventArgs e)
        {

        }
    }
}
