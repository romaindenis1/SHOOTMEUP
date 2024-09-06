namespace ShootEmUp
{
    public partial class Ship : Form
    {
        public Ship()
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
        }

        public void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            int x = pictureBox1.Location.X;
            const int y = 350;

            if (e.KeyCode == Keys.D)
            { 
                x += 3; 
            }

            if (e.KeyCode == Keys.A) 
            {
                x -= 3;
            }
            pictureBox1.Location = new Point(x, y);

            if (e.KeyCode == Keys.Space)
            {
                FireBullet(x);
            }
        }

        // Method to fire a bullet
        private void FireBullet(int x)
        {
            int bulletSpeed = 10;
            int bulletDamage = 1;
            int bulletY = pictureBox1.Location.Y; // Start the bullet from the ship's Y position

            Bullet bullet = new Bullet(bulletSpeed, bulletDamage, bulletY, x);

        }
    }
    public class Bullet
    {
        private int speed = 5;
        private int damage = 1;
        private int bulletY = 0;
        public int bulletX = 0;
        public Bullet(int speed, int damage, int bulletY, int bulletX)
        {
            this.speed = speed;
            this.damage = damage;
            this.bulletY = bulletY;
        }
        public void Shoot(int speed, int damage)
        {
            Bullet Bullet1 = new Bullet(speed, damage, bulletY, bulletX);

        }

    }
}