using System.Text;
using System.Text.RegularExpressions;

namespace DiamondKata;

public interface IDiamond
{
    string PlotDiamond(string character);
}

public partial class Diamond : IDiamond
{
    [GeneratedRegex("^[a-zA-Z]$")]
    private static partial Regex ValidateCharacter();

    private readonly List<DiamondDetails> _diamondDetails = new();

    public string PlotDiamond(string character)
    {
        //check letter is not null
        if (character is null)
        {
            throw new ArgumentNullException();
        }
        
        //check letter is alphanumeric
        if (!ValidateCharacter().IsMatch(character))
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
        var stringb = new StringBuilder();

        for (var dash = 0; dash < numberOfDashes; dash++)
        {
            stringb.Append('-');
        }

        return stringb.ToString();
    }

    private static string DrawDiamondToConsole(IEnumerable<DiamondDetails> diamondDetails)
    {
        var stringb = new StringBuilder();

        foreach (var diamondDetail in diamondDetails)
        {
            stringb.Append(DrawOuterDiamond(diamondDetail.NumberOfTrailingSpaces));

            stringb.Append(diamondDetail.Character);

            if (diamondDetail.NumberOfInternalSpaces > 0)
            {
                for (var spaces = 0; spaces < diamondDetail.NumberOfInternalSpaces; spaces++)
                {
                    stringb.Append('-');
                }

                stringb.Append(diamondDetail.Character);
            }

            stringb.Append(DrawOuterDiamond(diamondDetail.NumberOfTrailingSpaces));

            stringb.AppendLine(string.Empty);
        }
        
        return stringb.ToString();
    }
}