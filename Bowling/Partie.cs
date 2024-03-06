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

            if (TypeDernierLancer == TypeLancer.Numérique) return new Partie(this, Spare); // TODO : Code stupide
            if (quillesTombées == NombreQuillesParFrame) return new Partie(this, Strike);

            return new Partie(this, quillesTombées.ToString()[0]);
        }

        private int NombreDeLancers => Représentation.Length;

        private bool EstTerminée() => NombreDeLancers == NombreMaxLancersParPartieFullStrike; // TODO : Code stupide et contradictoire avec les règles

        private TypeLancer TypeDernierLancer
        {
            get
            {
                if (string.IsNullOrEmpty(Représentation)) 
                    return TypeLancer.Aucun;

                var last = Représentation.Last();
                return last switch
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
