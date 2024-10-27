using System;
using System.Windows.Forms;
using System.Drawing;
using LotOfWindowsSpaceInvader;

public class MenuForm : Form
{
    private Label titleLabel;
    private Label playLabel; // label pour le bouton "Play"
    private Label quitLabel; // label pour le bouton "Quit"

    public MenuForm()
    {
        InitializeComponent(); // initialiser les composants du formulaire
    }

    private void InitializeComponent()
    {
        this.titleLabel = new Label();
        this.playLabel = new Label(); // créer un nouveau label pour Play
        this.quitLabel = new Label(); // créer un nouveau label pour Quit
        this.SuspendLayout(); // suspendre la mise en page pour améliorer les performances pendant l'initialisation

        this.titleLabel.Text = "ShootEmUp";
        this.titleLabel.AutoSize = false;
        this.titleLabel.Size = new Size(300, 50);
        this.titleLabel.Location = new Point((this.ClientSize.Width - 275 ) / 2, 20);
        this.titleLabel.Font = new Font("Arial", 36, FontStyle.Bold);
        this.titleLabel.ForeColor = Color.White;
        this.titleLabel.BackColor = Color.Transparent;
        this.titleLabel.TextAlign = ContentAlignment.MiddleCenter;

        // 
        // playLabel
        // 
        this.playLabel.Text = "Play"; // définir le texte pour le label Play
        this.playLabel.Cursor = Cursors.Hand; // changer le curseur en main au survol
        this.playLabel.AutoSize = false; // autoriser le dimensionnement manuel du label
        this.playLabel.Size = new Size(125, 50); // définir la taille fixe pour le label
        this.playLabel.Location = new Point((this.ClientSize.Width - 100) / 2, 100); // centrer horizontalement dans le formulaire
        this.playLabel.Font = new Font("Arial", 24, FontStyle.Bold); // définir le style et la taille de la police
        this.playLabel.ForeColor = Color.White; // définir la couleur du texte
        this.playLabel.BackColor = Color.Transparent; // rendre l'arrière-plan transparent
        this.playLabel.BorderStyle = BorderStyle.FixedSingle; // ajouter une bordure autour du label
        this.playLabel.TextAlign = ContentAlignment.MiddleCenter; // centrer le texte dans le label
        this.playLabel.Click += new EventHandler(this.PlayLabel_Click); // gestionnaire d'événements pour l'événement de clic
        this.playLabel.MouseEnter += (s, e) => playLabel.ForeColor = Color.Yellow; // changer la couleur au survol
        this.playLabel.MouseLeave += (s, e) => playLabel.ForeColor = Color.White; // réinitialiser la couleur lorsqu'il n'est pas survolé

        // 
        // quitLabel
        // 
        this.quitLabel.Text = "Quit"; // définir le texte pour le label Quit
        this.quitLabel.Cursor = Cursors.Hand; // changer le curseur en main au survol
        this.quitLabel.AutoSize = false; // autoriser le dimensionnement manuel du label
        this.quitLabel.Size = new Size(125, 50); // définir la taille fixe pour le label
        this.quitLabel.Location = new Point((this.ClientSize.Width - 100) / 2, 160); // centrer horizontalement dans le formulaire
        this.quitLabel.Font = new Font("Arial", 24, FontStyle.Bold); // définir le style et la taille de la police
        this.quitLabel.ForeColor = Color.White; // définir la couleur du texte
        this.quitLabel.BackColor = Color.Transparent; // rendre l'arrière-plan transparent
        this.quitLabel.BorderStyle = BorderStyle.FixedSingle; // ajouter une bordure autour du label
        this.quitLabel.TextAlign = ContentAlignment.MiddleCenter; // centrer le texte dans le label
        this.quitLabel.Click += new EventHandler(this.QuitLabel_Click); // gestionnaire d'événements pour l'événement de clic
        this.quitLabel.MouseEnter += (s, e) => quitLabel.ForeColor = Color.Yellow; // changer la couleur au survol
        this.quitLabel.MouseLeave += (s, e) => quitLabel.ForeColor = Color.White; // réinitialiser la couleur lorsqu'il n'est pas survolé

        // 
        // MenuForm
        // 
        this.ClientSize = new Size(300, 300); // définir la taille du formulaire
        this.Controls.Add(this.titleLabel);
        this.Controls.Add(this.playLabel); // ajouter le label Play au formulaire
        this.Controls.Add(this.quitLabel); // ajouter le label Quit au formulaire
        this.Name = "MenuForm"; // définir le nom du formulaire
        this.Text = "ShootEmUp - Romain"; // définir le titre du formulaire
        this.StartPosition = FormStartPosition.CenterScreen; // centrer le menu à l'écran
        this.BackColor = Color.Black; // définir la couleur de fond pour le contraste
        this.ResumeLayout(false); // reprendre la mise en page après l'initialisation
    }

    private void PlayLabel_Click(object sender, EventArgs e)
    {
        this.Hide(); // masquer le formulaire du menu
        Form1 form1 = new Form1();
        form1.Show();
    }

    private void QuitLabel_Click(object sender, EventArgs e)
    {
        Application.Exit(); // fermer l'application
    }
} 