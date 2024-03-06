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
            Représentation = étatPrécédent.Représentation + action;
        }

        public Partie CompterLancer(int quillesTombées)
        {
            if (EstTerminée()) return this;

            var estUnSpare = TypeDernierLancer == TypeLancer.Numérique &&
                             ValeurDernierLancer + quillesTombées == NombreQuillesParFrame;
            if (estUnSpare)
                return new Partie(this, Spare);

            if (quillesTombées == NombreQuillesParFrame) 
                return new Partie(this, Strike);

            return new Partie(this, quillesTombées.ToString()[0]);
        }

        private char DernierLancer => Représentation.Last();

        private int ValeurDernierLancer => int.Parse(DernierLancer.ToString());

        private int NombreDeStrike => Représentation.Count(c => c == Strike);

        private bool EstTerminée() => NombreDeStrike == NombreMaxLancersParPartieFullStrike;

        private TypeLancer TypeDernierLancer
        {
            get
            {
                if (string.IsNullOrEmpty(Représentation)) 
                    return TypeLancer.Aucun;

                return DernierLancer switch
                {
                    Strike => TypeLancer.Strike,
                    Spare => TypeLancer.Spare,
                    _ => TypeLancer.Numérique
                };
            }
        }

        private enum TypeLancer
        {
            Aucun,
            Numérique,
            Spare,
            Strike
        }
    }
}
