using FluentAssertions;
using StringCalculator;

namespace StringCalculator.Test
{
    public class CalculatorTests
    {
        private Calc ctest = new Calc();

        [Fact]
        public void Add_ReturnsZero_WithEmptyString()
        {
            ctest.add("").Should().Be(0);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("2", 2)]
        [InlineData("432", 432)]
        public void Add_ReturnsInt_WithString(string number, int expected) 
        {
            ctest.add(number).Should().Be(expected);
        }

        [Theory]
        [InlineData("1,2",3)]
        [InlineData("1,2,3",6)]
        public void Add_Returnsaddition_WithComma(string number, int expected)
        {
            ctest.add(number).Should().Be(expected);
        }

        [Fact]
        public void Add_ReturnsAddition_WithDelimiter()
        {
            ctest.add("1\n2").Should().Be(3);
        }

        [Theory]
        [InlineData("//.\n1.2.3",6.0)]
        [InlineData("//;\n1;2;3",6.0)]
        public void Add_ReturnsExpected_WithCustomDelimiter(string numbers,double expected)
        {
            ctest.add(numbers).Should().Be(expected);
        }

        [Fact]
        public void Add_ReturnsInvalidOperationException_WithWrongCustomDelimiter()
        {
            Assert.Throws<InvalidOperationException>(() => ctest.add("//.\n1.2;3"));
        }

        [Fact]
        public void Add_ReturnsListException_WithNegatives()
        {
            Assert.Throws<InvalidOperationException>(() => ctest.add("1,-2,-3,-4,5"));
        }

        [Fact]
        public void Add_Ignore_WithNumbersGreaterThan1000() 
        {
            ctest.add("1,2000,3,4,1000").Should().Be(1008);
        }

        [Fact]
        public void Add_Addition_WithMultipleDelimiters()
        {
            ctest.add("//[*][,]\n5,5*8").Should().Be(18);
        }

        [Fact]
        public void Add_Addition_WithMultipleCharsDelimiters()
        {
            ctest.add("//[...][,][;]\n1...2,3;4").Should().Be(10);
        }
    }
}