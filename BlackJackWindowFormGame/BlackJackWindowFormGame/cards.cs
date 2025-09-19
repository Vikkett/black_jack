using System;
using System.Collections.Generic;

namespace BlackJackWindowFormGame
{
    // Manages the deck of cards and card-related operations
    public class Cards
    {
        private List<string> deck;        // Stores the current deck
        private Random random = new Random(); // For shuffling

        // Constructor: builds and shuffles a new deck
        public Cards()
        {
            deck = BuildDeck();
            ShuffleDeck();
        }

        // Build a standard 52-card deck
        private List<string> BuildDeck()
        {
            List<string> values = new List<string> { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
            List<string> types = new List<string> { "C", "D", "H", "S" };
            List<string> newDeck = new List<string>();

            foreach (string type in types)
                foreach (string value in values)
                    newDeck.Add(value + "-" + type); // Example: "A-S"

            return newDeck;
        }

        // Shuffle the deck using Fisher-Yates algorithm
        private void ShuffleDeck()
        {
            for (int i = 0; i < deck.Count; i++)
            {
                int j = random.Next(deck.Count);
                string temp = deck[i];
                deck[i] = deck[j];
                deck[j] = temp;
            }
        }

        // Draw the top card from the deck
        public string DrawCard()
        {
            if (deck.Count == 0)
            {
                deck = BuildDeck();
                ShuffleDeck();
            }

            string card = deck[0];
            deck.RemoveAt(0);
            return card;
        }

        // Returns the numeric value of a card
        public int GetValue(string card)
        {
            string value = card.Split('-')[0];
            if (value == "A") return 11;
            if (value == "K" || value == "Q" || value == "J") return 10;
            return int.Parse(value);
        }

        // Returns 1 if card is an Ace, otherwise 0
        public int CheckAce(string card)
        {
            string value = card.Split('-')[0];
            return value == "A" ? 1 : 0;
        }

        // Adjust sum for Aces if total exceeds 21
        public int AdjustForAce(int sum, int aceCount)
        {
            while (sum > 21 && aceCount > 0)
            {
                sum -= 10;
                aceCount--;
            }
            return sum;
        }
    }
}
