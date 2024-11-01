using System;
using System.Windows.Forms;
using System.Drawing;
using LotOfWindowsSpaceInvader;

public class MenuForm : Form
{
    private Label titleLabel;                                           //label pour le titre
    private Label playLabel;                                            //label pour le bouton Play
    private Label quitLabel;                                            //label pour le bouton Quit

    public MenuForm()
    {
        InitializeComponent(); //initialiser
    }

    /// <summary>
    /// Event de click sur le boutton play
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PlayLabel_Click(object sender, EventArgs e)
    {
        this.Hide(); // masquer le formulaire du menu
        LevelSelect levelSelect = new LevelSelect();
        levelSelect.Show();
    }
    /// <summary>
    /// Event de clicke sur le boutton quit
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void QuitLabel_Click(object sender, EventArgs e)
    {
        Application.Exit(); // fermer l'application
    }

    private void MenuForm_Load(object sender, EventArgs e)
    {

    }

    private void MenuForm_Load_1(object sender, EventArgs e)
    {

    }

    private void InitializeComponent()
    {
        this.SuspendLayout();


        // 
        // MenuForm
        // 
        this.ClientSize = new System.Drawing.Size(284, 261);
        this.Name = "MenuForm";
        this.Load += new System.EventHandler(this.MenuForm_Load_1);
        this.ResumeLayout(false);

        // Setup the titleLabel
        this.titleLabel = new Label();
        this.titleLabel.Text = "ShootEmUp";
        this.titleLabel.AutoSize = false;
        this.titleLabel.Size = new Size(300, 50);
        this.titleLabel.Location = new Point((this.ClientSize.Width - 275) / 2, 20);
        this.titleLabel.Font = new Font("Arial", 36, FontStyle.Bold);
        this.titleLabel.ForeColor = Color.White;
        this.titleLabel.BackColor = Color.Transparent;
        this.titleLabel.TextAlign = ContentAlignment.MiddleCenter;

        // Setup the playLabel
        this.playLabel = new Label();
        this.playLabel.Text = "Play";
        this.playLabel.Cursor = Cursors.Hand; 
        this.playLabel.AutoSize = false;
        this.playLabel.Size = new Size(125, 50);
        this.playLabel.Location = new Point((this.ClientSize.Width - 100) / 2, 100);
        this.playLabel.Font = new Font("Arial", 24, FontStyle.Bold);
        this.playLabel.ForeColor = Color.White;
        this.playLabel.BackColor = Color.Transparent;
        this.playLabel.BorderStyle = BorderStyle.FixedSingle;
        this.playLabel.TextAlign = ContentAlignment.MiddleCenter;
        this.playLabel.Click += new EventHandler(this.PlayLabel_Click);
        this.playLabel.MouseEnter += (s, e) => playLabel.ForeColor = Color.Yellow; 
        this.playLabel.MouseLeave += (s, e) => playLabel.ForeColor = Color.White; 

        // Setup the quitLabel
        this.quitLabel = new Label();
        this.quitLabel.Text = "Quit"; 
        this.quitLabel.Cursor = Cursors.Hand;
        this.quitLabel.AutoSize = false;
        this.quitLabel.Size = new Size(125, 50); 
        this.quitLabel.Location = new Point((this.ClientSize.Width - 100) / 2, 160); 
        this.quitLabel.Font = new Font("Arial", 24, FontStyle.Bold); 
        this.quitLabel.ForeColor = Color.White; 
        this.quitLabel.BackColor = Color.Transparent; 
        this.quitLabel.BorderStyle = BorderStyle.FixedSingle; 
        this.quitLabel.TextAlign = ContentAlignment.MiddleCenter; 
        this.quitLabel.Click += new EventHandler(this.QuitLabel_Click); 
        this.quitLabel.MouseEnter += (s, e) => quitLabel.ForeColor = Color.Yellow; 
        this.quitLabel.MouseLeave += (s, e) => quitLabel.ForeColor = Color.White; 

        // Setup the MenuForm
        this.ClientSize = new Size(300, 300); 
        this.Controls.Add(this.titleLabel); 
        this.Controls.Add(this.playLabel);
        this.Controls.Add(this.quitLabel); 
        this.Name = "MenuForm"; 
        this.Text = "ShootEmUp - Romain";
        this.StartPosition = FormStartPosition.CenterScreen; 
        this.BackColor = Color.Black; 
        this.ResumeLayout(false);

    }

    private void MenuForm_Load_3(object sender, EventArgs e)
    {

    }
}