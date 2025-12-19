namespace Cheat
{
    internal class Display
    {
        public const int CardWidth = 11;
        public const int CardHeight = 7;

        public static void Deck(Deck deck, int startPosition, int endPosition)
        {
            if (endPosition > deck.Length - 1)
            {
                endPosition = deck.Length - 1;
            }

            Card current;
            for (int i = startPosition; i < endPosition; i++)
            {
                current = deck.GetCard(i);
                Card(current);
            }
        }

        public static void Card(Card card)
        {
            string rightRank, leftRank;
            string rank = card.GetRank();
            char suit = card.GetSuit();
            bool selected = card.GetSelected();

            if (rank == "10")
            {
                rightRank = rank;
                leftRank = rank;
            }
            else
            {
                rightRank = rank + " ";
                leftRank = " " + rank;
            }
            if (selected)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.Write("┌─────────┐");
            NextLineCard();
            Console.Write($"│{leftRank}       │");
            NextLineCard();
            Console.Write("│         │");
            NextLineCard();
            Console.Write($"│    {suit}    │");
            NextLineCard();
            Console.Write("│         │");
            NextLineCard();
            Console.Write($"│       {rightRank}│");
            NextLineCard();
            Console.Write("└─────────┘");
            NextLineCard();
            Console.SetCursorPosition(Console.GetCursorPosition().Left + CardWidth, Console.GetCursorPosition().Top - CardHeight);
            Console.ResetColor();
        }

        public static void ShortCard(string side, Card card)
        {
            string rightRank, leftRank;
            string rank = card.GetRank();
            char suit = card.GetSuit();
            bool selected = card.GetSelected();
            if (rank == "10")
            {
                rightRank = rank;
                leftRank = rank;
            }
            else
            {
                rightRank = rank + " ";
                leftRank = " " + rank;
            }
            if (selected)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            if (side == "left")
            {
                Console.WriteLine();
                Console.Write("┌─────");
                NextLineSmallCard();
                Console.Write($"│{leftRank}   ");
                NextLineSmallCard();
                Console.Write($"│    {suit}");
                NextLineSmallCard();
                Console.Write("│     ");
                NextLineSmallCard();
                Console.Write("└─────");
                Console.SetCursorPosition(Console.GetCursorPosition().Left, Console.GetCursorPosition().Top - 5);

            }
            else
            {
                Console.Write("─────┐");
                NextLineSmallCard();
                Console.Write("     │");
                NextLineSmallCard();
                Console.Write($"{suit}    │");
                NextLineSmallCard();
                Console.Write($"   {rightRank}│");
                NextLineSmallCard();
                Console.Write("─────┘");
            }
            Console.ResetColor();
        }

        public static void ShortEmptyCard(string side)
        {
            if (side == "left")
            {
                Console.WriteLine();
                Console.Write("      ");
                NextLineSmallCard();
                Console.Write("      ");
                NextLineSmallCard();
                Console.Write("      ");
                NextLineSmallCard();
                Console.Write("      ");
                NextLineSmallCard();
                Console.Write("      ");
                Console.SetCursorPosition(Console.GetCursorPosition().Left, Console.GetCursorPosition().Top - 5);
            }
            else
            {
                Console.Write("      ");
                NextLineSmallCard();
                Console.Write("      ");
                NextLineSmallCard();
                Console.Write("      ");
                NextLineSmallCard();
                Console.Write("      ");
                NextLineSmallCard();
                Console.Write("      ");
            }
        }

        private static void NextLineSmallCard()
        {
            Console.SetCursorPosition(Console.GetCursorPosition().Left - 6, Console.GetCursorPosition().Top + 1);
        }

        private static void NextLineCard()
        {
            Console.SetCursorPosition(Console.GetCursorPosition().Left - CardWidth, Console.GetCursorPosition().Top + 1);
        }

        public static void ScrollMenu(Deck deck)
        {
            int current = 0;
            ConsoleKeyInfo inputKey;
            Card selectedCard;
            
            while (true)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                BigCard(deck, current);
                Console.SetCursorPosition(Console.GetCursorPosition().Left + 2, Console.GetCursorPosition().Top - 5);
                DisplaySelectedCards(deck);

                inputKey = Console.ReadKey(true);

                if (inputKey.Key == ConsoleKey.RightArrow)
                {
                    if (current < deck.Length - 1)
                    {
                        current++;
                    }
                }
                else if (inputKey.Key == ConsoleKey.LeftArrow)
                {
                    if (current > 0)
                    {
                        current--;
                    }
                }
                else if (inputKey.Key == ConsoleKey.Spacebar)
                {
                    selectedCard = deck.GetCard(current);
                    if (selectedCard.GetSelected())
                    {
                        selectedCard.DeSelect();
                    }
                    else
                    {
                        selectedCard.Select();
                    }
                }
                else if (inputKey.Key == ConsoleKey.Enter)
                {
                    break;
                }

            }
        }

        public static void BigCard(Deck deck, int card)
        {
            Card middleCard = deck.GetCard(card);
            Card previousCard = null, nextCard = null;

            if (card > 0)
            {
                previousCard = deck.GetCard(card - 1);
            }
            if (card < deck.Length - 1)
            {
               nextCard = deck.GetCard(card + 1);
            }

            if (previousCard != null) // attempt to display the previous card
            {
                Display.ShortCard("left", previousCard);
            }
            else
            {
                Display.ShortEmptyCard("left");
            }

            Display.Card(middleCard); // display the current card

            if (nextCard != null) // attempt to display the next card
            {
                Console.SetCursorPosition(Console.GetCursorPosition().Left + (CardWidth / 2) - 5, Console.GetCursorPosition().Top + 1);
                Display.ShortCard("right", nextCard);
            }
            else
            {
                Console.SetCursorPosition(Console.GetCursorPosition().Left + (CardWidth / 2) - 5, Console.GetCursorPosition().Top + 1);
                Display.ShortEmptyCard("right");
            }
        }

        public static void DisplaySelectedCards(Deck deck)
        {
            Console.Write("Selected Cards:");
            Console.SetCursorPosition(Console.GetCursorPosition().Left - 15, Console.GetCursorPosition().Top + 1); // "Selected Cards" is 15 chars
            List<Card> selectedCards = new List<Card>();
            foreach (Card card in deck.GetCards())
            {
                if (card.GetSelected())
                {
                    selectedCards.Add(card);
                }
            }
            foreach (Card card in selectedCards)
            {
                Console.Write(card.GetName());
                Console.SetCursorPosition(Console.GetCursorPosition().Left - card.GetName().Length, Console.GetCursorPosition().Top + 1);
            }

        }
    }
}
