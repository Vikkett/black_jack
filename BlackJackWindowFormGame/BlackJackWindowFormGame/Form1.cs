using System;
using System.Drawing;
using System.Windows.Forms;

namespace BlackJackWindowFormGame
{
    // Main form for the Blackjack game
    public partial class Form1 : Form
    {
        private Game game;           // Game instance that controls logic

        // References to game objects
        private Cards cards;         // Deck reference
        private User player;         // Player reference
        private User dealer;         // Dealer reference
        private string hidden;       // Dealer's hidden first card

        private bool canHit = true;  // Whether the player can still take cards

        // Labels to display totals on the form
        private Label playerCardsLabel;
        private Label dealerCardsLabel;

        // Constructor: initializes the form and starts a new game
        public Form1()
        {
            InitializeComponent();   // Initialize form components (required)

            // Create labels
            playerCardsLabel = new Label();
            dealerCardsLabel = new Label();

            // Set positions and sizes
            playerCardsLabel.Location = new Point(20, 20);
            playerCardsLabel.Size = new Size(200, 30);
            dealerCardsLabel.Location = new Point(20, 60);
            dealerCardsLabel.Size = new Size(200, 30);

            Controls.Add(playerCardsLabel);
            Controls.Add(dealerCardsLabel);

            // Connect Designer buttons to methods
            button1.Click += hitButton_Click;   // Hit
            button2.Click += standButton_Click; // Stand

            // Optional: add a Replay button in Designer called "buttonReplay"
            // buttonReplay.Click += replayButton_Click;

            // Initialize game
            game = new Game();
            cards = game.Cards;
            player = game.Player;
            dealer = game.Dealer;
            hidden = game.HiddenCard;

            UpdateUI(); // Show initial totals
        }

        // Update UI labels
        private void UpdateUI(bool revealDealer = false)
        {
            playerCardsLabel.Text = $"Player total: {player.GetFinalSum(cards)}";
            dealerCardsLabel.Text = revealDealer
                ? $"Dealer total: {dealer.GetFinalSum(cards)}"
                : "Dealer total: ?";
        }

        // Hit button
        private void hitButton_Click(object sender, EventArgs e)
        {
            if (canHit)
            {
                game.PlayerHit();
                UpdateUI();

                if (player.GetFinalSum(cards) > 21)
                {
                    canHit = false;
                    game.DealerTurn();
                    UpdateUI(revealDealer: true);
                    MessageBox.Show(game.CheckWinner());
                }
            }
        }

        // Stand button
        private void standButton_Click(object sender, EventArgs e)
        {
            canHit = false;
            game.DealerTurn();
            UpdateUI(revealDealer: true);
            MessageBox.Show(game.CheckWinner());
        }

        // Replay button (call this from a Designer button click)
        private void replayButton_Click(object sender, EventArgs e)
        {
            game.Reset();   // Reset game state
            canHit = true;  // Allow player to hit
            UpdateUI();     // Reset totals display
        }
    }
}
