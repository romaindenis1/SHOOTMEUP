/******************************************************************************
** PROGRAMME  BulletTest.cs                                                  **
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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using LotOfWindowsSpaceInvader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LotOfWindowsSpaceInvader.Tests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void GameLaunch_MenuOpens()
        {
            //arrange
            var menuForm = new MenuForm();

            //act
            menuForm.Show();

            //assert
            Assert.IsTrue(menuForm.Visible, "Menu should be visible on game launch.");
        }

        [TestMethod]
        public void HUD_UpdatesOnScoreIncrease()
        {
            //arrange
            var hud = new HUD();

            //act
            hud.IncreaseScore();

            //assert

            Assert.AreEqual(1, hud._scoreValue, "La HUD est mise a jour apres que le score augemente");
        }


        [TestMethod]
        public void HUD_UpdatesOnLivesDecrease()
        {
            //arrange
            var hud = new HUD();

            //act
            hud.DecreaseLives();

            //assert

            Assert.AreEqual(9, hud._livesValue, "HUD should update when player loses a life.");
        }
        

    }
}