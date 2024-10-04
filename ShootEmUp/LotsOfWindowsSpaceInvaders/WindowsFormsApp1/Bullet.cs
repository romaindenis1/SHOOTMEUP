// Bullet.cs

using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
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

            // Load the bullet image
            PictureBox _bulletImage = new PictureBox();
            _bulletImage.Image = Image.FromFile(imagePath);
            _bulletImage.SizeMode = PictureBoxSizeMode.AutoSize;
            this.Controls.Add(_bulletImage);

            _bulletImage.Padding = new Padding(0);
            _bulletImage.Margin = new Padding(0);

            // Set the form's size to fit the picture box
            this.ClientSize = _bulletImage.Size;

            this.MinimumSize = _bulletImage.Size;
            this.MaximumSize = _bulletImage.Size;

            //timer de balles 
            _bulletTimer = new Timer();
            _bulletTimer.Interval = 3;
            _bulletTimer.Tick += MoveBullet;
            _bulletTimer.Start();
        }

        // Move the bullet upwards
        private void MoveBullet(object sender, EventArgs e)
        {
            this.Top -= _speed; 
            if (this.Top + this.Height < 0) 
            {
                _bulletTimer.Stop();
                this.Close(); 
            }
        }

        // Move the bullet downward
        private void MoveEvilBullet(object sender, EventArgs e)
        {
            this.Top += _speed;
            if (this.Top + this.Height == 0)
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
