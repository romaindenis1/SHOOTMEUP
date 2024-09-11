using System;
using System.Collections.Generic;  // Make sure this is included for List<T>
using System.Windows.Forms;
using System.Drawing;

namespace ShootEmUp
{
    public partial class Form1 : Form
    {
        private MoveHelper moveHelper;
        private List<Bullet> bullets;  

        public Form1()
        {
            InitializeComponent();
            
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;

            
            moveHelper = new MoveHelper(375);

            
            bullets = new List<Bullet>();
        }

        public void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int shipX = moveHelper.GetX();  

            const int y = 350;

            if (e.KeyCode == Keys.D)
            {
                shipX += 5;
            }

            if (e.KeyCode == Keys.A)
            {
                shipX -= 5;
            }

            
            moveHelper.SetX(shipX);

            
            pictureBox1.Location = new Point(shipX, y);

            
            if (e.KeyCode == Keys.Space)
            {
                
                Bullet newBullet = new Bullet(moveHelper, y);
                bullets.Add(newBullet);
            }

            
            foreach (var bullet in bullets)
            {
                bullet.Move();
            }
        }
    }
}
