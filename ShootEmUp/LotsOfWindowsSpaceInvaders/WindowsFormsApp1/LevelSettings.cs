using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LotOfWindowsSpaceInvader
{
    public class LevelSettings : Form
    {
        public int RequiredScore { get; set; }                                                  //le score que le joueur a besoin pour gagner
        public double InvaderSpeedMultiplier { get; set; }                                      //mulitplieur de la vitesse des enemis
        public string EnemySprite { get; set; }                                                 //la photo de lenemi

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // LevelSettings
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "LevelSettings";
            this.Load += new System.EventHandler(this.LevelSettings_Load);
            this.ResumeLayout(false);

        }

        private void LevelSettings_Load(object sender, EventArgs e)
        {

        }
    }


}

