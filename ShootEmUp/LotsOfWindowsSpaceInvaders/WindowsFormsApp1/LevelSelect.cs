using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace LotOfWindowsSpaceInvader
{
    public class LevelSelect : Form
    {
        private Label titleLabel;                                                                                           //le label du titre
        private Label level1Label;                                                                                          //le label du niveau 1
        private Label level2Label;                                                                                          //le label du niveau 2
        private Label level3Label;                                                                                          //le label du niveau 3

        private int levelIndex;                                                                                             //le index du tableau choisi

        private string EnemySprite1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "enemy1.png");                    //transformation de string pour le chemin du sprite enemi niveau 1
        private string EnemySprite2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "enemy2.png");                    //transformation de string pour le chemin du sprite enemi niveau 2
        private string EnemySprite3 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "enemy3.png");                    //transformation de string pour le chemin du sprite enemi niveau 3

        private LevelSettings[] _levelSettings;                                                                             //liste de levelsettings
        

        public LevelSelect()
        {
            _levelSettings = new LevelSettings[]                                                                                        //
            {                                                                                                                           //
            new LevelSettings { RequiredScore = 25, InvaderSpeedMultiplier = 1.0, EnemySprite = Convert.ToString(EnemySprite1)},        //constructeur de la liste
            new LevelSettings { RequiredScore = 50, InvaderSpeedMultiplier = 1.5, EnemySprite = Convert.ToString(EnemySprite2)},        //
            new LevelSettings { RequiredScore = 75, InvaderSpeedMultiplier = 2.0, EnemySprite = Convert.ToString(EnemySprite3)}         //
            };                                                                                                                          //
            InitializeComponent(); 



            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.Black;
            this.Name = "LevelSelect";

            // Setup de titleLabel
            this.titleLabel = new Label();
            this.titleLabel.Text = "Select Level";
            this.titleLabel.AutoSize = false;
            this.titleLabel.Size = new Size(300, 35);
            this.titleLabel.Location = new Point((this.ClientSize.Width - 300) / 2, 20);                        //le meme bla bla bla
            this.titleLabel.Font = new Font("Arial", 24, FontStyle.Bold);
            this.titleLabel.ForeColor = Color.White;
            this.titleLabel.BackColor = Color.Black;
            this.titleLabel.TextAlign = ContentAlignment.MiddleCenter;

            // Setup de level1Label
            this.level1Label = new Label();
            this.level1Label.Text = "Level 1";
            this.level1Label.Cursor = Cursors.Hand; 
            this.level1Label.AutoSize = false;
            this.level1Label.Size = new Size(100, 35);
            this.level1Label.Location = new Point((this.ClientSize.Width - 100) / 2, 90);
            this.level1Label.Font = new Font("Arial", 18, FontStyle.Bold);
            this.level1Label.ForeColor = Color.White;
            this.level1Label.BackColor = Color.Transparent;
            this.level1Label.BorderStyle = BorderStyle.FixedSingle;
            this.level1Label.TextAlign = ContentAlignment.MiddleCenter;
            this.level1Label.MouseEnter += (s, e) => level1Label.ForeColor = Color.Yellow; 
            this.level1Label.MouseLeave += (s, e) => level1Label.ForeColor = Color.White; 
            this.level1Label.Click += new EventHandler(this.level1Label_Click);

            // Setup de level2Label
            this.level2Label = new Label();
            this.level2Label.Text = "Level 2";
            this.level2Label.Cursor = Cursors.Hand; 
            this.level2Label.AutoSize = false;
            this.level2Label.Size = new Size(100, 35);
            this.level2Label.Location = new Point((this.ClientSize.Width - 100) / 2, 150);
            this.level2Label.Font = new Font("Arial", 18, FontStyle.Bold);
            this.level2Label.ForeColor = Color.White;
            this.level2Label.BackColor = Color.Transparent;
            this.level2Label.BorderStyle = BorderStyle.FixedSingle;
            this.level2Label.TextAlign = ContentAlignment.MiddleCenter;
            this.level2Label.MouseEnter += (s, e) => level2Label.ForeColor = Color.Yellow; 
            this.level2Label.MouseLeave += (s, e) => level2Label.ForeColor = Color.White; 
            this.level2Label.Click += new EventHandler(this.level2Label_Click);

            // Setup de level3Label
            this.level3Label = new Label();
            this.level3Label.Text = "Level 3";
            this.level3Label.Cursor = Cursors.Hand; 
            this.level3Label.AutoSize = false;
            this.level3Label.Size = new Size(100, 35);
            this.level3Label.Location = new Point((this.ClientSize.Width - 100) / 2, 210);
            this.level3Label.Font = new Font("Arial", 18, FontStyle.Bold);
            this.level3Label.ForeColor = Color.White;
            this.level3Label.BackColor = Color.Transparent;
            this.level3Label.BorderStyle = BorderStyle.FixedSingle;
            this.level3Label.TextAlign = ContentAlignment.MiddleCenter;
            this.level3Label.MouseEnter += (s, e) => level3Label.ForeColor = Color.Yellow; 
            this.level3Label.MouseLeave += (s, e) => level3Label.ForeColor = Color.White; 
            this.level3Label.Click += new EventHandler(this.level3Label_Click);


            //ajoute a lecran
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.level1Label);
            this.Controls.Add(this.level2Label);
            this.Controls.Add(this.level3Label);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // LevelSelect
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "LevelSelect";
            this.Load += new System.EventHandler(this.LevelSelect_Load);
            this.ResumeLayout(false);

        }
        /// <summary>
        /// Function qui ouvre le jeu avec lindex de jeu
        /// </summary>
        /// <param name="levelIndex">index du niveau</param>
        private void OpenForm1(int levelIndex)
        {
            LevelSettings settings = _levelSettings[levelIndex];
            Form1 Form1 = new Form1(_levelSettings[levelIndex]); 
            Form1.Show(); 
            this.Hide();
        }

        //ya une maniere plus simple probablement mais sah il est 2h du mat donc voila
        /// <summary>
        /// Evenement de clicke 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="levelIndex">Les settings</param>
        private void level1Label_Click(object sender, EventArgs e)
        {
            levelIndex = 0;
            OpenForm1(levelIndex);
        }

        /// <summary>
        /// Evenement de clicke 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="levelIndex">Les settings</param>
        private void level2Label_Click(object sender, EventArgs e)
        {
            levelIndex = 1;
            OpenForm1(levelIndex);
        }

        /// <summary>
        /// Evenement de clicke 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="levelIndex">Les settings</param>
        private void level3Label_Click(object sender, EventArgs e)
        {
            levelIndex = 2;
            OpenForm1(levelIndex);
        }

        private void LevelSelect_Load(object sender, EventArgs e)
        {

        }
    }
}