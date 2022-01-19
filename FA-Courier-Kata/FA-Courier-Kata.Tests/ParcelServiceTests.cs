using FA_Courier_Kata.Domain.Models;
using FA_Courier_Kata.Domain.Models.Enums;
using FA_Courier_Kata.Domain.Services;
using FluentAssertions;
using Xunit;

namespace FA_Courier_Kata.Tests
{
    public class ParcelServiceTests
    {
        [Fact]
        public void GetParcelCost_AllDimensionsAreLessThan10_Return3()
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
            var result = sut.GetParcelCost(parcel);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.Small);
            result.PostageCost.Should().Be(3);
        }

        [Fact]
        public void GetParcelCost_AllDimensionAreGreaterThanOrEqualTo10ButLessThan50_Return8()
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
            var result = sut.GetParcelCost(parcel);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.Medium);
            result.PostageCost.Should().Be(8);
        }

        [Fact]
        public void GetParcelCost_OneDimensionIsGreaterThanOrEqualTo10ButLessThan50_Return8()
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
            var result = sut.GetParcelCost(parcel);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.Medium);
            result.PostageCost.Should().Be(8);
        }

        [Fact]
        public void GetParcelCost_AllDimensionAreGreaterThanOrEqualTo50ButLessThan100_Return15()
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
            var result = sut.GetParcelCost(parcel);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.Large);
            result.PostageCost.Should().Be(15);
        }

        [Fact]
        public void GetParcelCost_OneDimensionIsGreaterThanOrEqualTo50ButLessThan100_Return15()
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
            var result = sut.GetParcelCost(parcel);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.Large);
            result.PostageCost.Should().Be(15);
        }

        [Fact]
        public void GetParcelCost_OneDimensionIsGreaterThan100_Return25()
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
            var result = sut.GetParcelCost(parcel);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.ExtraLarge);
            result.PostageCost.Should().Be(25);
        }

        [Fact]
        public void GetParcelCost_OneDimensionIsEqualTo100_Return25()
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
            var result = sut.GetParcelCost(parcel);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.ExtraLarge);
            result.PostageCost.Should().Be(25);
        }

        [Fact]
        public void GetParcelCost_AllDimensionAreGreaterThanOrEqualTo100_Return25()
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
            var result = sut.GetParcelCost(parcel);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.ExtraLarge);
            result.PostageCost.Should().Be(25);
        }
    }
}
