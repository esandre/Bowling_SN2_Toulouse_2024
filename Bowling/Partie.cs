namespace Bowling
{
    public class Partie
    {
        private const char Strike = 'X';
        private const char Spare = '/';
        private const int NombreQuillesParFrame = 10;
        private const int NombreMaxLancersParPartieFullStrike = 12;

        public string Représentation { get; } = string.Empty;

        public Partie()
        {
        }

        private Partie(Partie étatPrécédent, char action)
        { 
            var premierLancerValide = int.TryParse(étatPrécédent.Représentation, out var nombreQuillesPremierLancer);
            nombreQuillesPremierLancer = premierLancerValide ? nombreQuillesPremierLancer : 0;

            var nombreQuillesSecondLancer = action == Strike ? NombreQuillesParFrame : int.Parse(action.ToString());

            if (premierLancerValide && nombreQuillesPremierLancer + nombreQuillesSecondLancer == NombreQuillesParFrame) Représentation = Spare.ToString();
            else Représentation = étatPrécédent.Représentation + action;
        }

        public Partie CompterLancer(int quillesTombées)
        {
            if (EstTerminée()) return this;

            if (quillesTombées == NombreQuillesParFrame) return new Partie(this, Strike);
            return new Partie(this, quillesTombées.ToString()[0]);
        }

        private int NombreDeStrike => Représentation.Count(c => c == Strike);

        private bool EstTerminée() => NombreDeStrike == NombreMaxLancersParPartieFullStrike;
    }
}
