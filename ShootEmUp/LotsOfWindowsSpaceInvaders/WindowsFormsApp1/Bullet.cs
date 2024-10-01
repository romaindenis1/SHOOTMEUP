using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class Bullet : Form
{
    private PictureBox _bulletImage;                                                        //ou l'image de la balle est
    private int _bulletSpeed = 5;                                                           //vitesse de la balle
    private int _bulletY = 5;                                                               //la position y de le balle

    //consructeur du invader
    public Bullet(string imagePath, int _bulletY)
    {
        InitializeComponent();
        this.FormBorderStyle = FormBorderStyle.None;
        this.StartPosition = FormStartPosition.Manual;


        _bulletImage = new PictureBox();

        this._bulletY = _bulletY;

        _bulletImage.SizeMode = PictureBoxSizeMode.AutoSize;
        this.Controls.Add(_bulletImage);


        this.Size = _bulletImage.Image.Size;
    }

    //methode pour bouger le invader
    public void Shoot()
    {
        
        //jsp fait du code ici a un moment futur
    }

    private void InitializeComponent()
    {
        this.SuspendLayout();

        this.ClientSize = new System.Drawing.Size(284, 261);
        this.Name = "Bullet";
        this.Load += new System.EventHandler(this.Bullet_Load);
        this.ResumeLayout(false);
    }

    private void Bullet_Load(object sender, EventArgs e)
    {

    }
}


