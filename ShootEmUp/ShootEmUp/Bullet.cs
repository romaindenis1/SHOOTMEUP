using ShootEmUp;
using System;
using System.Windows.Forms;

public class Bullet
{
    private PictureBox pictureBox2;
    public int speed = 5;
	private int damage = 1;
    private int bulletY = 0;
	public Bullet(int speed, int damage, int bulletY, int bulletX)
	{
		this.speed = speed;
		this.damage = damage;
		this.bulletY = bulletY;
	}
	public void Shoot(int speed, int damage, int bulletX, int bulletY)
	{ 
		Bullet Bullet1 = new Bullet(speed, damage, bulletY, bulletX);
	}
}
