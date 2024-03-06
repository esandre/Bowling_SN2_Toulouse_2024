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

    [Theory]
    [InlineData(0, 0)]
    [InlineData(0, 1)]
    [InlineData(0, 9)]
    [InlineData(1, 0)]
    [InlineData(9, 0)]
    public void DeuxLancersNonSpare(int quillesTombéesPremierLancer, int quillesTombéesSecondLancer)
    {
        // ETANT DONNE une partie
        var partie = new Partie();

        // QUAND on effectue 2 lancers tombant moins de 10 quilles ensemble
        partie = partie.CompterLancer(quillesTombéesPremierLancer);
        partie = partie.CompterLancer(quillesTombéesSecondLancer);

        // ALORS sa représentation est la concaténation des 2 chiffres
        Assert.Equal($"{quillesTombéesPremierLancer}{quillesTombéesSecondLancer}", partie.Représentation);
    }

    [Fact]
    public void LancerAprèsPremiereFrame()
    {
        // ETANT DONNE un partie ayant déjà une Frame achevée par deux Strike
        var partie = new Partie().CompterLancer(10).CompterLancer(0).CompterLancer(10);

        // QUAND on effectue un lancer
        partie = partie.CompterLancer(1);

        // ALORS sa représentation est celle de la partie avant plus le nombre de quilles tombées
        Assert.Equal("X0/1", partie.Représentation);
    }

    [Fact]
    public void PasFinPartieSiPasStrike()
    {
        // ETANT DONNE une partie de 12 lancers normaux
        var partie12Coups = Enumerable
            .Repeat(0, 12)
            .Aggregate(new Partie(), (partie, quillesTombées) => partie.CompterLancer(quillesTombées));

        // QUAND on en effectue un treizième
        var partie13Coups = partie12Coups.CompterLancer(0);

        // ALORS la partie continue
        Assert.Equal(partie12Coups.Représentation + '0', partie13Coups.Représentation);
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

    [Fact]
    public void FinPartieNormale()
    {
        // ETANT DONNE une partie ayant effectué 20 lancers
        var partieTerminée = Enumerable
            .Repeat(0, 20)
            .Aggregate(new Partie(), (partie, quillesTombées) => partie.CompterLancer(quillesTombées));

        // QUAND on effectue un lancer supplémentaire
        var partieTestée = partieTerminée.CompterLancer(0);

        // ALORS sa représentation reste la même
        Assert.Equal(partieTerminée.Représentation, partieTestée.Représentation);
    }
}