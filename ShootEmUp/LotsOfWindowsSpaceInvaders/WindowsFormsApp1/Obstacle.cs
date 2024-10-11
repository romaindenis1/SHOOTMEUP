using System.Drawing;
using System.Windows.Forms;
using System;
using WindowsFormsApp1;

namespace LotOfWindowsSpaceInvader
{
    public class Obstacle : Form
    {
        private PictureBox _obstacleImage;
        private Timer _moveTimer;
        private int _moveSpeed = 5; // vitesse oublie de change aussi
        private bool _movingRight;

        public Obstacle(string imagePath, bool movingRight)
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

            this.Size = _obstacleImage.Image.Size;
            this.MinimumSize = _obstacleImage.Size;
            this.MaximumSize = _obstacleImage.Size;

            _movingRight = movingRight;
            SetInitialPosition();

            _moveTimer = new Timer
            {
                Interval = 50 // oublie pas de changer ca
            };
            _moveTimer.Tick += MoveObstacle;
            _moveTimer.Start();

            this.Show();
        }

        private void SetInitialPosition()
        {
            if (_movingRight)
            {
                this.Location = new Point(-this.Width, new Random().Next(50, Screen.PrimaryScreen.WorkingArea.Height - this.Height - 50));
            }
            else
            {
                this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width, new Random().Next(50, Screen.PrimaryScreen.WorkingArea.Height - this.Height - 50));
            }
        }

        private void MoveObstacle(object sender, EventArgs e)
        {
            if (_movingRight)
            {
                this.Left += _moveSpeed;
                if (this.Left > Screen.PrimaryScreen.WorkingArea.Width)
                {
                    this.Close();
                }
            }
            else
            {
                this.Left -= _moveSpeed;
                if (this.Right < 0)
                {
                    this.Close();
                }
            }
        }

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

        public bool BlocksBullet(Bullet bullet)
        {
            return this.Bounds.IntersectsWith(bullet.Bounds);
        }

        public bool BlocksEvilBullet(EvilBullet evilBullet)
        {
            return this.Bounds.IntersectsWith(evilBullet.Bounds);
        }

        private void Obstacle_Load(object sender, EventArgs e)
        {

        }
    }
}