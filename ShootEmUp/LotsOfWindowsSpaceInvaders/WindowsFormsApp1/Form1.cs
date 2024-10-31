using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1;
using LotOfWindowsSpaceInvader;
using System.Linq;

namespace LotOfWindowsSpaceInvader
{
    public partial class Form1 : Form
    {

        private List<EvilBullet> _evilBullets = new List<EvilBullet>();                                     //liste de mauvaises balles
        private List<Invader> _invaders = new List<Invader>();                                              //liste de enemis
        private List<Bullet> _bullets = new List<Bullet>();                                                 //liste de balles
        private List<Obstacle> _obstacles = new List<Obstacle>();                                           //liste de obstacles
        List<Bullet> bulletsToRemove = new List<Bullet>();                                                  //liste de balles a enlever
        List<Invader> invadersToRemove = new List<Invader>();                                               //liste de enemis balle a enlever
        List<EvilBullet> evilBulletsToRemove = new List<EvilBullet>();                                      //liste de mauvaise balles a enlever

        private Timer _evilBulletMoveTimer;                                                                 //timer pour bouger mauvaises balles
        private Timer _moveTimer;                                                                           //timer pour bouger
        private Timer _collisionTimer;                                                                      //timer qui regarde les collisions
        private Timer _obstacleSpawnTimer;                                                                  //timer qui spawn des obstacles

        private int _moveSpeed = 20;                                                                        //la vitesse du vaisseau
        private PictureBox _playerShip;                                                                     //la picturebox du vaisseau
        private HUD _hudDisplay;

        private double _invaderSpeedMultiplier = 1.0;                                                       //Double pour ajouter de la vitesse a chaque wave
        private int _waveNumber = 0;                                                                        //Le nombre de wave que le joueur a tue
        private bool _isGameOver = false;                                                                   //pour check si le jeux est fini
        private bool _isMenuOpen = false;                                                                   //bool bete pour que le menu est la que 1 fois
        public Form1()
        {
            InitializeComponent();

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

            SpawnNewWave();

            _hudDisplay = new HUD();
            _hudDisplay.Show();


            StartTimers();
        }

        /// <summary>
        /// bouger le vaisseau
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
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

