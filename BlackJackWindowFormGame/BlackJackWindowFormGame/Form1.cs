// Déclare l’espace de noms principal du jeu
namespace BlackJackWindowFormGame
{
    // Déclare la classe Form1 qui hérite de Form (fenêtre Windows Forms)
    public partial class Form1 : Form
    {
        // La somme totale des cartes du joueur
        private int yourSum = 0;
        // La somme totale des cartes du croupier
        private int dealerSum = 0;
        // Nombre d’As que possède le croupier
        private int dealerCount = 0;
        // Nombre d’As que possède le joueur
        private int yourCount = 0;
        // Carte cachée du croupier
        private string hidden;
        // Liste représentant le paquet de cartes
        private List<string> deck;
        // Indique si le joueur peut encore tirer une carte
        private bool canHit = true;
        // Variable générique (non utilisée pour l’instant)
        private int Count = 0;
        // Constructeur de la fenêtre Form1
        public Form1()
        {
            // Initialise les composants graphiques
            InitializeComponent();

            // Prépare un nouveau jeu
            InitializeGame();
        }

        // Fonction qui initialise un nouveau jeu
        private void InitializeGame()
        {
            // Crée le paquet de cartes
            deck = BuildDeck();
            // Mélange le paquet
            ShuffleDeck();
            // Démarre la partie 
            StartGame();
        }

        // Fonction qui construit un paquet de 52 cartes
        private List<string> BuildDeck()
        {
            // Liste des valeurs possibles pour une carte
            List<string> values = new List<string> { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

            // Liste des types (C = Trèfle, D = Carreau, H = Cœur, S = Pique)
            List<string> types = new List<string> { "C", "D", "H", "S" };

            // Liste qui contiendra toutes les cartes
            List<string> newDeck = new List<string>();

            // Parcourt chaque type de carte
            foreach (string type in types)
            {
                // Parcourt chaque valeur possible
                foreach (string value in values)
                {
                    // Ajoute une carte au paquet sous la forme "valeur-type" (ex: "A-S")
                    newDeck.Add(value + "-" + type);
                }
            }

            // Retourne le paquet complet
            return newDeck;
        }

        // Fonction qui mélange le paquet
        private void ShuffleDeck()
        {
            // Crée un générateur de nombres aléatoires
            Random random = new Random();

            // Parcourt toutes les cartes du paquet
            for (int i = 0; i < deck.Count; i++)
            {
                // Choisit un index aléatoire
                int j = random.Next(deck.Count);

                // Stocke temporairement la carte courante
                string temp = deck[i];

                // Échange la carte courante avec une carte aléatoire
                deck[i] = deck[j];
                deck[j] = temp;
            }
        }

        // Fonction qui ajuste la valeur des As pour éviter de dépasser 21
        private int Calculate(int sum, int aceCount)
        {
            // Tant que la somme est > 21 et qu’il reste des As
            while (sum > 21 && aceCount > 0)
            {
                // Change un As de 11 → 1
                sum -= 10;

                // Réduit le compteur d’As
                aceCount--;
            }

            // Retourne la somme ajustée
            return sum;
        }

        // Fonction qui tire une carte du paquet
        private string Cards()
        {
            // Si le paquet est vide
            if (deck.Count == 0)
            {
                // Reconstruit le paquet
                deck = BuildDeck();

                // Mélange le paquet
                ShuffleDeck();
            }

            // Prend la première carte du paquet
            string card = deck[0];

            // Retire cette carte du paquet
            deck.RemoveAt(0);

            // Retourne la carte tirée
            return card;
        }

        // Retourne la valeur d’une carte (2–10 = valeur numérique, J/Q/K = 10, A = 11)
        private int getValue(string card)
        {
            // Extrait juste la valeur sans le type (ex: "A-S" → "A")
            string value = card.Split('-')[0];

            if (value == "A") return 11;
            if (value == "K" || value == "Q" || value == "J") return 10;
            return int.Parse(value); // pour "2" à "10"
        }

        // Retourne 1 si la carte est un As, sinon 0
        private int checkACE(string card)
        {
            string value = card.Split('-')[0];
            return value == "A" ? 1 : 0;
        }


        // Fonction qui démarre une partie
        private void StartGame()
        {
            // Le croupier reçoit une carte cachée
            hidden = Cards();

            // Ajoute sa valeur à la somme du croupier
            dealerSum += getValue(hidden);

            // Vérifie si c’est un As
            dealerCount += checkACE(hidden);

            // Tant que le croupier a moins de 17
            while (dealerSum < 17)
            {
                // Le croupier tire une nouvelle carte
                string dealerCard = Cards();

                // Ajoute sa valeur à la somme
                dealerSum += getValue(dealerCard);

                // Met à jour le compteur d’As du croupier
                dealerCount += checkACE(dealerCard);
            }

            // Le joueur reçoit 2 cartes au départ
            for (int i = 0; i < 2; i++)
            {
                // Le joueur tire une carte
                string playerCard = Cards();

                // Ajoute sa valeur à la somme du joueur
                yourSum += getValue(playerCard);

                // Met à jour le compteur d’As du joueur
                yourCount += checkACE(playerCard);
            }
        }
    }
}
