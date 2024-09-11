using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootEmUp
{
    public class MoveHelper
    {
        public int x;
        public MoveHelper(int initialX)
        {
            x = initialX;
        }

       
        public int GetX()
        {
            return x;
        }

        
        public void SetX(int newX)
        {
            x = newX;
        }
    }
}
