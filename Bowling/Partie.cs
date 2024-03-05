namespace Bowling
{
    public class Partie
    {
        public int Score { get; }

        public Partie()
        {
        }

        private Partie(int score)
        {
            Score = score;
        }

        public Partie CompterLancer(int quillesTombées)
        {
            return new Partie(quillesTombées);
        }
    }
}
