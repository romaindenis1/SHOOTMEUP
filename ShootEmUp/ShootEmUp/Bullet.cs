using System.Drawing;
using System.Windows.Forms;

namespace ShootEmUp
{
    public class Bullet
    {
        private PictureBox bulletPictureBox;
        private int y; 

        public Bullet(MoveHelper moveHelper, int startY)
        {
            
            bulletPictureBox = new PictureBox
            {
                Size = new Size(10, 10),
                BackColor = Color.Red, 
                Location = new Point(moveHelper.GetX() + 21 , startY ), 
            };

            y = startY;
        }

        
        public void Move()
        {
            y -= 5; 
            bulletPictureBox.Top = y; 
        }

        
        public PictureBox GetPictureBox()
        {
            return bulletPictureBox;
        }

       
        public bool IsOutOfBounds(int formHeight)
        {
            return bulletPictureBox.Top < 0;
        }
    }
}
