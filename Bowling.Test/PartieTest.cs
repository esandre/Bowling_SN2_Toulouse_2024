using System.Runtime.InteropServices;

namespace Bowling.Test
{
    public class PartieTest
    {
        [Fact]
        public void ScoreZero()
        {
            // ETANT DONNE une partie
            var partie = new Partie();

            // ALORS son score est de zéro
            Assert.Equal(0, partie.Score);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(9)]
        public void UnLancer(int quillesTombées)
        {
            // ETANT DONNE une partie
            var partie = new Partie();

            // QUAND on effectue un lancer tombant un nombre de quilles de 1 à 9
            partie = partie.CompterLancer(quillesTombées);

            // ALORS son score est de un
            Assert.Equal(quillesTombées, partie.Score);
        }
    }
}