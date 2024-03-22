// Skeleton Program code for the AQA COMP1 Summer 2014 examination this code should be used in conjunction with the Preliminary Material
// written by L-o-ng
// developed in the C# programming environment
// version 3 edited 21/03/2024

using System;
using System.Net.Http.Headers;

namespace _2014_Skeleton_Code
{
    class TCard
    {
        public int Suit;
        public int Rank;
        public TCard()
        {
            Suit = 0;
            Rank = 0;
        }
    }

    class TRecentScore
    {
        public string Name;
        public int Score;
        public TRecentScore()
        {
            Name = "";
            Score = 0;
        }
    }

    internal class Program
    {
        static List<TCard> Deck = new();
        static List<TRecentScore> RecentScores = new();
        static string Choice = "";
        static Random r = new();
        static int NO_OF_RECENT_SCORES = 3;


        static string GetRank(int RankNo)
        {
            string Rank = "";
            if (RankNo == 1)
                Rank = "Ace";
            else if (RankNo == 2)
                Rank = "Two";
            else if (RankNo == 3)
                Rank = "Three";
            else if (RankNo == 4)
                Rank = "Four";
            else if (RankNo == 5)
                Rank = "Five";
            else if (RankNo == 6)
                Rank = "Six";
            else if (RankNo == 7)
                Rank = "Seven";
            else if (RankNo == 8)
                Rank = "Eight";
            else if (RankNo == 9)
                Rank = "Nine";
            else if (RankNo == 10)
                Rank = "Ten";
            else if (RankNo == 11)
                Rank = "Jack";
            else if (RankNo == 12)
                Rank = "Queen";
            else if (RankNo == 13)
                Rank = "King";
            return Rank;
        }

        static string GetSuit(int SuitNo)
        {
            string Suit = "";
            if (SuitNo == 1)
                Suit = "Clubs";
            else if (SuitNo == 2)
                Suit = "Diamonds";
            else if (SuitNo == 3)
                Suit = "Hearts";
            else if (SuitNo == 4)
                Suit = "Spades";
            return Suit;
        }

        static void DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("MAIN MENU");
            Console.WriteLine();
            Console.WriteLine("1. Play game (with shuffle)");
            Console.WriteLine("2. Play game (without shuffle)");
            Console.WriteLine("3. Display recent scores");
            Console.WriteLine("4. Reset recent scores");
            Console.WriteLine();
            Console.Write("Select an option from the menu (or enter q to quit): ");
        }

        static string GetMenuChoice()
        {
            string Choice = Console.ReadLine();
            Console.WriteLine();
            return Choice;
        }

        static void LoadDeck(ref List<TCard> Deck)
        {
            StreamReader CurrentFile = new("deck.txt");
            int Count = 0;

            while (true) {
                string LineFromFile = CurrentFile.ReadLine();
                if (LineFromFile == null)
                {
                    CurrentFile.Close();
                    break;
                }
                Deck[Count].Suit = int.Parse(LineFromFile);
                LineFromFile = CurrentFile.ReadLine();
                Deck[Count].Rank = int.Parse(LineFromFile);
                Count = Count + 1;
            }
        }

        static void ShuffleDeck(ref List<TCard> Deck)
        {
            var SwapSpace = new TCard();
            int NoOfSwaps = 1000;
            for (int NoOfSwapsMadeSoFar = 0; NoOfSwapsMadeSoFar < NoOfSwaps; NoOfSwapsMadeSoFar++)
            {
                int Position1 = r.Next(1, 52);
                int Position2 = r.Next(1, 52);
                SwapSpace.Rank = Deck[Position1].Rank;
                SwapSpace.Suit = Deck[Position1].Suit;
                Deck[Position1].Rank = Deck[Position2].Rank;
                Deck[Position1].Suit = Deck[Position2].Suit;
                Deck[Position2].Rank = SwapSpace.Rank;
                Deck[Position2].Suit = SwapSpace.Suit;
            }
        }

        static void DisplayCard(TCard ThisCard)
        {
            Console.WriteLine();
            Console.WriteLine($"Card is the {GetRank(ThisCard.Rank)} of {GetSuit(ThisCard.Suit)}");
            Console.WriteLine();
        }

        static void GetCard(ref TCard ThisCard, ref List<TCard> Deck, int NoOfCardsTurnedOver)
        {
            ThisCard.Rank = Deck[0].Rank;
            ThisCard.Suit = Deck[0].Suit;
            for (int Count = 0; Count < 51 - NoOfCardsTurnedOver; Count++)
            {
                Deck[Count].Rank = Deck[Count + 1].Rank;
                Deck[Count].Suit = Deck[Count + 1].Suit;
            }
            Deck[51 - NoOfCardsTurnedOver].Suit = 0;
            Deck[51 - NoOfCardsTurnedOver].Rank = 0;
        }

        static bool IsNextCardHigher(TCard LastCard, TCard NextCard)
        {
            bool Higher = false;
            if (NextCard.Rank > LastCard.Rank)
                Higher = true;
            return Higher;
        }

