namespace Bowling.Test;

using CasTest = IEnumerable<object[]>;

public class PartieTest
{
    [Fact]
    public void PartieVide()
    {
        // ETANT DONNE une partie
        var partie = new Partie();

        // ALORS sa repr�sentation est vide
        Assert.Equal("", partie.Repr�sentation);
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

        // ALORS sa repr�sentation est ce nombre
        Assert.Equal(quillesTomb�es.ToString(), partie.Repr�sentation);
    }

    [Fact]
    public void Strike()
    {
        // ETANT DONNE une partie
        var partie = new Partie();

        // QUAND on effectue un lancer tombant dix quilles
        partie = partie.CompterLancer(10);

        // ALORS sa repr�sentation est ce nombre
        Assert.Equal("X", partie.Repr�sentation);
    }

    public static CasTest CasSpare 
        => Enumerable.Range(0, 10).Select(cas => new object[] { cas });

    [Theory]
    [MemberData(nameof(CasSpare))]
    public void Spare(int quillesTomb�esAuPremierLancer)
    {
        // ETANT DONNE une partie
        var partie = new Partie();

        // QUAND on effectue un lancer tombant moins de dix quilles puis un second faisant tomber les autres
        partie = partie.CompterLancer(quillesTomb�esAuPremierLancer);
        partie = partie.CompterLancer(10 - quillesTomb�esAuPremierLancer);

        // ALORS sa repr�sentation est le nombre de quilles du premier lancer suivi de '/'
        Assert.Equal($"{quillesTomb�esAuPremierLancer}/", partie.Repr�sentation);
    }

    [Fact]
    public void FinPartieStrike()
    {
        // ETANT DONNE une partie
        var partie = new Partie();

        // QUAND on effectue douze strikes puis un lancer suppl�mentaire
        for(var i = 0; i < 12; i++)
            partie = partie.CompterLancer(10);

        partie = partie.CompterLancer(1);

        // ALORS sa repr�sentation est 12 fois X
        Assert.Equal(new string('X', 12), partie.Repr�sentation);
    }
}