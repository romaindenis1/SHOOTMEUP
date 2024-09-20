
namespace ShootEmUp
{
    public class Invader
{
    private PictureBox pictureBox;
    private int x;
    private int y;
    private int speed;

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

    public PictureBox GetPictureBox()
    {
        return pictureBox;
    }

    public void Move()
    {
        x += speed;
        pictureBox.Location = new Point(x, y);

        if (x > 750 || x < 0)
        {
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
