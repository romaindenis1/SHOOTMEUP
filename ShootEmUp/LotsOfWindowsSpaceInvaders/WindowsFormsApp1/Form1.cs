using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace LotOfWindowsSpaceInvader 
{
    public partial class Form1 : Form
    {
        private List<Invader> _invaders = new List<Invader>();                                              //liste de enemis
        private List<Bullet> _bullets = new List<Bullet>();                                                 //liste de balles
        private Timer _moveTimer;                                                                           //le timer pour bouger
        private int _moveSpeed = 20;                                                                        //la vitesse du vaisseau
        private PictureBox _playerShip;                                                                     //la picturebox du vaisseau
        private Timer _collisionTimer;
        private Score _scoreWindow;
        private List<Obstacle> _obstacles = new List<Obstacle>();
        private Timer _obstacleSpawnTimer;
        public Form1()
        {
            InitializeComponent();

            // Couleur de fond
            

            // Style de barre en haut
            this.FormBorderStyle = FormBorderStyle.None;

            // Setup PictureBox
            _playerShip = new PictureBox
            {
                Image = Image.FromFile("spaceship.png"), 
                SizeMode = PictureBoxSizeMode.AutoSize 
            };

            // Met la taille de la fenêtre égal à la taille de l'image
            this.Size = _playerShip.Image.Size;

            // Calcul pour mettre le vaisseau en bas au milieu
            _playerShip.Location = new Point((this.ClientSize.Width - _playerShip.Width) / 2, (this.ClientSize.Height - _playerShip.Height) / 2);
            this.Controls.Add(_playerShip); 

            // Calcul pour mettre la fenêtre en bas au milieu
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, Screen.PrimaryScreen.WorkingArea.Height - this.Height);

            this.MinimumSize = _playerShip.Size;                                                                //ligne pour que la taille de la fenetre soit aussi grand que l'image
            this.MaximumSize = _playerShip.Size;                                                                //ligne pour que la taille de la fenetre soit aussi grand que l'image


            int spacing = 100;                                                                                  //entier qui met l'espace entre les enemis

            //met des invaders
            for (int i = 0; i <= 10; i++) 
            {
                Invader invader = new Invader("enemy.png"); 
                invader.StartPosition = FormStartPosition.Manual; 
                invader.Location = new Point(i * (100 + spacing), 50); 
                invader.Show(); 
                _invaders.Add(invader); 
            }
            _scoreWindow = new Score();
            _scoreWindow.Show();


            StartTimers();
        }

        //bouger le vaisseau
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //aller a gauche
            if (keyData == Keys.A)
            {
                this.Left = Math.Max(0, this.Left - _moveSpeed);
                return true;
            }
            //aller a droite
            else if (keyData == Keys.D)
            {
                this.Left = Math.Min(Screen.PrimaryScreen.WorkingArea.Width - this.Width, this.Left + _moveSpeed);
                return true;
            }
            //tirer
            else if (keyData == Keys.Space)
            {
                ShootBullet();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }



        //bouger les enemis
        private void MoveInvaders(object sender, EventArgs e)
        {
            foreach (var invader in _invaders)
            {
                invader.Move();
            }
        }
        private void ShootBullet()
        {
            //calculations pour que la balle soit au dessus de vaisseau
            Point bulletPosition = new Point(this.Location.X + this.Width / 2 - 10, this.Location.Y - 20);

            //creation de balle
            Bullet bullet = new Bullet("bullet.png", bulletPosition); 
            bullet.Show(); 
            _bullets.Add(bullet);

            //pour rester dans la fenetre form1 et pouvoir bouger
            this.Focus();
        }

        private void CheckCollisions()
        {
            List<Bullet> bulletsToRemove = new List<Bullet>();                                                                  //liste qui contient la balles a enlever
            List<Invader> invadersToRemove = new List<Invader>();                                                               //liste qui contient la enemis a enlever

            foreach (Bullet bullet in _bullets)
            {
                foreach (Invader invader in _invaders)
                {
                    if (bullet.Bounds.IntersectsWith(invader.Bounds))
                    {
                        bulletsToRemove.Add(bullet);
                        invadersToRemove.Add(invader);

                        _scoreWindow.IncreaseScore();
                    }
                }
            }
            
            foreach (Bullet bullet in bulletsToRemove)
            {
                bullet.Close(); //ferme la fenetre
                _bullets.Remove(bullet); //enleve d'item de la liste principale
            }

            foreach (Invader invader in invadersToRemove)
            {
                invader.Close(); //ferme la fenetre
                _invaders.Remove(invader); //enleve d'item de la liste principale
            }
        }

        private void StartTimers()
        {
            //timer pour bouger
            _moveTimer = new Timer();
            _moveTimer.Interval = 100;
            _moveTimer.Tick += MoveInvaders;
            _moveTimer.Start();

            //timer pour les collisions
            _collisionTimer = new Timer();
            _collisionTimer.Interval = 1; 
            _collisionTimer.Tick += (sender, e) => CheckCollisions();
            _collisionTimer.Start();

            //timer pour spawn des obstacles
            _obstacleSpawnTimer = new Timer();
            _obstacleSpawnTimer.Interval = 7000;
            _obstacleSpawnTimer.Tick += SpawnObstacle;
            _obstacleSpawnTimer.Start();

        }


        private void SpawnObstacle(object sender, EventArgs e)
        {
            bool movingRight = new Random().NextDouble() < 0.5;                                 //genere une bool etre true et false pour savoir dans quel sens bouge l'object
            Obstacle obstacle = new Obstacle("bullet.png", movingRight);                        //cree un obstacle
            _obstacles.Add(obstacle);                                                           //ajoute a la liste

            this.Focus();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
