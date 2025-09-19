namespace BlackJackWindowFormGame
{
    // Represents a player or dealer
    public class User
    {
        public int Sum { get; private set; }       // Current total value of hand
        public int AceCount { get; private set; }  // Number of Aces in hand

        // Add a card to the hand
        public void AddCard(string card, Cards deck)
        {
            Sum += deck.GetValue(card);
            AceCount += deck.CheckAce(card);
        }

        // Reset hand for a new game
        public void Reset()
        {
            Sum = 0;
            AceCount = 0;
        }

        // Get final sum, adjusting for Aces
        public int GetFinalSum(Cards deck)
        {
            return deck.AdjustForAce(Sum, AceCount);
        }
    }
}
