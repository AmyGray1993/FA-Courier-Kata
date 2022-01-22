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
        public void GetParcelCost_ParcelWithSpeedyShipping_TotalCostIsDoubled()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new Parcel
            {
                Width = 9.9M,
                Height = 9.9M,
                Depth = 9.9M,
                WeightKg = 3
            };

            // Act
            var result = sut.GetParcelCost(parcel, true);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.Small);
            result.ItemCost.Should().Be(3);
            result.ExcessWeightCost.Should().Be(4);
            // (itemCost + excessWeightcost) * 2
            // (3 + 4) * 2
            result.SubTotal.Should().Be(14);
        }

        [Fact]
        public void GetParcelCost_SmallParcelExceedsLimitBy2kg_TotalCostIs7()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new Parcel
            {
                Width = 9.9M,
                Height = 9.9M,
                Depth = 9.9M,
                WeightKg = 3
            };

            // Act
            var result = sut.GetParcelCost(parcel, false);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.Small);
            result.ItemCost.Should().Be(3);
            result.ExcessWeightCost.Should().Be(4);
            result.SubTotal.Should().Be(7);
        }

        [Fact]
        public void GetParcelCost_MediumParcelExceedsLimitBy2kg__TotalCostIs12()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new Parcel
            {
                Width = 10,
                Height = 35.9M,
                Depth = 49.9M,
                WeightKg = 5
            };

            // Act
            var result = sut.GetParcelCost(parcel, false);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.Medium);
            result.ItemCost.Should().Be(8);
            result.ExcessWeightCost.Should().Be(4);
            result.SubTotal.Should().Be(12);
        }

        [Fact]
        public void GetParcelCost_LargeParcelExceedsLimitBy2kg_TotalCostIs19()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new Parcel
            {
                Width = 50,
                Height = 75.9M,
                Depth = 99.9M,
                WeightKg = 8
            };

            // Act
            var result = sut.GetParcelCost(parcel, false);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.Large);
            result.ItemCost.Should().Be(15);
            result.ExcessWeightCost.Should().Be(4);
            result.SubTotal.Should().Be(19);
        }

        [Fact]
        public void GetParcelCost_ExtraLargeParcelExceedsLimitBy2kg_TotalCostIs29()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new Parcel
            {
                Width = 100,
                Height = 50.9M,
                Depth = 50.9M,
                WeightKg = 12
            };

            // Act
            var result = sut.GetParcelCost(parcel, false);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.ExtraLarge);
            result.ItemCost.Should().Be(25);
            result.ExcessWeightCost.Should().Be(4);
            result.SubTotal.Should().Be(29);
        }

        [Fact(Skip="true")]
        public void GetParcelCost_SmallParcelWithSpeedyShipping_TotalCostIs6()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new Parcel
            {
                Width = 9.9M,
                Height = 9.9M,
                Depth = 9.9M
            };

            // Act
            var result = sut.GetParcelCost(parcel, true);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.Small);
            result.ItemCost.Should().Be(3);
            result.ExcessWeightCost.Should().Be(0);
            result.SubTotal.Should().Be(6);
        }

        [Fact(Skip = "true")]
        public void GetParcelCost_MediumParcelWithSpeedyShipping_TotalCostIs16()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new Parcel
            {
                Width = 10,
                Height = 35.9M,
                Depth = 49.9M
            };

            // Act
            var result = sut.GetParcelCost(parcel, true);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.Medium);
            result.ItemCost.Should().Be(8);
            result.ExcessWeightCost.Should().Be(0);
            result.SubTotal.Should().Be(16);
        }

        [Fact(Skip = "true")]
        public void GetParcelCost_LargeParcelWithSpeedyShipping_TotalCostIs30()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new Parcel
            {
                Width = 50,
                Height = 75.9M,
                Depth = 99.9M,
            };

            // Act
            var result = sut.GetParcelCost(parcel, true);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.Large);
            result.ItemCost.Should().Be(15);
            result.ExcessWeightCost.Should().Be(0);
            result.SubTotal.Should().Be(30);
        }

        [Fact(Skip = "true")]
        public void GetParcelCost_ExtraLargeParcelWithSpeedyShipping_TotalCostIs50()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new Parcel
            {
                Width = 100,
                Height = 50.9M,
                Depth = 50.9M
            };

            // Act
            var result = sut.GetParcelCost(parcel, true);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.ExtraLarge);
            result.ItemCost.Should().Be(25);
            result.ExcessWeightCost.Should().Be(0);
            result.SubTotal.Should().Be(50);
        }

        [Fact(Skip = "true")]
        public void GetParcelCost_HeavyParcelWithSpeedyShipping_TotalCostIs50()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new Parcel
            {
                Width = 100,
                Height = 50.9M,
                Depth = 50.9M,
                WeightKg = 25
            };

            // Act
            var result = sut.GetParcelCost(parcel, true);

            // Assert
            /*
                Based on dimensions alone, the parcel matches the criteria of an XL parcel.
                However, it would be cheaper for the customer as a heavy parcel

                var parcel = new Parcel
                {
                    Width = 100,
                    Height = 50.9M,
                    Depth = 50.9M,
                    WeightKg = 25
                };

                XL = $25
                Weight limit = 10kg
                Excess = 15kg
                Excess cost = 15 * 2 = $30
                Total = £55 * 2 (Speedy Shipping Applied) = £110

                Heavy = $50
                Weight limit = 50kg
                Excess = 0
                Excess cost = 0 = $0
                Total = £50 * 2 (Speedy Shipping Applied) = £100
            */

            result.ParcelSize.Should().Be(ParcelSize.Heavy);
            result.ItemCost.Should().Be(50);
            result.ExcessWeightCost.Should().Be(0);
            result.SubTotal.Should().Be(100);
        }

        [Fact]
        public void GetParcelCost_HeavyParcelNoExcess_TotalCostIs50()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new Parcel
            {
                Width = 100,
                Height = 50.9M,
                Depth = 50.9M,
                WeightKg = 25
            };

            // Act
            var result = sut.GetParcelCost(parcel, false);

            // Assert
            /*
                Based on dimensions alone, the parcel matches the criteria of an XL parcel.
                However, it would be cheaper for the customer as a heavy parcel

                var parcel = new Parcel
                {
                    Width = 100,
                    Height = 50.9M,
                    Depth = 50.9M,
                    WeightKg = 25
                };

                XL = $25
                Weight limit = 10kg
                Excess = 15kg
                Excess cost = 15 * 2 = $30
                Total = £55

                Heavy = $50
                Weight limit = 50kg
                Excess = 0
                Excess cost = 0 = $0
                Total = £50
            */

            result.ParcelSize.Should().Be(ParcelSize.Heavy);
            result.ItemCost.Should().Be(50);
            result.ExcessWeightCost.Should().Be(0);
            result.SubTotal.Should().Be(50);
        }

        [Fact]
        public void GetParcelCost_HeavyParcelExceedsLimitBy10kg_TotalCostIs60()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new Parcel
            {
                Width = 100,
                Height = 50.9M,
                Depth = 50.9M,
                WeightKg = 60
            };

            // Act
            var result = sut.GetParcelCost(parcel, false);

            // Assert
            /*
                Based on dimensions alone, the parcel matches the criteria of an XL parcel.
                However, it would be cheaper for the customer as a heavy parcel

                var parcel = new Parcel
                {
                    Width = 100,
                    Height = 50.9M,
                    Depth = 50.9M,
                    WeightKg = 60
                };

                XL = $25
                Weight limit = 10kg
                Excess = 50kg
                Excess cost = 50 * 2 = $100
                Total = £125

                Heavy = $50
                Weight limit = 50kg
                Excess = 10kg
                Excess cost = 10 * 1 = $10
                Total = £60
            */

            result.ParcelSize.Should().Be(ParcelSize.Heavy);
            result.ItemCost.Should().Be(50);
            result.ExcessWeightCost.Should().Be(10);
            result.SubTotal.Should().Be(60);
        }

        [Theory]
        [InlineData(15, ParcelSize.ExtraLarge, 35)]
        [InlineData(25, ParcelSize.Heavy, 50)]
        public void GetParcelCost_ParcelExceedsLimit_ReturnsCheapestOption(decimal parcelWeight, ParcelSize expectedParcelSize, decimal expectedTotal)
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new Parcel
            {
                Width = 100,
                Height = 50.9M,
                Depth = 50.9M,
                WeightKg = parcelWeight
            };

            // Act
            var result = sut.GetParcelCost(parcel, false);

            // Assert
            /*
                Based on dimensions alone, the parcel matches the criteria of an XL parcel.
                However, it would be cheaper for the customer as a heavy parcel

                var parcel = new Parcel
                {
                    Width = 100,
                    Height = 50.9M,
                    Depth = 50.9M,
                    WeightKg = 15
                };

                XL = $25
                Weight limit = 10kg
                Excess = 15 - 10 = 5kg
                Excess cost = 5 * 2 = $10
                Total = $35

                Heavy = $50
                Weight limit = 50kg
                Excess = 0kg
                Excess cost = 0
                Total = $50
            */

            result.ParcelSize.Should().Be(expectedParcelSize);
            result.SubTotal.Should().Be(expectedTotal);
        }

        [Fact]
        public void GetParcelCost_AllDimensionsAreLessThan10_IsSmallParcel()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new Parcel
            {
                Width = 9.9M,
                Height = 9.9M,
                Depth = 9.9M,
                WeightKg = 1
            };

            // Act
            var result = sut.GetParcelCost(parcel, false);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.Small);
            result.ItemCost.Should().Be(3);
            result.ExcessWeightCost.Should().Be(0);
            result.SubTotal.Should().Be(3);
        }

        [Fact]
        public void GetParcelCost_AllDimensionAreGreaterThanOrEqualTo10ButLessThan50_IsMediumParcel()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new Parcel
            {
                Width = 10,
                Height = 35.9M,
                Depth = 49.9M,
                WeightKg = 3
            };

            // Act
            var result = sut.GetParcelCost(parcel, false);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.Medium);
            result.ItemCost.Should().Be(8);
            result.ExcessWeightCost.Should().Be(0);
            result.SubTotal.Should().Be(8);
        }

        [Fact]
        public void GetParcelCost_OneDimensionIsGreaterThanOrEqualTo10ButLessThan50_IsMediumParcel()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new Parcel
            {
                Width = 10,
                Height = 9.9M,
                Depth = 9.9M,
                WeightKg = 3
            };

            // Act
            var result = sut.GetParcelCost(parcel, false);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.Medium);
            result.ItemCost.Should().Be(8);
            result.ExcessWeightCost.Should().Be(0);
            result.SubTotal.Should().Be(8);
        }

        [Fact]
        public void GetParcelCost_AllDimensionAreGreaterThanOrEqualTo50ButLessThan100_IsLargeParcel()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new Parcel
            {
                Width = 50,
                Height = 75.9M,
                Depth = 99.9M,
                WeightKg = 6
            };

            // Act
            var result = sut.GetParcelCost(parcel, false);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.Large);
            result.ItemCost.Should().Be(15);
            result.ExcessWeightCost.Should().Be(0);
            result.SubTotal.Should().Be(15);
        }

        [Fact]
        public void GetParcelCost_OneDimensionIsGreaterThanOrEqualTo50ButLessThan100_IsLargeParcel()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new Parcel
            {
                Width = 50,
                Height = 49.9M,
                Depth = 49.9M,
                WeightKg = 6
            };

            // Act
            var result = sut.GetParcelCost(parcel, false);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.Large);
            result.ItemCost.Should().Be(15);
            result.ExcessWeightCost.Should().Be(0);
            result.SubTotal.Should().Be(15);
        }

        [Fact]
        public void GetParcelCost_OneDimensionIsGreaterThan100_IsExtraLargeParcel()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new Parcel
            {
                Width = 101,
                Height = 50.9M,
                Depth = 50.9M,
                WeightKg = 10
            };

            // Act
            var result = sut.GetParcelCost(parcel, false);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.ExtraLarge);
            result.ItemCost.Should().Be(25);
            result.ExcessWeightCost.Should().Be(0);
            result.SubTotal.Should().Be(25);
        }

        [Fact]
        public void GetParcelCost_OneDimensionIsEqualTo100_IsExtraLargeParcel()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new Parcel
            {
                Width = 100,
                Height = 50.9M,
                Depth = 50.9M,
                WeightKg = 10
            };

            // Act
            var result = sut.GetParcelCost(parcel, false);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.ExtraLarge);
            result.ItemCost.Should().Be(25);
            result.ExcessWeightCost.Should().Be(0);
            result.SubTotal.Should().Be(25);
        }

        [Fact]
        public void GetParcelCost_AllDimensionAreGreaterThanOrEqualTo100_ItemCostIs25()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new Parcel
            {
                Width = 100,
                Height = 125.9M,
                Depth = 199.9M,
                WeightKg = 10
            };

            // Act
            var result = sut.GetParcelCost(parcel, false);

            // Assert
            result.ParcelSize.Should().Be(ParcelSize.ExtraLarge);
            result.ItemCost.Should().Be(25);
            result.ExcessWeightCost.Should().Be(0);
            result.SubTotal.Should().Be(25);
        }
    }
}
