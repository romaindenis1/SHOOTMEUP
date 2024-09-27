using System;
using System.Windows.Forms;



//ROMAIN SEMAINE PROCHAINE REGARDE ICI ERRUR EST ICI JPS POURQUOI
namespace LotOfWindowsSpaceInvader;
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1()); 
        }
    }
}
