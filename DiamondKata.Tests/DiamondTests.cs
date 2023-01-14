using System.Text;

namespace DiamondKata.Tests
{
    public class DiamondTests
    {
        private readonly IDiamond _diamond;

        public DiamondTests() : this (new Diamond())
        {
        }

        private DiamondTests(IDiamond diamond)
        {
            _diamond = diamond;
        }

        [Theory]
        [InlineData("", "Invalid character supplied - must be an alphabetical single character")]
        [InlineData("£", "Invalid character supplied - must be an alphabetical single character")]
        [InlineData("£E", "Invalid character supplied - must be an alphabetical single character")]
        [InlineData("E£", "Invalid character supplied - must be an alphabetical single character")]
        [InlineData("Ab", "Invalid character supplied - must be an alphabetical single character")]
        [InlineData("Ef", "Invalid character supplied - must be an alphabetical single character")]
        public void Invalid_Supplied_Character_Throws_Argument_Exception(string character, string errorMessage)
        {
            var result = Assert.Throws<ArgumentException>(() => _diamond.PlotDiamond(character));
            Assert.Equal(errorMessage, result.Message);
        }

        [Fact]
        public void Null_Character_Throws_Argument_Null_Exception()
        {
            var result = Assert.Throws<ArgumentNullException>(() => _diamond.PlotDiamond(null));
            Assert.Equal("Value cannot be null.", result.Message);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("A")]
        public void Character_A_Returns_Correct_Result(string character)
        {
            var expectedResult = new StringBuilder();

            expectedResult.Append('A');
            expectedResult.AppendLine(string.Empty);
            
            var result = _diamond.PlotDiamond(character);
           Assert.Equal(expectedResult.ToString(), result); 
        }

        [Theory]
        [InlineData("e")]
        [InlineData("E")]
        public void Character_E_Returns_Correct_Result(string character)
        {
            var expectedResult = new StringBuilder();
            
            expectedResult.AppendLine("----A----");
            expectedResult.AppendLine("---B-B---");
            expectedResult.AppendLine("--C---C--");
            expectedResult.AppendLine("-D-----D-");
            expectedResult.AppendLine("E-------E");
            expectedResult.AppendLine("-D-----D-");
            expectedResult.AppendLine("--C---C--");
            expectedResult.AppendLine("---B-B---");
            expectedResult.AppendLine("----A----");
            
            var result = _diamond.PlotDiamond(character);
            Assert.Equal(expectedResult.ToString(), result);
        }
    }
}