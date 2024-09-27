using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LotOfWindowsSpaceInvader 
{
    public partial class Form1 : Form
    {
        private List<Invader> _invaders = new List<Invader>();
        private Timer _moveTimer;
        private int _moveSpeed = 10; 
        private PictureBox _playerShip; 

        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.Black; 
            this.FormBorderStyle = FormBorderStyle.None; 

            
            _playerShip = new PictureBox();
            _playerShip.Image = Image.FromFile("spaceship.png"); 
            _playerShip.SizeMode = PictureBoxSizeMode.AutoSize; 

            
            this.Size = _playerShip.Image.Size; 

            
            _playerShip.Location = new Point((this.ClientSize.Width - _playerShip.Width) / 2,
                                              this.ClientSize.Height - _playerShip.Height);
            this.Controls.Add(_playerShip); 

            
            this.StartPosition = FormStartPosition.Manual; 
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                                      Screen.PrimaryScreen.WorkingArea.Height - this.Height); 

            
            int spacing = 175; 
            for (int i = 0; i < 5; i++) 
            {
                Invader invader = new Invader("enemy.png"); 
                invader.StartPosition = FormStartPosition.Manual; 
                invader.Location = new Point(i * (100 + spacing), 50); 
                invader.Show(); 
                _invaders.Add(invader); 
            }


            
            _moveTimer = new Timer();
            _moveTimer.Interval = 100; 
            _moveTimer.Tick += MoveInvaders;
            _moveTimer.Start();
        }

        
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
            return base.ProcessCmdKey(ref msg, keyData);
        }

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
