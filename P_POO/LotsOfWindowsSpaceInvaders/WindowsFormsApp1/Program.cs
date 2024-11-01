/******************************************************************************
** PROGRAMME  Program.cs                                                     **
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
using LotOfWindowsSpaceInvader;
using System;
using System.Windows.Forms;

namespace LotOfWindowsSpaceInvader
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
