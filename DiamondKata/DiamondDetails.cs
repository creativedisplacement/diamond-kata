namespace DiamondKata;

public record DiamondDetails
{
    public char Character { get; init; }
    public int NumberOfTrailingSpaces { get; init; }
    public int NumberOfInternalSpaces { get; init; }
}