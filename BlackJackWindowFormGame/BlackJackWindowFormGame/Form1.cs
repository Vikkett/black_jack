namespace BlackJackWindowFormGame
{
    public partial class Form1 : Form
    {
        private int yourSum = 0;
        private int dealerSum = 0;
        private int dealerCount = 0;
        private int yourCount = 0;
        private string hidden;
        private List<string> deck;
        private bool canHit = true;
        private int Count = 0;

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }
        private void InitializeGame()
        {
            deck = BuildDeck();
            ShuffleDeck();
            //StartGame();


        }

        private List<string> BuildDeck()
        {

            List<string> values = new List<string> { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
            List<string> types = new List<string> { "C", "D", "H", "S" };
            List<string> newDeck = new List<string>();

            foreach (string type in types)
            {
                foreach (string value in values)
                {
                    newDeck.Add(value + "-" + type);
                }
            }
            return newDeck;
        }
        private void ShuffleDeck()
        {
            Random random = new Random();
            for (int i = 0; i < deck.Count; i++)
            {
                int j = random.Next(deck.Count);
                string temp = deck[i];
                deck[i] = deck[j];
                deck[j] = temp;
            }
        }
       /*private void StartGame()
        {
            hidden = deck[0];
            deck.Remove(hidden);
            dealerSum += getValue(hidden);
            dealerCount += checkACE(hidden);

            while (dealerCount < 17)
            {
                string dealerCard = deck[0];
                deck.Remove(dealerCard);
                dealerSum += getValue(dealerCard);
                dealerCount += checkACE(dealerCard);

            }
        }*/
       

       
    }
}
