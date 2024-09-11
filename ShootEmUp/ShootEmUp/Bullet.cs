using System;
using System.Drawing;

namespace ShootEmUp
{
    public class Bullet
    {
        private int x; 
        private int y; 

        
        public Bullet(MoveHelper moveHelper, int startY)
        {
           
            x = moveHelper.GetX();
            y = startY;
        }

        
        public void Move()
        {
            y -= 10; 
        }

        
        public Point GetPosition()
        {
            return new Point(x, y);
        }
    }
}