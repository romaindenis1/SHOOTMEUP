using LotOfWindowsSpaceInvader;
using System;
using System.Windows.Forms;

namespace YourNamespace
{
    class Program
    {
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MenuForm());
        }
    }
}
