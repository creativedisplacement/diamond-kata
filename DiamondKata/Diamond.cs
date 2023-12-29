using System.Text;
using System.Text.RegularExpressions;

namespace DiamondKata;

public interface IDiamond
{
    string PlotDiamond(string? character);
}

public class Diamond : IDiamond
{
    private readonly List<DiamondDetails> _diamondDetails = new();

    public string PlotDiamond(string? character)
    {
        //check letter is not null
        if (character is null)
        {
            throw new ArgumentNullException(nameof(character),"Null character supplied - must supply an alphabetical single character");
        }
        
        //check letter is alphanumeric
        if (!Regex.IsMatch(character,"^[a-zA-Z]$", RegexOptions.Compiled))
        {
            throw new ArgumentException("Invalid character supplied - must be an alphabetical single character");
        }

        // take the first letter and make uppercase
        var validCharacter = character
            .ToUpper()
            .ToCharArray()
            .FirstOrDefault();

        CalculateDiamondDetails(validCharacter);
        
        var diamondTop = DrawDiamondToConsole(_diamondDetails);
        
        // as we don't need the supplied character to be printed again, use linq to skip it
        var diamondBottom = DrawDiamondToConsole(_diamondDetails.AsEnumerable().Reverse().Skip(1));

        return $"{diamondTop}{diamondBottom}";
    }

    private void CalculateDiamondDetails(int lastCharacterIndex)
    {
        const int firstCharacterIndex = 'A';
        
        for (var character = firstCharacterIndex; character <= lastCharacterIndex; character++)
        {
            
            _diamondDetails.Add(new DiamondDetails
            {
                Character = (char)character,
                NumberOfInternalSpaces = CalculateInternalSpacing(character),
                NumberOfTrailingSpaces = CalculateLeadingAndTrailingSpacing(character, lastCharacterIndex)
            });
            
        }
    }

    private static int CalculateInternalSpacing(int startingCharacter)
    {
        var spacing = (startingCharacter - 65) * 2;

        if (spacing != 0)
        {
            if (spacing % 2 == 0)
            {
                spacing -= 1;
            }
        }
        else
        {
            spacing = 0;
        }

        return spacing;
    }

    private static int CalculateLeadingAndTrailingSpacing(int startingCharacter, int endingCharacter)
    {
        return endingCharacter % startingCharacter;
    }

    private static string DrawOuterDiamond(int numberOfDashes)
    {
        var stringBuilder = new StringBuilder();

        for (var dash = 0; dash < numberOfDashes; dash++)
        {
            stringBuilder.Append('-');
        }

        return stringBuilder.ToString();
    }

    private static string DrawDiamondToConsole(IEnumerable<DiamondDetails> diamondDetails)
    {
        var stringBuilder = new StringBuilder();

        foreach (var diamondDetail in diamondDetails)
        {
            stringBuilder.Append(DrawOuterDiamond(diamondDetail.NumberOfTrailingSpaces));

            stringBuilder.Append(diamondDetail.Character);

            if (diamondDetail.NumberOfInternalSpaces > 0)
            {
                for (var spaces = 0; spaces < diamondDetail.NumberOfInternalSpaces; spaces++)
                {
                    stringBuilder.Append('-');
                }

                stringBuilder.Append(diamondDetail.Character);
            }

            stringBuilder.Append(DrawOuterDiamond(diamondDetail.NumberOfTrailingSpaces));

            stringBuilder.AppendLine(string.Empty);
        }
        
        return stringBuilder.ToString();
    }
}