using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace LotOfWindowsSpaceInvader 
{
    public class Invader : Form
    {
        private PictureBox _invaderImage;                                                       //ou l'image de l'enemi est
        private int _moveSpeed = 25;                                                            //vitesse du invader
        private bool _movingRight = true;                                                       //bool qui permet de bouger les enemis dans le bon sens
       

        //consructeur du invader
        public Invader(string imagePath)
        {
            InitializeComponent();
            
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;

            
            _invaderImage = new PictureBox();
            _invaderImage.Image = Image.FromFile(imagePath);
            _invaderImage.SizeMode = PictureBoxSizeMode.AutoSize;
            this.Controls.Add(_invaderImage); 
            
            
            this.Size = _invaderImage.Image.Size;

            this.MinimumSize = _invaderImage.Size;                                              //ligne pour que la taille de la fenetre soit aussi grand que l'image
            this.MaximumSize = _invaderImage.Size;
            this._invaderImage.BackColor = System.Drawing.Color.Transparent;
        }

        //methode pour bouger le invader
        public void Move()
        {
            
            if (_movingRight)
            {
                this.Left += _moveSpeed;
                if (this.Right >= Screen.PrimaryScreen.WorkingArea.Width) 
                {
                    _movingRight = false; 
                    this.Top += 175; 
                }
            }
            else
            {
                this.Left -= _moveSpeed;
                if (this.Left <= 0) 
                {
                    _movingRight = true; 
                    this.Top += 175; 
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "Invader";
            this.Load += new System.EventHandler(this.Invader_Load);
            this.ResumeLayout(false);

        }

        private void Invader_Load(object sender, EventArgs e)
        {

        }
    }
}
