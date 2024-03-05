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

            // ALORS son score est de z�ro
            Assert.Equal(0, partie.Score);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(9)]
        public void UnLancer(int quillesTomb�es)
        {
            // ETANT DONNE une partie
            var partie = new Partie();

            // QUAND on effectue un lancer tombant un nombre de quilles de 1 � 9
            partie = partie.CompterLancer(quillesTomb�es);

            // ALORS son score est de un
            Assert.Equal(quillesTomb�es, partie.Score);
        }
    }
}