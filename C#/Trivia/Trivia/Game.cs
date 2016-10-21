using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UglyTrivia
{
    public class Game
    {


        List<string> lPlayers = new List<string>();

        int[] iPlaces = new int[6];
        int[] iPurses = new int[6];

        bool[] bInPenaltyBox = new bool[6];

        LinkedList<string> lPopQuestions = new LinkedList<string>();
        LinkedList<string> lScienceQuestions = new LinkedList<string>();
        LinkedList<string> lSportsQuestions = new LinkedList<string>();
        LinkedList<string> lRockQuestions = new LinkedList<string>();

        int iCurrentPlayer = 0;
        bool bIsGettingOutOfPenaltyBox;

        public Game()
        {
            for (int i = 0; i < 50; i++)
            {
                lPopQuestions.AddLast("Pop Question " + i);
                lScienceQuestions.AddLast(("Science Question " + i));
                lSportsQuestions.AddLast(("Sports Question " + i));
                lRockQuestions.AddLast(createRockQuestion(i));
            }
        }

        public String createRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool isPlayable()
        {
            return (howManyPlayers() >= 2);
        }

        public bool add(String playerName)
        {


            lPlayers.Add(playerName);
            iPlaces[howManyPlayers()] = 0;
            iPurses[howManyPlayers()] = 0;
            bInPenaltyBox[howManyPlayers()] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + lPlayers.Count);
            return true;
        }

        public int howManyPlayers()
        {
            return lPlayers.Count;
        }

        public void roll(int roll)
        {
            Console.WriteLine(lPlayers[iCurrentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (bInPenaltyBox[iCurrentPlayer])
            {
                if (roll % 2 != 0)
                {
                    bIsGettingOutOfPenaltyBox = true;

                    Console.WriteLine(lPlayers[iCurrentPlayer] + " is getting out of the penalty box");
                    iPlaces[iCurrentPlayer] = iPlaces[iCurrentPlayer] + roll;
                    if (iPlaces[iCurrentPlayer] > 11) iPlaces[iCurrentPlayer] = iPlaces[iCurrentPlayer] - 12;

                    Console.WriteLine(lPlayers[iCurrentPlayer]
                            + "'s new location is "
                            + iPlaces[iCurrentPlayer]);
                    Console.WriteLine("The category is " + currentCategory());
                    askQuestion();
                }
                else
                {
                    Console.WriteLine(lPlayers[iCurrentPlayer] + " is not getting out of the penalty box");
                    bIsGettingOutOfPenaltyBox = false;
                }

            }
            else
            {

                iPlaces[iCurrentPlayer] = iPlaces[iCurrentPlayer] + roll;
                if (iPlaces[iCurrentPlayer] > 11) iPlaces[iCurrentPlayer] = iPlaces[iCurrentPlayer] - 12;

                Console.WriteLine(lPlayers[iCurrentPlayer]
                        + "'s new location is "
                        + iPlaces[iCurrentPlayer]);
                Console.WriteLine("The category is " + currentCategory());
                askQuestion();
            }

        }

        private void askQuestion()
        {
            if (currentCategory() == "Pop")
            {
                Console.WriteLine(lPopQuestions.First());
                lPopQuestions.RemoveFirst();
            }
            if (currentCategory() == "Science")
            {
                Console.WriteLine(lScienceQuestions.First());
                lScienceQuestions.RemoveFirst();
            }
            if (currentCategory() == "Sports")
            {
                Console.WriteLine(lSportsQuestions.First());
                lSportsQuestions.RemoveFirst();
            }
            if (currentCategory() == "Rock")
            {
                Console.WriteLine(lRockQuestions.First());
                lRockQuestions.RemoveFirst();
            }
        }


        private String currentCategory()
        {
            if (iPlaces[iCurrentPlayer] == 0) return "Pop";
            if (iPlaces[iCurrentPlayer] == 4) return "Pop";
            if (iPlaces[iCurrentPlayer] == 8) return "Pop";
            if (iPlaces[iCurrentPlayer] == 1) return "Science";
            if (iPlaces[iCurrentPlayer] == 5) return "Science";
            if (iPlaces[iCurrentPlayer] == 9) return "Science";
            if (iPlaces[iCurrentPlayer] == 2) return "Sports";
            if (iPlaces[iCurrentPlayer] == 6) return "Sports";
            if (iPlaces[iCurrentPlayer] == 10) return "Sports";
            return "Rock";
        }

        public bool wasCorrectlyAnswered()
        {
            if (bInPenaltyBox[iCurrentPlayer])
            {
                if (bIsGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    iPurses[iCurrentPlayer]++;
                    Console.WriteLine(lPlayers[iCurrentPlayer]
                            + " now has "
                            + iPurses[iCurrentPlayer]
                            + " Gold Coins.");

                    bool winner = didPlayerWin();
                    iCurrentPlayer++;
                    if (iCurrentPlayer == lPlayers.Count) iCurrentPlayer = 0;

                    return winner;
                }
                else
                {
                    iCurrentPlayer++;
                    if (iCurrentPlayer == lPlayers.Count) iCurrentPlayer = 0;
                    return true;
                }



            }
            else
            {

                Console.WriteLine("Answer was corrent!!!!");
                iPurses[iCurrentPlayer]++;
                Console.WriteLine(lPlayers[iCurrentPlayer]
                        + " now has "
                        + iPurses[iCurrentPlayer]
                        + " Gold Coins.");

                bool bWinner = didPlayerWin();
                iCurrentPlayer++;
                if (iCurrentPlayer == lPlayers.Count) iCurrentPlayer = 0;

                return bWinner;
            }
        }

        public bool wrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(lPlayers[iCurrentPlayer] + " was sent to the penalty box");
            bInPenaltyBox[iCurrentPlayer] = true;

            iCurrentPlayer++;
            if (iCurrentPlayer == lPlayers.Count) iCurrentPlayer = 0;
            return true;
        }


        private bool didPlayerWin()
        {
            return !(iPurses[iCurrentPlayer] == 6);
        }
    }

}
