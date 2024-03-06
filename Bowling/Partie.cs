namespace Bowling
{
    public class Partie
    {
        public string Représentation { get; } = string.Empty;

        public Partie()
        {
        }

        private Partie(Partie étatPrécédent, char action)
        { 
            var premierLancerValide = int.TryParse(étatPrécédent.Représentation, out var nombreQuillesPremierLancer);
            nombreQuillesPremierLancer = premierLancerValide ? nombreQuillesPremierLancer : 0;

            var nombreQuillesSecondLancer = action == 'X' ? 10 : int.Parse(action.ToString());

            if (premierLancerValide && nombreQuillesPremierLancer + nombreQuillesSecondLancer == 10) Représentation = "/";
            else Représentation = étatPrécédent.Représentation + action;
        }

        public Partie CompterLancer(int quillesTombées)
        {
            if (EstTerminée()) return this;

            if (quillesTombées == 10) return new Partie(this, 'X');
            return new Partie(this, quillesTombées.ToString()[0]);
        }

        private int NombreDeStrike => Représentation.Count(c => c == 'X');

        private bool EstTerminée() => NombreDeStrike == 12;
    }
}
