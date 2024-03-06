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

    [Theory]
    [InlineData(0, 0)]
    [InlineData(0, 1)]
    [InlineData(0, 9)]
    [InlineData(1, 0)]
    [InlineData(9, 0)]
    public void DeuxLancersNonSpare(int quillesTomb�esPremierLancer, int quillesTomb�esSecondLancer)
    {
        // ETANT DONNE une partie
        var partie = new Partie();

        // QUAND on effectue 2 lancers tombant moins de 10 quilles ensemble
        partie = partie.CompterLancer(quillesTomb�esPremierLancer);
        partie = partie.CompterLancer(quillesTomb�esSecondLancer);

        // ALORS sa repr�sentation est la concat�nation des 2 chiffres
        Assert.Equal($"{quillesTomb�esPremierLancer}{quillesTomb�esSecondLancer}", partie.Repr�sentation);
    }

    [Fact]
    public void LancerApr�sPremiereFrame()
    {
        // ETANT DONNE un partie ayant d�j� une Frame achev�e par deux Strike
        var partie = new Partie().CompterLancer(10).CompterLancer(0).CompterLancer(10);

        // QUAND on effectue un lancer
        partie = partie.CompterLancer(1);

        // ALORS sa repr�sentation est celle de la partie avant plus le nombre de quilles tomb�es
        Assert.Equal("X0/1", partie.Repr�sentation);
    }

    [Fact]
    public void PasFinPartieSiPasStrike()
    {
        // ETANT DONNE une partie de 12 lancers normaux
        var partie12Coups = Enumerable
            .Repeat(0, 12)
            .Aggregate(new Partie(), (partie, quillesTomb�es) => partie.CompterLancer(quillesTomb�es));

        // QUAND on en effectue un treizi�me
        var partie13Coups = partie12Coups.CompterLancer(0);

        // ALORS la partie continue
        Assert.Equal(partie12Coups.Repr�sentation + '0', partie13Coups.Repr�sentation);
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

    [Fact]
    public void FinPartieNormale()
    {
        // ETANT DONNE une partie ayant effectu� 20 lancers
        var partieTermin�e = Enumerable
            .Repeat(0, 20)
            .Aggregate(new Partie(), (partie, quillesTomb�es) => partie.CompterLancer(quillesTomb�es));

        // QUAND on effectue un lancer suppl�mentaire
        var partieTest�e = partieTermin�e.CompterLancer(0);

        // ALORS sa repr�sentation reste la m�me
        Assert.Equal(partieTermin�e.Repr�sentation, partieTest�e.Repr�sentation);
    }
}