/******************************************************************************
** PROGRAMME  Form1.cs                                                       **
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
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace LotOfWindowsSpaceInvader
{
    public partial class Form1 : Form
    {
        //toutes les listes
        private List<EvilBullet> _evilBullets = new List<EvilBullet>();                                     //liste de mauvaises balles
        private List<Invader> _invaders = new List<Invader>();                                              //liste de enemis
        private List<Bullet> _bullets = new List<Bullet>();                                                 //liste de balles
        private List<Obstacle> _obstacles = new List<Obstacle>();                                           //liste de obstacles
        private List<EvilBullet> evilBulletsToRemove = new List<EvilBullet>();                              //liste de mauvaise balles a enlever
        private List<Invader> invadersToRemove = new List<Invader>();                                       //liste de enemis balle a enlever
        private List<Bullet> bulletsToRemove = new List<Bullet>();                                          //liste de balles a enlever

        //tout les timers
        private Timer _evilBulletMoveTimer;                                                                 //timer pour bouger mauvaises balles
        private Timer _moveTimer;                                                                           //timer pour bouger
        private Timer _collisionTimer;                                                                      //timer qui regarde les collisions
        private Timer _obstacleSpawnTimer;                                                                  //timer qui spawn des obstacles

        //variables de vaisseau
        private PictureBox _playerShip;                                                                     //la picturebox du vaisseau
        private int _moveSpeed = 20;                                                                        //la vitesse du vaisseau
        
        private HUD _hudDisplay;                                                                            //la hud

        private double _invaderSpeedMultiplier = 1.0;                                                       //double pour ajouter de la vitesse a chaque wave

        private int _waveNumber = 0;                                                                        //Le nombre de wave que le joueur a tue

        //toutes les bool
        private bool _isGameOver = false;                                                                   //pour check si le jeux est fini
        private bool _isMenuOpen = false;                                                                   //bool bete pour que le menu est la que 1 fois
        private bool hasSpawnedNewWave = false;                                                             //check si on a deja spawn une nouvelle vague
        private bool _gameOverShown = false;                                                                //si on a deja montre le message de fin
        private bool _canShoot = true;                                                                      //pour voire si le joueur peut tirer
        public bool _hasShot = false;                                                                       //pour les test savoir si le ship a tire

        private const int _shootDelay = 500;                                                                //delay pour tirer

        //variables de levelselect.cs
        private LevelSettings _settings;                                                                    //instances des settings
        private string _enemySprite = "";                                                                   //sprite de lenemi
        private int _requiredScore = 0;                                                                     //score dont on a besion pour gagner
        
        //main
        public Form1(LevelSettings _settings)
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;

            // Setup PictureBox
            _playerShip = new PictureBox
            {
                Image = Image.FromFile("spaceship.png"),
                SizeMode = PictureBoxSizeMode.AutoSize
            };
            this.Controls.Add(_playerShip);
            // Style de barre en haut

            // Met la taille de la fenêtre égal à la taille de l'image
            this.Size = _playerShip.Image.Size;

            // Calcul pour mettre le vaisseau en bas au milieu
            _playerShip.Location = new Point((this.ClientSize.Width - _playerShip.Width) / 2, (this.ClientSize.Height - _playerShip.Height) / 2);
            
            // Calcul pour mettre la fenêtre en bas au milieu
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, Screen.PrimaryScreen.WorkingArea.Height - this.Height);

            this.MinimumSize = _playerShip.Size;                                                                //ligne pour que la taille de la fenetre soit aussi grand que l'image
            this.MaximumSize = _playerShip.Size;                                                                //ligne pour que la taille de la fenetre soit aussi grand que l'image
           
            GetLevelStats(_settings);
            SpawnNewWave();

            _hudDisplay = new HUD();                                                                            //cree la hud
            _hudDisplay.Show();                                                                                 //cree la hud

            StartTimers();                                                                                      //lance tout les timers
            SpawnObstacle(null, null);                                                                          //spawn les obstacles
        }

        /// <summary>
        /// Un constructeur de settings de niveau importe
        /// </summary>
        /// <param name="_settings"></param>
        public void GetLevelStats(LevelSettings _settings)
        {
            _invaderSpeedMultiplier = _settings.InvaderSpeedMultiplier;
            _enemySprite = _settings.EnemySprite;
            _requiredScore = _settings.RequiredScore;
        }

        /// <summary>
        /// bouger le vaisseau
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData">La touche clicke</param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //aller a gauche
            if (keyData == Keys.A || keyData == Keys.Left)
            {
                this.Left = Math.Max(0, this.Left - _moveSpeed);
                return true;
            }

            //aller a droite
            else if (keyData == Keys.D || keyData == Keys.Right)
            {
                this.Left = Math.Min(Screen.PrimaryScreen.WorkingArea.Width - this.Width, this.Left + _moveSpeed);
                return true;
            }

            //tirer
            else if (keyData == Keys.Space)
            {
                ShootBullet();
                _hasShot = true;
            }
			
			//reset le jeu 
			if (keyData == Keys.Escape)
            {
                ReturnToMainMenu();
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
                invader.Move(_invaderSpeedMultiplier); //ajoute le multiplier
            }
        }
        /// <summary>
        /// Tire une balle
        /// </summary>
        private void ShootBullet()
        {
            if (_canShoot)                                  //si on peut tirer
            {
                //calculation de la position de la balle
                Point bulletPosition = new Point(this.Location.X + this.Width / 2 - 10, this.Location.Y - 20);

                Bullet bullet = new Bullet("bullet.png", bulletPosition);
                bullet.Show();                                              //fait une balle
                _bullets.Add(bullet);

                
                this.Focus(); //alt tab sur le vaisseau

                
                _canShoot = false; //le joueur ne peut pas tirer
                StartShootDelayTimer(); //commence le delai
            }
        }

        //timer delay entre tir
        private void StartShootDelayTimer()
        {
            Timer shootDelayTimer = new Timer();
            shootDelayTimer.Interval = _shootDelay;
            shootDelayTimer.Tick += (s, e) =>
            {
                _canShoot = true; 
                shootDelayTimer.Stop(); 
                shootDelayTimer.Dispose(); 
            };
            shootDelayTimer.Start(); 
        }

        /// <summary>
        /// Regarde si 2 choses se sont touche
        /// </summary>
        private void CheckCollisions()
        {
            //check si il faut spawn une nouvelle vague je sais cest probablement pas cense etre la
            if (((int)_hudDisplay._scoreValue % 10) == 0 && _hudDisplay._scoreValue != 0 && !hasSpawnedNewWave)
            {
                hasSpawnedNewWave = true; 
                _waveNumber++;
                _invaderSpeedMultiplier += 0.1;
                SpawnNewWave();
            }
            else if (((int)_hudDisplay._scoreValue % 10) != 0)
            {
                hasSpawnedNewWave = false; 
            }


            //enkeve balle en jeu
            foreach (Bullet bullet in bulletsToRemove)
            {
                bullet.Close();
                _bullets.Remove(bullet);
            }

            //enleve invader en jeu
            foreach (Invader invader in invadersToRemove)
            {
                invader.Close();
                _invaders.Remove(invader);
            }

            

            //check collision balles enemis
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

            //check collisions balles enemis vaisseau
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
                        foreach (EvilBullet evilBullet in evilBulletsToRemove)
                        {
                            evilBullet.Close();
                            _evilBullets.Remove(evilBullet);
                        }
                        _hudDisplay.DecreaseLives();
                    }
                
            }

            //check collision balles obstacles
            foreach (Obstacle obstacle in _obstacles.ToList())
            {
                foreach (Bullet bullet in _bullets)
                {
                    if (obstacle.BlocksBullet(bullet))
                    {
                        bulletsToRemove.Add(bullet);
                        obstacle.Close();
                        _obstacles.Remove(obstacle); //enleve obstacle
                    }
                }

                
            }

            //check collision balles enemis obstacles
            foreach (Obstacle obstacle in _obstacles.ToList())
            {
                foreach (EvilBullet evilBullet in _evilBullets)
                {
                    if (obstacle.BlocksEvilBullet(evilBullet))
                    {
                        evilBulletsToRemove.Add(evilBullet);
                        evilBullet.Close(); 
                        obstacle.Close();
                        _obstacles.Remove(obstacle); //enleve obstacle
                    }
                }
            }

            if (_invaders.Count == 0 || _hudDisplay._scoreValue >= _requiredScore && !_gameOverShown)
            {
                _gameOverShown = true; //pour pas ouvrir plusieurs fenetres
                ShowGameOverText("You Win!");
                ReturnToMainMenu();
            }

            // Your existing game over checks...
            // For example, if the player loses
            if (_hudDisplay._livesValue <= 0 && !_gameOverShown)
            {
                _gameOverShown = true; //pour pas ouvrir plusieurs fenetres
                ShowGameOverText("Game Over!");
                ReturnToMainMenu();
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
            int randomIndex = random.Next(_invaders.Count);                                 //prend un enemi random
            Invader shootingEnemy = _invaders[randomIndex];


            EvilBullet evilBullet = new EvilBullet("evilbullet.png", new Point(shootingEnemy.Left + (shootingEnemy.Width / 2) - 5, shootingEnemy.Bottom));      //creer une balle enemi
            this.Controls.Add(evilBullet);                                                                                                                      //ajoute a la liste
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
                if (evilBullet.Top > this.ClientSize.Height)                                        //enleve balle si elle sort de ecran
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
            for (int i = 0; i < 3; i++)                                                         // Boucle pour créer trois obstacles
            {
                Obstacle obstacle = new Obstacle("obstacle.png", i);                            // Crée un obstacle à la position donnée par l'index
                _obstacles.Add(obstacle);                                                       // Ajoute l'obstacle à la liste
            }

            this.Focus();                                                                       // Remet le focus sur la fenêtre principale
        }


        /// <summary>
        /// Reset le jeu pour pouvoir relancer
        /// </summary>
        private void ReturnToMainMenu()
        {

            MenuForm MenuForm = new MenuForm();
            MenuForm.Show();
            
            //enleve toutes les balles
            foreach (var bullet in _bullets.ToList())
            {
                bullet.Close();
            }
            _bullets.Clear();

            //enleve toutes les balles enemis
            foreach (var evilBullet in _evilBullets.ToList())
            {
                evilBullet.Close();
            }
            _evilBullets.Clear();

            //enleve tout les enemis
            foreach (var invader in _invaders.ToList())
            {
                invader.Close();
            }
            _invaders.Clear();

            //enleve tout les obstacles
            foreach (var obstacle in _obstacles.ToList())
            {
                obstacle.Close();
            }
            _obstacles.Clear();
            
            //ferme la hud
            _hudDisplay.Close();

            //enleve le vaisseau
            this.Close();

            //reset les timers
            ResetTimers();
        }



        /// <summary>
        /// reset tout les timers
        /// </summary>
        private void ResetTimers()
        {
            //pour que le menu ouvre que 1 fois  
            if (_isMenuOpen)                        
            {                                       
                return;                             
            }                                       
            _isMenuOpen = true;                     

            //stop timer pour bouger
            if (_moveTimer != null) 
            {
                _moveTimer.Stop();
                _moveTimer.Dispose();
                _moveTimer = null;
            }
            
            //stop timer de collision
            if (_collisionTimer != null)
            {
                _collisionTimer.Stop();
                _collisionTimer.Dispose();
                _collisionTimer = null;
            }

            //stop timer balles enemis qui bougent
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
            _isGameOver = false;                                                   //faux par ce que le jeu est pas fini   
            _isMenuOpen = false;                                                   //faux par ce que le menu est ferme
        }

        /// <summary>
        /// Fonction qui ajoute une nouvelle vague de enemis
        /// </summary>
        public void SpawnNewWave()
        {
            int spacing = 100;                                                      //lespacement entre chaque enemis en px
            for (int i = 0; i <= 10 + _waveNumber; i++)
            {
                Invader invader = new Invader(_enemySprite);                        //
                invader.StartPosition = FormStartPosition.Manual;                   //fait les enemis
                invader.Location = new Point(i * (100 + spacing), 50);              //
                invader.Show();
                _invaders.Add(invader);                                             //ajoute a la liste
            }
            this.Focus();                                                           //alt tab sur le vaisseau
        }

        /// <summary>
        /// Fonction qui montre le message de fin
        /// </summary>
        /// <param name="message">Le message de fin, dependant sur le resultat de la partie</param>
        private void ShowGameOverText(string message)
        {
            // Create an instance of GameOverWindow with the message
            GameOverWindow gameOverWindow = new GameOverWindow(message);
            gameOverWindow.ShowDialog(); // Show the window as a modal dialog
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
