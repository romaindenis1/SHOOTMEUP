/******************************************************************************
** PROGRAMME  EvilBullet.cs                                                  **
**                                                                           **
** Lieu      : ETML - section informatique                                   **
** Auteur    : Romain Denis                                                  **
** Date      : 01.11.24                                                      **
**                                                                           **
** Modifications                                                             **
**   Auteur  :                                                               **
**   Version :                                                               **
**   Date    :                                                               **
**   Raisons :                                                               **
**                                                                           **
**                                                                           **
******************************************************************************/

/******************************************************************************
** DESCRIPTION                                                               **
** Voire en bas                                                              **     
**                                                                           **
**                                                                           **
******************************************************************************/

using System.Drawing;
using System.Windows.Forms;
using System;

public class EvilBullet : Form
{
    private PictureBox _bulletImage;                                    //image de la balle
    private Timer _moveTimer;                                           //timer pour bouger
    private int _bulletSpeed = 10;                                      //vitesse de la balle

    public EvilBullet(string imagePath, Point startPosition)
    {
        this.StartPosition = FormStartPosition.Manual;
        this.Location = startPosition;

        // picturebox
        _bulletImage = new PictureBox();
        _bulletImage.Image = Image.FromFile(imagePath);
        _bulletImage.SizeMode = PictureBoxSizeMode.AutoSize;

        // taille
        this.Size = _bulletImage.Size;
        this.Controls.Add(_bulletImage);

        // proprietes
        this.FormBorderStyle = FormBorderStyle.None;
        this.BackColor = Color.Black;
        this.ShowInTaskbar = false;

        this.MinimumSize = _bulletImage.Size;
        this.MaximumSize = _bulletImage.Size;

        // timer pour bouger
        _moveTimer = new Timer();
        _moveTimer.Interval = 100; 
        _moveTimer.Tick += MoveBullet; 
        _moveTimer.Start(); 
    }

    private void MoveBullet(object sender, EventArgs e)
    {
        // decend la balle
        this.Top += _bulletSpeed;

        // enleve balle si sort de l'ecran
        if (this.Top > Screen.PrimaryScreen.WorkingArea.Height)
        {
            _moveTimer.Stop(); 
            this.Close(); 
        }
    }

    private void InitializeComponent()
    {
            this.SuspendLayout();
            // 
            // EvilBullet
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "EvilBullet";
            this.Load += new System.EventHandler(this.EvilBullet_Load);
            this.ResumeLayout(false);

    }

    private void EvilBullet_Load(object sender, EventArgs e)
    {

    }
}
