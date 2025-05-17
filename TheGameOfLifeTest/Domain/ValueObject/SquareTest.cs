using TheGameOfLife.Domain.ValueObject;

namespace TheGameOfLifeTest.Domain.ValueObject;

public class SquareTest
{
    [Fact(DisplayName = "")]
    public void Constructor_WithMoneyGainType_AppendsEffectTextCorrectly()
    {
        // Arrange
        int position = 1;
        string baseText = "テストマス: ";
        SquareType squareType = SquareType.MoneyGain;
        int moneyValue = 100;
            
        // Act
        var square = new Square(position, baseText, squareType, moneyValue, skipTurnCount: 0);
            
        // Assert
        string expectedText = baseText + $"{moneyValue}円もらう。";
        Assert.Equal(expectedText, square.Text);
        Assert.Equal(position, square.Position);
        Assert.Equal(squareType, square.SquareType);
        Assert.Equal(moneyValue, square.MoneyValue);
        Assert.Equal(0, square.SkipTurnCount);
    }
}