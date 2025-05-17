namespace TheGameOfLife.Domain.ValueObject;

/// <summary>
/// マス
/// </summary>
public class Square : IEquatable<Square>
{
    public int Position { get; }
    public SquareType SquareType { get; }
    public string Text { get; }
    public int MoneyValue { get; }
    public int SkipTurnCount { get; }

    public Square(int position, string text, SquareType squareType = SquareType.Normal, int moneyValue = 0,
        int skipTurnCount = 0)
    {
        if (position < 1)
            throw new ArgumentException("Positionは1以上でなくてはなりません", nameof(position));

        if (moneyValue < 0)
        {
            throw new ArgumentException("金額は自然数で指定して下さい");
        }

        if (moneyValue != 0 && skipTurnCount != 0)
        {
            throw new ArgumentException("お金と休みの効果を同時に設定することはできません", nameof(moneyValue));
        }

        if ((squareType == SquareType.MoneyGain || squareType == SquareType.MoneyLoss) && moneyValue != 0)
        {
            throw new ArgumentException($"マスの効果と設定値が矛盾しています。SquareType:{squareType}, moneyValue:{moneyValue}");
        }

        if (squareType == SquareType.SkipTurn && skipTurnCount != 0)
        {
            throw new ArgumentException($"マスの効果と設定値が矛盾しています。SquareType:{squareType}, SkipTurnCount:{skipTurnCount}");
        }
        
        Position = position;
        SquareType = squareType;
        Text = text + MakeEffectText(squareType, moneyValue, skipTurnCount);
        MoneyValue = moneyValue;
        SkipTurnCount = skipTurnCount;
    }

    /// <summary>
    /// マスのタイプから、効果が記載された文章を作成する
    /// </summary>
    /// <param name="squareType"></param>
    /// <param name="moneyValue"></param>
    /// <param name="skipTurnCount"></param>
    /// <returns></returns>
    private string MakeEffectText(SquareType squareType, int moneyValue, int skipTurnCount)
    {
        return squareType switch
        {
            SquareType.MoneyGain => $"{moneyValue}円もらう。",
            SquareType.MoneyLoss => $"{moneyValue}円はらう。",
            SquareType.SkipTurn => $"{skipTurnCount}回休み。",
            _ => string.Empty,
        };
    }
    
    public bool Equals(Square? other)
    {
        if (other is null) return false;
        return Position == other.Position &&
               Text == other.Text &&
               SquareType == other.SquareType &&
               MoneyValue == other.MoneyValue &&
               SkipTurnCount == other.SkipTurnCount;
    }

    public override bool Equals(object? obj) => Equals(obj as Square);

    public override int GetHashCode() => HashCode.Combine(Position, SquareType, MoneyValue, SkipTurnCount);
}

public enum SquareType
{
    Normal,
    MoneyGain,
    MoneyLoss,
    SkipTurn,
    ProfessionGain,
    ProfessionLoss
}