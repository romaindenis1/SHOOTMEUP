using System.Drawing;
using System.Windows.Forms;
using System;
using WindowsFormsApp1;

namespace LotOfWindowsSpaceInvader
{
    public class Obstacle : Form
    {
        private PictureBox _obstacleImage;                                              //l'image de l'obstacle
        private Timer _moveTimer;                                                       //le timer pour que l'obstacle bouge
        private int _moveSpeed = 5;                                                     //vitesse de l'obstacle
        private bool _movingRight;                                                      //si l'obstacle bouge a droite ou pas

        public Obstacle(string imagePath, bool movingRight)
        {
            InitializeComponent();

            //setup
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

            //allume le timer
            _moveTimer = new Timer
            {
                Interval = 5
            };
            _moveTimer.Tick += MoveObstacle;
            _moveTimer.Start();

            this.Show();
        }
        /// <summary>
        /// Met la position de base de l'obstacle
        /// </summary>
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

        /// <summary>
        /// Bouger l'obstacle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Encore du setup
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
        /// <summary>
        /// Bool pour savoir si l'obstacle touche une balle
        /// </summary>
        /// <param name="bullet">La balle du joueur</param>
        /// <returns></returns>
        public bool BlocksBullet(Bullet bullet)
        {
            return this.Bounds.IntersectsWith(bullet.Bounds);
        }
        /// <summary>
        /// Bool pour savoir si l'obstacle touche une balle enemi
        /// </summary>
        /// <param name="evilBullet">La balles des enemis</param>
        /// <returns></returns>
        public bool BlocksEvilBullet(EvilBullet evilBullet)
        {
            return this.Bounds.IntersectsWith(evilBullet.Bounds);
        }

        private void Obstacle_Load(object sender, EventArgs e)
        {

        }
    }
}