using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LotOfWindowsSpaceInvader 
{
    public partial class Form1 : Form
    {
        private List<Invader> _invaders = new List<Invader>();                                              //liste de enemis
        private List<Bullet> _bullets = new List<Bullet>();                                                 //liste de balles
        private Timer _moveTimer;                                                                           //le timer pour bouger
        private int _moveSpeed = 10;                                                                        //la vitesse du vaisseau
        private PictureBox _playerShip;                                                                     //la picturebox du vaisseau

        public Form1()
        {
            InitializeComponent();

            //couleur de fond
            //this.BackColor = Color.Transparent;
            
            //style de barre en haut
            this.FormBorderStyle = FormBorderStyle.None; 

            //setup picturebox
            _playerShip = new PictureBox();
            _playerShip.Image = Image.FromFile("spaceship.png"); 
            _playerShip.SizeMode = PictureBoxSizeMode.AutoSize; 

            //met la taille de la fenetre equal a la taille de l'image
            this.Size = _playerShip.Image.Size; 

            //calcul pour metre le vaisseau en bas au millieu
            _playerShip.Location = new Point((this.ClientSize.Width - _playerShip.Width) / 2,
                                              this.ClientSize.Height - _playerShip.Height);
            this.Controls.Add(_playerShip);

            //calcul pour metre la fenetre en bas au millieu
            this.StartPosition = FormStartPosition.Manual; 
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                                      Screen.PrimaryScreen.WorkingArea.Height - this.Height); 

            
            int spacing = 200;                                                                                  //entier qui met l'espace entre les enemis

            //met des invaders
            for (int i = 0; i < 100; i++) 
            {
                Invader invader = new Invader("enemy.png"); 
                invader.StartPosition = FormStartPosition.Manual; 
                invader.Location = new Point(i * (100 + spacing), 50); 
                invader.Show(); 
                _invaders.Add(invader); 
            }


            //timer pour bouger
            _moveTimer = new Timer();
            _moveTimer.Interval = 100; 
            _moveTimer.Tick += MoveInvaders;
            _moveTimer.Start();
        }

        //bouger le vaisseau
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.A)
            {
                this.Left -= _moveSpeed; 
                return true; 
            }
            else if (keyData == Keys.D)
            {
                this.Left += _moveSpeed; 
                return true; 
            }
            else if (keyData == Keys.Space)
            {
                /*
                Bullet newBullet = new Bullet(, );
                bullets.Add(newBullet);
                this.Controls.Add(newBullet.GetPictureBox());
                */
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
