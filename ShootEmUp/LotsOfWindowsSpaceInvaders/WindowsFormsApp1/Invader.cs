﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace LotOfWindowsSpaceInvader 
{
    public class Invader : Form
    {
        private PictureBox _invaderImage;                                                       //ou l'image de l'enemi est
        private int _moveSpeed = 1;                                                             //vitesse du invader
        private bool _movingRight = false;                                                       //bool qui permet de bouger les enemis dans le bon sens

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