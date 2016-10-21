using Microsoft.VisualStudio.TestTools.UnitTesting;
using UglyTrivia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UglyTrivia.Tests
{
    [TestClass()]
    public class GameTests
    {
        private static bool bNotAWinner;
        String[] args;

        [TestMethod()]
        public void addTest()
        {
            Game aGame = new Game();

            aGame.add("Chet");
            aGame.add("Pat");
            aGame.add("Sue");

            Random rand = (args.Length == 0 ? new Random() : new Random(args[0].GetHashCode()));

            do
            {

                aGame.roll(rand.Next(5) + 1);

                if (rand.Next(9) == 7)
                {
                    bNotAWinner = aGame.wrongAnswer();
                }
                else
                {
                    bNotAWinner = aGame.wasCorrectlyAnswered();
                }



            } while (bNotAWinner);
            Assert.AreEqual(false, true);

        }
    }
}