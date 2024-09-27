
namespace ShootEmUp
{
    public class Invader
{

    private PictureBox pictureBox;                                              //le sprite de l'ennemi
    private int x;                                                              //le coordone x de l'ennemi
    private int y;                                                              //le coordone < de l'ennemi
    private int speed;                                                          //la vitesse de l'ennemi

    //constructeur
    public Invader(int x, int y, int speed)
    {
        this.x = x;
        this.y = y;
        this.speed = speed;

        pictureBox = new PictureBox();
        pictureBox.Location = new Point(x, y);
        pictureBox.Size = new Size(20, 20);
        pictureBox.BackColor = Color.Green;
    }

    //fait un picturebox
    public PictureBox GetPictureBox()
    {
        return pictureBox;
    }

    //bouge l'ennemi
    public void Move()
    {
        x += speed;
        pictureBox.Location = new Point(x, y);

        if (x > 750 || x < 0)
        {
                for (int i = 0; i < 25; i++) 
                {
                    y -= -1;
                    
                }    
            speed = -speed;
        }
    }

    public bool IsOutOfBounds(int height)
    {
        return y > height;
    }

    public void SetY(int y)
    {
        this.y = y;
        pictureBox.Location = new Point(x, y);
    }
}
}