        /// <summary>
        /// bouger les enemis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveInvaders(object sender, EventArgs e)
        {
            foreach (var invader in _invaders)
            {
                invader.Move(_invaderSpeedMultiplier); // Pass the speed multiplier to each invader's move method
            }
        }
        /// <summary>
        /// Tire une balle
        /// </summary>
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
        /// <summary>
        /// Regarde si 2 choses se sont touche
        /// </summary>
        private void CheckCollisions()
        {

            if (((int)_hudDisplay._scoreValue % 10) == 0 && _hudDisplay._scoreValue != 0)
            {
                for (int i = 0; i < 1; i++)
                {
                    _waveNumber = 0;
                    _invaderSpeedMultiplier += 0.1;
                    SpawnNewWave();
                }
            }
            foreach (Bullet bullet in _bullets)
            {
                foreach (Invader invader in _invaders)
                {
                    if (bullet.Bounds.IntersectsWith(invader.Bounds))
                    {
                        bulletsToRemove.Add(bullet);
                        invadersToRemove.Add(invader);
                        _hudDisplay.IncreaseScore();
                    }
                }
            }

            foreach (EvilBullet evilbullet in _evilBullets.ToList())
            {
                if (evilbullet.IsDisposed)
                {
                    _evilBullets.Remove(evilbullet);
                    continue;
                }
                //Beaucoup de maths qui ma pri 3 jours pour detecter les collisions que j'aime pas du tout
                Point evilbulletPoint = evilbullet.PointToScreen(new Point(0, 0));
                Rectangle evilbulletRect = new Rectangle(evilbulletPoint.X, evilbulletPoint.Y, evilbullet.Width, evilbullet.Height);

                Point playerShipPoint = _playerShip.PointToScreen(new Point(0, 0));
                Rectangle playerShipRect = new Rectangle(playerShipPoint.X, playerShipPoint.Y, _playerShip.Width, _playerShip.Height);

                Rectangle intersection = Rectangle.Intersect(evilbulletRect, playerShipRect);
                if (intersection.Width > 0 && intersection.Height > 0)
                {
                    evilBulletsToRemove.Add(evilbullet);
                    _hudDisplay.DecreaseLives();
                    CheckIfDead();
                }
            }

            //balles
            foreach (Bullet bullet in bulletsToRemove)
            {
                bullet.Close(); //ferme la fenetre
                _bullets.Remove(bullet); //eneleve de la liste
            }

            foreach (Obstacle obstacle in _obstacles)
            {
                foreach (Bullet bullet in _bullets)
                {
                    if (obstacle.BlocksBullet(bullet))
                    {
                        // enleve balle et obstacle
                        bulletsToRemove.Add(bullet);
                        obstacle.Close();
                    }
                }

                foreach (EvilBullet evilBullet in _evilBullets)
                {
                    if (obstacle.BlocksEvilBullet(evilBullet))
                    {
                        // enleve balle et obstacle
                        evilBulletsToRemove.Add(evilBullet);
                        obstacle.Close();
                    }
                }
            }

            //enemis
            foreach (Invader invader in invadersToRemove)
            {
                invader.Close(); //ferme la fenetre
                _invaders.Remove(invader); //eneleve de la liste
            }

            //balle mechant
            foreach (EvilBullet evilBullet in evilBulletsToRemove)
            {
                evilBullet.Dispose(); //ferme la fenetre
                _evilBullets.Remove(evilBullet); //eneleve de la liste
            }

        }

        /// <summary>
        /// Commence tout les timers
        /// </summary>
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

            //timer pour tirer mechantes balles
            Timer evilBulletShootTimer = new Timer();
            evilBulletShootTimer.Interval = 2000; //tire tout les 2 secondes
            evilBulletShootTimer.Tick += (sender, e) => ShootEvilBullet();
            evilBulletShootTimer.Start();

            _evilBulletMoveTimer = new Timer();
            _evilBulletMoveTimer.Interval = 2000;
            _evilBulletMoveTimer.Tick += (sender, e) => ShootEvilBullet(); // Apelle methode ShootEvilBullet 
            _evilBulletMoveTimer.Start();

        }
        /// <summary>
        /// Fait tirer enemi
        /// </summary>
        private void ShootEvilBullet()
        {
            // prend un random enemi
            if (_invaders.Count > 0)
            {
                Random random = new Random();
                int randomIndex = random.Next(_invaders.Count);
                Invader invader = _invaders[randomIndex];

                // cree une balle
                Point bulletPosition = new Point(invader.Location.X + invader.Width / 2, invader.Location.Y + invader.Height);
                EvilBullet evilBullet = new EvilBullet("evilbullet.png", bulletPosition);
                evilBullet.Show();

                // ajoute a la liste
                _evilBullets.Add(evilBullet);

                //alt tab
                this.Focus();
            }
        }
        /// <summary>
        /// Fait tirer les enemis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnemyShootTimer_Tick(object sender, EventArgs e)
        {
            if (_invaders.Count == 0) return;

            Random random = new Random();
            int randomIndex = random.Next(_invaders.Count);
            Invader shootingEnemy = _invaders[randomIndex];


            EvilBullet evilBullet = new EvilBullet("evilbullet.png", new Point(shootingEnemy.Left + (shootingEnemy.Width / 2) - 5, shootingEnemy.Bottom));
            this.Controls.Add(evilBullet);
        }

