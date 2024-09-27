using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace ShootEmUp
{
    public partial class Form1 : Form
    {
        //instancie la class qui aide a bouger
        private MoveHelper moveHelper;
        //instancie la classe de ennemi
        private List<Invader> invaders;
        //instancie la classe de balles
        private List<Bullet> bullets;
        ////instancie un timer
        private System.Windows.Forms.Timer gameTimer;

        public Form1()
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;


            //donne le x de base
            moveHelper = new MoveHelper(375);

            //fait des balles
            bullets = new List<Bullet>();

            //setup du timer
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 16;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            //fait des ennemis
            invaders = new List<Invader>();
            for (int i = 0; i < 25; i++)
            {
                Invader invader = new Invader(50 + i * 30, 50, 2);
                invaders.Add(invader);
                this.Controls.Add(invader.GetPictureBox());
            }
        }

        //mouvement du vaisseau
        public void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int shipX = moveHelper.GetX();
            const int shipY = 350;

            if (e.KeyCode == Keys.D && shipX < 0)
            {
                shipX += 5;
            }

            if (e.KeyCode == Keys.A && shipX > 750)
            {
                shipX -= 5;
            }
            
            moveHelper.SetX(shipX);
            pictureBox1.Location = new Point(shipX, shipY);

            //pour tirer
            if (e.KeyCode == Keys.Space)
            {
                Bullet newBullet = new Bullet(moveHelper, shipY);
                bullets.Add(newBullet);
                this.Controls.Add(newBullet.GetPictureBox());
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            //bouge balles
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                Bullet bullet = bullets[i];
                bullet.Move();

                //enleve balle si elle sort de l'ecran
                if (bullet.IsOutOfBounds(this.Height))
                {
                    this.Controls.Remove(bullet.GetPictureBox());
                    bullets.RemoveAt(i);
                }
            }

            // bouge invaders
            for (int i = invaders.Count - 1; i >= 0; i--)
            {
                Invader invader = invaders[i];
                invader.Move();

                
                if (invader.IsOutOfBounds(this.Height))
                {
                    this.Controls.Remove(invader.GetPictureBox());
                    invaders.RemoveAt(i);
                }
            }

            //regarde si invader se fait touche par une balle
            CheckCollisions();
        }

        private void CheckCollisions()
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                Bullet bullet = bullets[i];
                Rectangle bulletRect = bullet.GetPictureBox().Bounds;

                for (int j = invaders.Count - 1; j >= 0; j--)
                {
                    Invader invader = invaders[j];
                    Rectangle invaderRect = invader.GetPictureBox().Bounds;

                    //check si balle touche enemi
                    if (bulletRect.IntersectsWith(invaderRect))
                    {
                        //enleve la balle et l'ennemi
                        this.Controls.Remove(bullet.GetPictureBox());
                        this.Controls.Remove(invader.GetPictureBox());
                        bullets.RemoveAt(i);
                        invaders.RemoveAt(j);

                        break;
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
