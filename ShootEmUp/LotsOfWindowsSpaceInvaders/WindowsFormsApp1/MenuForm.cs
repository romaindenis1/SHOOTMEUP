using System;
using System.Windows.Forms;
using System.Drawing;
using LotOfWindowsSpaceInvader; 

public class MenuForm : Form
{
    private PictureBox playPictureBox;
    private PictureBox quitPictureBox;

    public MenuForm()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.playPictureBox = new PictureBox();
        this.quitPictureBox = new PictureBox();

        // 
        // playPictureBox
        // 
        this.playPictureBox.Image = Image.FromFile("spaceship.png");
        this.playPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        this.playPictureBox.Location = new System.Drawing.Point(100, 100);
        this.playPictureBox.Size = new System.Drawing.Size(100, 50);
        this.playPictureBox.Click += new EventHandler(this.PlayPictureBox_Click);
        this.playPictureBox.Cursor = Cursors.Hand; 

        // 
        // quitPictureBox
        // 
        this.quitPictureBox.Image = Image.FromFile("enemy.png"); 
        this.quitPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        this.quitPictureBox.Location = new System.Drawing.Point(100, 160);
        this.quitPictureBox.Size = new System.Drawing.Size(100, 50);
        this.quitPictureBox.Click += new EventHandler(this.QuitPictureBox_Click);

        // 
        // MenuForm
        // 
        this.ClientSize = new System.Drawing.Size(300, 300);
        this.Controls.Add(this.playPictureBox);
        this.Controls.Add(this.quitPictureBox);
        this.Text = "Game Menu";
    }

    private void PlayPictureBox_Click(object sender, EventArgs e)
    {
        this.Hide(); 
        Form1 form1 = new Form1();
        form1.Show();
    }

    private void QuitPictureBox_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }
}