        static string GetPlayerName()
        {
            Console.WriteLine();
            Console.Write("Please enter your name: ");
            string PlayerName = Console.ReadLine();
            Console.WriteLine();
            return PlayerName;
        }

        static string GetChoiceFromUser()
        {
            Console.Write("Do you think the next card will be higher than the last card (enter y or n)? ");
            Choice = Console.ReadLine();
            return Choice;
        }

        static void DisplayEndOfGameMessage(int Score)
        {
            Console.WriteLine();
            Console.WriteLine("Your score was " + Score);
            if (Score == 51)
                Console.WriteLine("WOW! You completed a perfect game.");
            Console.WriteLine();
        }

        static void DisplayCorrectGuessMessage(int Score)
        {
            Console.WriteLine();
            Console.WriteLine("Well done! You guessed correctly.");
            Console.WriteLine($"Your score is now {Score}");
            Console.WriteLine();
        }

        static void ResetRecentScores(ref List<TRecentScore> RecentScores)
        {
            for (int Count = 0; Count < NO_OF_RECENT_SCORES; Count++)
            {
                RecentScores[Count].Name = "";
                RecentScores[Count].Score = 0;
            }
        }

        static void DisplayRecentScores(ref List<TRecentScore> RecentScores)
        {
            Console.WriteLine();
            Console.WriteLine("Recent Scores: ");
            Console.WriteLine();
            for (int Count = 0; Count < NO_OF_RECENT_SCORES; Count++)
            {
                Console.WriteLine(RecentScores[Count].Name + " got a score of " + RecentScores[Count].Score);
            }
            Console.WriteLine();
            Console.WriteLine("Press the Enter key to return to the main menu");
            Console.ReadLine();
            Console.WriteLine();
        }

        static void UpdateRecentScores(ref List<TRecentScore> RecentScores, int Score)
        {
            string PlayerName = GetPlayerName();
            bool FoundSpace = false;
            int Count = 0;
            while (!FoundSpace && Count < NO_OF_RECENT_SCORES)
            {
                if (RecentScores[Count].Name == "")
                    FoundSpace = true;
                else
                    Count = Count + 1;
            }
            if (!FoundSpace)
            {
                for (Count = 0; Count < NO_OF_RECENT_SCORES; Count++)
                {
                    RecentScores[Count].Name = RecentScores[Count].Name;
                    RecentScores[Count].Score = RecentScores[Count].Score;
                }
                Count = NO_OF_RECENT_SCORES - 1;
            }
            RecentScores[Count].Name = PlayerName;
            RecentScores[Count].Score = Score;
        }

        static void PlayGame(ref List<TCard> Deck, ref List<TRecentScore> RecentScores)
        {
            TCard LastCard = new TCard();
            TCard NextCard = new TCard();
            bool GameOver = false;
            GetCard(ref LastCard, ref Deck, 0);
            DisplayCard(LastCard);
            int NoOfCardsTurnedOver = 1;
            while (NoOfCardsTurnedOver < 52 && !GameOver)
            {
                GetCard(ref NextCard, ref Deck, NoOfCardsTurnedOver);
                Choice = "";
                while (Choice != "y" && Choice != "n")
                {
                    Choice = GetChoiceFromUser();
                }
                DisplayCard(NextCard);
                NoOfCardsTurnedOver = NoOfCardsTurnedOver + 1;
                bool Higher = IsNextCardHigher(LastCard, NextCard);
                if ((Higher && Choice == "y") || (!Higher && Choice == "n"))
                {
                    DisplayCorrectGuessMessage(NoOfCardsTurnedOver - 1);
                    LastCard.Rank = NextCard.Rank;
                    LastCard.Suit = NextCard.Suit;
                }
                else
                {
                    GameOver = true;
                }
            }
            if (GameOver)
            {
                DisplayEndOfGameMessage(NoOfCardsTurnedOver - 2);
                UpdateRecentScores(ref RecentScores, NoOfCardsTurnedOver - 2);
            }
            else
            {
                DisplayEndOfGameMessage(51);
                UpdateRecentScores(ref RecentScores, 51);
            }
        }

        static void Main(string[] args)
        {
            for (int Count = 1; Count < 53; Count++)
            {
                Deck.Add(new TCard());
            }
            for (int Count = 1; Count < NO_OF_RECENT_SCORES + 1; Count++)
            {
                RecentScores.Add(new TRecentScore());
            }
            Choice = "";
            while (Choice != "q")
            {
                DisplayMenu();
                Choice = GetMenuChoice();
                if (Choice == "1")
                {
                    LoadDeck(ref Deck);
                    ShuffleDeck(ref Deck);
                    PlayGame(ref Deck, ref RecentScores);
                }
                else if (Choice == "2")
                {
                    LoadDeck(ref Deck);
                    PlayGame(ref Deck, ref RecentScores);
                }
                else if (Choice == "3")
                {
                    DisplayRecentScores(ref RecentScores);
                }
                else if (Choice == "4")
                {
                    ResetRecentScores(ref RecentScores);
                }
            }
        }
    }
}