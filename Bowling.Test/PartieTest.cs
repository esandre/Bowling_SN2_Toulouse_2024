namespace Bowling.Test;

using CasTest = IEnumerable<object[]>;

public class PartieTest
{
    [Fact]
    public void PartieVide()
    {
        // ETANT DONNE une partie
        var partie = new Partie();

        // ALORS sa représentation est vide
        Assert.Equal("", partie.Représentation);
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

        // ALORS sa représentation est ce nombre
        Assert.Equal(quillesTombées.ToString(), partie.Représentation);
    }

    [Fact]
    public void Strike()
    {
        // ETANT DONNE une partie
        var partie = new Partie();

        // QUAND on effectue un lancer tombant dix quilles
        partie = partie.CompterLancer(10);

        // ALORS sa représentation est ce nombre
        Assert.Equal("X", partie.Représentation);
    }

    public static CasTest CasSpare 
        => Enumerable.Range(0, 10).Select(cas => new object[] { cas });

    [Theory]
    [MemberData(nameof(CasSpare))]
    public void Spare(int quillesTombéesAuPremierLancer)
    {
        // ETANT DONNE une partie
        var partie = new Partie();

        // QUAND on effectue un lancer tombant moins de dix quilles puis un second faisant tomber les autres
        partie = partie.CompterLancer(quillesTombéesAuPremierLancer);
        partie = partie.CompterLancer(10 - quillesTombéesAuPremierLancer);

        // ALORS sa représentation est le nombre de quilles du premier lancer suivi de '/'
        Assert.Equal($"{quillesTombéesAuPremierLancer}/", partie.Représentation);
    }

    [Fact]
    public void FinPartieStrike()
    {
        // ETANT DONNE une partie
        var partie = new Partie();

        // QUAND on effectue douze strikes puis un lancer supplémentaire
        for(var i = 0; i < 12; i++)
            partie = partie.CompterLancer(10);

        partie = partie.CompterLancer(1);

        // ALORS sa représentation est 12 fois X
        Assert.Equal(new string('X', 12), partie.Représentation);
    }
}