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
    }
}