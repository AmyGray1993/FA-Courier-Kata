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
        public void GetParcelCost_SmallParcelWithSpeedyShipping_TotalCostIs6()
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
            var result = sut.GetParcelCost(parcel, true);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.Small);
            result.ItemCost.Should().Be(3);
            result.PostageCost.Should().Be(3);
            result.TotalCost.Should().Be(6);
        }

        [Fact]
        public void GetParcelCost_MediumParcelWithSpeedyShipping_TotalCostIs16()
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
            var result = sut.GetParcelCost(parcel, true);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.Medium);
            result.ItemCost.Should().Be(8);
            result.PostageCost.Should().Be(8);
            result.TotalCost.Should().Be(16);
        }

        [Fact]
        public void GetParcelCost_LargeParcelWithSpeedyShipping_TotalCostIs30()
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
            var result = sut.GetParcelCost(parcel, true);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.Large);
            result.ItemCost.Should().Be(15);
            result.PostageCost.Should().Be(15);
            result.TotalCost.Should().Be(30);
        }

        [Fact]
        public void GetParcelCost_ExtraLargeParcelWithSpeedyShipping_TotalCostIs50()
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
            var result = sut.GetParcelCost(parcel, true);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.ExtraLarge);
            result.ItemCost.Should().Be(25);
            result.PostageCost.Should().Be(25);
            result.TotalCost.Should().Be(50);
        }

        [Fact]
        public void GetParcelCost_AllDimensionsAreLessThan10_IsSmallParcel()
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
            var result = sut.GetParcelCost(parcel, false);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.Small);
            result.ItemCost.Should().Be(3);
            result.PostageCost.Should().Be(0);
            result.TotalCost.Should().Be(3);
        }

        [Fact]
        public void GetParcelCost_AllDimensionAreGreaterThanOrEqualTo10ButLessThan50_IsMediumParcel()
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
            var result = sut.GetParcelCost(parcel, false);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.Medium);
            result.ItemCost.Should().Be(8);
            result.PostageCost.Should().Be(0);
            result.TotalCost.Should().Be(8);
        }

        [Fact]
        public void GetParcelCost_OneDimensionIsGreaterThanOrEqualTo10ButLessThan50_IsMediumParcel()
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
            var result = sut.GetParcelCost(parcel, false);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.Medium);
            result.ItemCost.Should().Be(8);
            result.PostageCost.Should().Be(0);
            result.TotalCost.Should().Be(8);
        }

        [Fact]
        public void GetParcelCost_AllDimensionAreGreaterThanOrEqualTo50ButLessThan100_IsLargeParcel()
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
            var result = sut.GetParcelCost(parcel, false);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.Large);
            result.ItemCost.Should().Be(15);
            result.PostageCost.Should().Be(0);
            result.TotalCost.Should().Be(15);
        }

        [Fact]
        public void GetParcelCost_OneDimensionIsGreaterThanOrEqualTo50ButLessThan100_IsLargeParcel()
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
            var result = sut.GetParcelCost(parcel, false);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.Large);
            result.ItemCost.Should().Be(15);
            result.PostageCost.Should().Be(0);
            result.TotalCost.Should().Be(15);
        }

        [Fact]
        public void GetParcelCost_OneDimensionIsGreaterThan100_IsExtraLargeParcel()
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
            var result = sut.GetParcelCost(parcel, false);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.ExtraLarge);
            result.ItemCost.Should().Be(25);
            result.PostageCost.Should().Be(0);
            result.TotalCost.Should().Be(25);
        }

        [Fact]
        public void GetParcelCost_OneDimensionIsEqualTo100_IsExtraLargeParcel()
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
            var result = sut.GetParcelCost(parcel, false);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.ExtraLarge);
            result.ItemCost.Should().Be(25);
            result.PostageCost.Should().Be(0);
            result.TotalCost.Should().Be(25);
        }

        [Fact]
        public void GetParcelCost_AllDimensionAreGreaterThanOrEqualTo100_ItemCostIs25()
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
            var result = sut.GetParcelCost(parcel, false);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.ExtraLarge);
            result.ItemCost.Should().Be(25);
            result.PostageCost.Should().Be(0);
            result.TotalCost.Should().Be(25);
        }
    }
}
