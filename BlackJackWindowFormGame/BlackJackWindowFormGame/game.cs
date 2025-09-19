namespace BlackJackWindowFormGame
{
    // Controls game flow, dealer AI, and winner checking
    public class Game
    {
        private Cards cards;         // Deck of cards
        private string hidden;       // Dealer's hidden first card

        public User Player { get; private set; }   // Player instance
        public User Dealer { get; private set; }   // Dealer instance

        // Public getters for Form1 to access deck and hidden card
        public Cards Cards => cards;
        public string HiddenCard => hidden;

        // Constructor: create deck, player, dealer, start game
        public Game()
        {
            cards = new Cards();
            Player = new User();
            Dealer = new User();
            StartGame();
        }

        // Initial deal
        private void StartGame()
        {
            hidden = cards.DrawCard();     // Dealer hidden card
            Dealer.AddCard(hidden, cards);

            Dealer.AddCard(cards.DrawCard(), cards); // Dealer visible card

            for (int i = 0; i < 2; i++)
                Player.AddCard(cards.DrawCard(), cards); // Player starts with 2 cards
        }

        // Player chooses to hit
        public void PlayerHit()
        {
            Player.AddCard(cards.DrawCard(), cards);
        }

        // Dealer automatic AI
        public void DealerTurn()
        {
            int dealerSum = Dealer.GetFinalSum(cards);

            while (dealerSum < 17)
            {
                Dealer.AddCard(cards.DrawCard(), cards);
                dealerSum = Dealer.GetFinalSum(cards);
            }
        }

        public void Reset()
        {
            // Clear player and dealer hands
            Player.Reset();
            Dealer.Reset();

            // Create a new shuffled deck
            cards = new Cards();

            // Start a new round
            StartGame();
        }


        // Compare player and dealer hands to decide winner
        public string CheckWinner()
        {
            int playerTotal = Player.GetFinalSum(cards);
            int dealerTotal = Dealer.GetFinalSum(cards);

            if (playerTotal > 21) return "Player busts! Dealer wins.";
            if (dealerTotal > 21) return "Dealer busts! Player wins.";
            if (playerTotal > dealerTotal) return "Player wins!";
            if (playerTotal < dealerTotal) return "Dealer wins!";
            return "It's a tie!";
        }
    }
}