        /// <summary>
        /// faire bouger les balles enemis
        /// </summary>
        private void MoveEvilBullets()
        {
            List<EvilBullet> bulletsToRemove = new List<EvilBullet>();

            foreach (var evilBullet in _evilBullets)
            {
                evilBullet.Top += 10;                                                               //descend balle

                //check si balle sort de l'ecran
                if (evilBullet.Top > this.ClientSize.Height)
                {
                    bulletsToRemove.Add(evilBullet);
                }
            }

            //enleve balle si elle sort de l'ecran
            foreach (var bullet in bulletsToRemove)
            {
                bullet.Dispose();
                _evilBullets.Remove(bullet);
            }
        }
        /// <summary>
        /// fait des obstacles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpawnObstacle(object sender, EventArgs e)
        {
            bool movingRight = new Random().NextDouble() < 0.5;                                 //genere une bool etre true et false pour savoir dans quel sens bouge l'object
            Obstacle obstacle = new Obstacle("obstacle.png", movingRight);                      //cree un obstacle
            _obstacles.Add(obstacle);                                                           //ajoute a la liste

            this.Focus();                                                                       //remet sur la bonne fenetre
        }
        /// <summary>
        /// regarde si joueur est mort
        /// </summary>
        private void CheckIfDead()
        {
            if (_hudDisplay != null && _hudDisplay.GetLivesValue() == 0 && !_isGameOver)        //si le jeu est fini ou il y a plus de vies
            {
                _isGameOver = true;
                using (GameOverWindow gameOverWindow = new GameOverWindow("Game Over!"))        //dit gameover
                {
                    gameOverWindow.ShowDialog();
                }

                ReturnToMainMenu();                                                             //return au menu
            }
        }

        /// <summary>
        /// methode pour enlever tout
        /// </summary>
        private void ReturnToMainMenu()
        {
            //enleve tout
            foreach (var bullet in _bullets.ToList())
            {
                bullet.Close();
            }
            _bullets.Clear();

            foreach (var evilBullet in _evilBullets.ToList())
            {
                evilBullet.Close();
            }
            _evilBullets.Clear();

            foreach (var invader in _invaders.ToList())
            {
                invader.Close();
            }
            _invaders.Clear();

            foreach (var obstacle in _obstacles.ToList())
            {
                obstacle.Close();
            }
            _obstacles.Clear();

            _hudDisplay.Close();

            this.Focus();

            //reset hud
            _hudDisplay.ResetHUD();

            //reset logique
            _isGameOver = false; 

            //ouvre le menu 
            MenuForm MenuForm = new MenuForm();
            MenuForm.Show(); 
            this.Hide(); 
        }

        /// <summary>
        /// reset tout les timers
        /// </summary>
        private void ResetTimers()
        {

            if (_isMenuOpen)
            {
                return;
            }
            _isMenuOpen = true;

            if (_moveTimer != null)
            {
                _moveTimer.Stop();
                _moveTimer.Dispose();
                _moveTimer = null;
            }

            if (_collisionTimer != null)
            {
                _collisionTimer.Stop();
                _collisionTimer.Dispose();
                _collisionTimer = null;
            }

            if (_obstacleSpawnTimer != null)
            {
                _obstacleSpawnTimer.Stop();
                _obstacleSpawnTimer.Dispose();
                _obstacleSpawnTimer = null;
            }

            if (_evilBulletMoveTimer != null)
            {
                _evilBulletMoveTimer.Stop();
                _evilBulletMoveTimer.Dispose();
                _evilBulletMoveTimer = null;
            }
        }
        /// <summary>
        /// Reset le jeu
        /// </summary>
        private void ResetGame()
        {
            _isGameOver = false;
            _isMenuOpen = false;
        }
        public void SpawnNewWave()
        {
            int spacing = 100;
            for (int i = 0; i <= 10 + _waveNumber; i++)
            {
                Invader invader = new Invader("enemy.png");
                invader.StartPosition = FormStartPosition.Manual;
                invader.Location = new Point(i * (100 + spacing), 50);
                invader.Show();
                _invaders.Add(invader);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
