using FA_Courier_Kata.Domain.Models;
using FA_Courier_Kata.Domain.Services;
using FluentAssertions;
using Xunit;

namespace FA_Courier_Kata.Tests
{
    public class ParcelServiceTests
    {
        [Fact]
        public void CalculateParcelPostage_AllDimensionsAreLessThan10_Return3()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new Parcel
            {
                Width = 9,
                Height = 9,
                Depth = 9
            };

            // Act
            var result = sut.CalculateParcelPostage(parcel);

            // Assert
            result.Should().Be(3);
        }

        [Fact]
        public void CalculateParcelPostage_AllDimensionAreGreaterThanOrEqualTo10ButLessThan50_Return8()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new Parcel
            {
                Width = 10,
                Height = 35,
                Depth = 49
            };

            // Act
            var result = sut.CalculateParcelPostage(parcel);

            // Assert
            result.Should().Be(8);
        }

        [Fact]
        public void CalculateParcelPostage_OneDimensionIsGreaterThanOrEqualTo10ButLessThan50_Return8()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new Parcel
            {
                Width = 10,
                Height = 9,
                Depth = 9
            };

            // Act
            var result = sut.CalculateParcelPostage(parcel);

            // Assert
            result.Should().Be(8);
        }

        [Fact]
        public void CalculateParcelPostage_AllDimensionAreGreaterThanOrEqualTo50ButLessThan100_Return15()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new Parcel
            {
                Width = 50,
                Height = 75,
                Depth = 99
            };

            // Act
            var result = sut.CalculateParcelPostage(parcel);

            // Assert
            result.Should().Be(15);
        }

        [Fact]
        public void CalculateParcelPostage_OneDimensionIsGreaterThanOrEqualTo50ButLessThan100_Return15()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new Parcel
            {
                Width = 50,
                Height = 49,
                Depth = 49
            };

            // Act
            var result = sut.CalculateParcelPostage(parcel);

            // Assert
            result.Should().Be(15);
        }

        [Fact]
        public void CalculateParcelPostage_OneDimensionIsGreaterThan100_Return25()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new Parcel
            {
                Width = 101,
                Height = 50,
                Depth = 50
            };

            // Act
            var result = sut.CalculateParcelPostage(parcel);

            // Assert
            result.Should().Be(25);
        }

        [Fact]
        public void CalculateParcelPostage_OneDimensionIsEqualTo100_Return25()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new Parcel
            {
                Width = 100,
                Height = 50,
                Depth = 50
            };

            // Act
            var result = sut.CalculateParcelPostage(parcel);

            // Assert
            result.Should().Be(25);
        }

        [Fact]
        public void CalculateParcelPostage_AllDimensionAreGreaterThanOrEqualTo100_Return25()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new Parcel
            {
                Width = 100,
                Height = 125,
                Depth = 199
            };

            // Act
            var result = sut.CalculateParcelPostage(parcel);

            // Assert
            result.Should().Be(25);
        }
    }
}
