using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace ShootEmUp
{
    public partial class Form1 : Form
    {
        private MoveHelper moveHelper;
        private List<Invader> invaders;
        private List<Bullet> bullets;
        private System.Windows.Forms.Timer gameTimer;

        public Form1()
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;

            moveHelper = new MoveHelper(375); 
            bullets = new List<Bullet>();     

            
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 16; 
            gameTimer.Tick += GameTimer_Tick; 
            gameTimer.Start();

            invaders = new List<Invader>();
            for (int i = 0; i < 10; i++)
            {
                Invader invader = new Invader(50 + i * 30, 50, 2);
                invaders.Add(invader);
                this.Controls.Add(invader.GetPictureBox());
            }
        }

        public void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int shipX = moveHelper.GetX();
            const int shipY = 350; 

            
            if (e.KeyCode == Keys.D)
            {
                shipX += 5;
            }

            
            if (e.KeyCode == Keys.A)
            {
                shipX -= 5;
            }

            moveHelper.SetX(shipX);
            pictureBox1.Location = new Point(shipX, shipY); 

            if (e.KeyCode == Keys.Space)
            {
                Bullet newBullet = new Bullet(moveHelper, shipY);
                bullets.Add(newBullet);
                this.Controls.Add(newBullet.GetPictureBox()); 
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                Bullet bullet = bullets[i];
                bullet.Move();

                
                if (bullet.IsOutOfBounds(this.Height))
                {
                    this.Controls.Remove(bullet.GetPictureBox()); 
                    bullets.RemoveAt(i); 
                }

                
            }
            foreach (Invader invader in invaders)
            {
                invader.Move();
                if (invader.IsOutOfBounds(this.Height))
                {
                    // handle invader going off screen
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
