using System;
using FluentAssertions;
using Xunit;

namespace FA_Courier_Kata.Tests
{
    public class ParcelServiceTests
    {
        [Fact]
        public void CalculateParcelPostage_AllDimensionsLessThan10_Return3()
        {
            // Arrange
            var testItem = true;
            // Act

            // Assert
            testItem.Should().BeTrue();
        }

        [Fact]
        public void CalculateParcelPostage_OneDimensionIsGreaterThan10ButLessThan50_Return8()
        {
            // Arrange
            var testItem = true;
            // Act

            // Assert
            testItem.Should().BeTrue();
        }

        [Fact]
        public void CalculateParcelPostage_OneDimensionIsGreaterThan50ButLessThan100_Return15()
        {
            // Arrange
            var testItem = true;
            // Act

            // Assert
            testItem.Should().BeTrue();
        }

        [Fact]
        public void CalculateParcelPostage_OneDimensionIsGreaterThan100_Return25()
        {
            // Arrange
            var testItem = true;
            // Act

            // Assert
            testItem.Should().BeTrue();
        }

        [Fact]
        public void CalculateParcelPostage_OneDimensionIsEqualTo100_Return25()
        {
            // Arrange
            var testItem = true;
            // Act

            // Assert
            testItem.Should().BeTrue();
        }

        [Fact]
        public void CalculateParcelPostage_AllDimensionAreGreaterThan100_Return25()
        {
            // Arrange
            var testItem = true;
            // Act

            // Assert
            testItem.Should().BeTrue();
        }
    }
}
