namespace Cards
{
    public class StartUp
    {
        public static void Main()
        {
            string[] inputCards = Console.ReadLine().Split(", ");
            List<Card> cards = new List<Card>();

            foreach (string pair in inputCards)
            {
                string[] cardInfo = pair.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string face = cardInfo[0];
                string suit = cardInfo[1];
                try
                {
                    Card card = CreateCard(face, suit);
                    cards.Add(card);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine(string.Join(" ", cards));
        }

        static Card CreateCard(string face, string suit)
        {
            HashSet<string> faces = new HashSet<string>()
            {
                "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"
            };

            Dictionary<string, string> suits = new Dictionary<string, string>()
            {
                { "S", "\u2660" },
                { "H", "\u2665" },
                { "D", "\u2666" },
                { "C", "\u2663" }
            };

            if (!faces.Contains(face) || !suits.ContainsKey(suit))
            {
                throw new ArgumentException("Invalid card!");
            }

            Card card = new Card(face, suits[suit]);
            return card;
        }
    }

    public class Card
    {
        public Card(string face, string suit)
        {
            Face = face;
            Suit = suit;
        }

        public string Face { get; private set; }

        public string Suit { get; private set; }

        public override string ToString()
        {
            return $"[{Face}{Suit}]";
        }
    }
}