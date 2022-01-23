using System.Collections.Generic;
using FA_Courier_Kata.Domain.Models;
using FA_Courier_Kata.Domain.Models.Enums;
using FA_Courier_Kata.Domain.Services;
using FluentAssertions;
using Xunit;

namespace FA_Courier_Kata.Tests
{
    public class ParcelServiceTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ProcessShippingRequest_SingleParcel_TotalCostIsValid(bool speedyShipping)
        {
            // Arrange
            var sut = new ParcelService();
            var parcels = new List<Parcel> {
                new Parcel
                {
                    Width = 9.9M,
                    Height = 9.9M,
                    Depth = 9.9M,
                    WeightKg = 3
                }
            };

            var expectedTotal = speedyShipping ? 14 : 7;

            // Act
            var result = sut.ProcessShippingRequest(parcels, speedyShipping);

            // Assert
            var parcelOutput = result.Parcels[0];

            parcelOutput.ParcelSize.Should().Be(ParcelSize.Small);
            parcelOutput.ItemCost.Should().Be(3);
            parcelOutput.ExcessWeightCost.Should().Be(4);
            parcelOutput.SubTotal.Should().Be(7);

            result.SpeedyShipping.Should().Be(speedyShipping);
            result.TotalCost.Should().Be(expectedTotal);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ProcessShippingRequest_MultipleParcels_TotalCostIsValid(bool speedyShipping)
        {
            // Arrange
            var sut = new ParcelService();
            var parcels = new List<Parcel> {
                new Parcel
                {
                    Width = 9.9M,
                    Height = 9.9M,
                    Depth = 9.9M,
                    WeightKg = 1
                },
                new Parcel
                {
                    Width = 10,
                    Height = 35.9M,
                    Depth = 49.9M,
                    WeightKg = 3
                },
            };

            var expectedTotal = speedyShipping ? 22 : 11;

            // Act
            var result = sut.ProcessShippingRequest(parcels, speedyShipping);

            // Assert
            result.Parcels.Count.Should().Be(2);

            var smallParcel = result.Parcels[0];
            smallParcel.ParcelSize.Should().Be(ParcelSize.Small);
            smallParcel.SubTotal.Should().Be(3);

            var mediumParcel = result.Parcels[1];
            mediumParcel.ParcelSize.Should().Be(ParcelSize.Medium);
            mediumParcel.SubTotal.Should().Be(8);

            result.SpeedyShipping.Should().Be(speedyShipping);
            result.TotalCost.Should().Be(expectedTotal);
        }

        [Fact]
        public void ProcessShippingRequest_MultipleParcelsNoExcessWeight_TotalCostIncludesAllSubTotals()
        {
            // Arrange
            var sut = new ParcelService();
            var parcels = new List<Parcel> {
                new Parcel
                {
                    Width = 9.9M,
                    Height = 9.9M,
                    Depth = 9.9M,
                    WeightKg = 1
                },
                new Parcel
                {
                    Width = 10,
                    Height = 35.9M,
                    Depth = 49.9M,
                    WeightKg = 3
                },
                new Parcel
                {
                    Width = 50,
                    Height = 75.9M,
                    Depth = 99.9M,
                    WeightKg = 6
                },
                new Parcel
                {
                    Width = 100,
                    Height = 50.9M,
                    Depth = 50.9M,
                    WeightKg = 10
                },
                new Parcel
                {
                    Width = 100,
                    Height = 50.9M,
                    Depth = 50.9M,
                    WeightKg = 25
                }
            };

            // Act
            var result = sut.ProcessShippingRequest(parcels, false);

            // Assert
            result.Parcels.Count.Should().Be(5);

            var smallParcel = result.Parcels[0];
            smallParcel.ParcelSize.Should().Be(ParcelSize.Small);
            smallParcel.SubTotal.Should().Be(3);

            var mediumParcel = result.Parcels[1];
            mediumParcel.ParcelSize.Should().Be(ParcelSize.Medium);
            mediumParcel.SubTotal.Should().Be(8);

            var largeParcel = result.Parcels[2];
            largeParcel.ParcelSize.Should().Be(ParcelSize.Large);
            largeParcel.SubTotal.Should().Be(15);

            var xlParcel = result.Parcels[3];
            xlParcel.ParcelSize.Should().Be(ParcelSize.ExtraLarge);
            xlParcel.SubTotal.Should().Be(25);

            var heavyParcel = result.Parcels[4];
            heavyParcel.ParcelSize.Should().Be(ParcelSize.Heavy);
            heavyParcel.SubTotal.Should().Be(50);

            result.SpeedyShipping.Should().BeFalse();
            result.TotalCost.Should().Be(101);
        }

        [Fact]
        public void ProcessShippingRequest_MultipleParcelsEachExceedsLimitBy2kg_SubTotalsIncludeExcessWeightCost()
        {
            // Arrange
            var sut = new ParcelService();
            var parcels = new List<Parcel> {
                new Parcel
                {
                    Width = 9.9M,
                    Height = 9.9M,
                    Depth = 9.9M,
                    WeightKg = 3
                },
                new Parcel
                {
                    Width = 10,
                    Height = 35.9M,
                    Depth = 49.9M,
                    WeightKg = 5
                },
                new Parcel
                {
                    Width = 50,
                    Height = 75.9M,
                    Depth = 99.9M,
                    WeightKg = 8
                },
                new Parcel
                {
                    Width = 100,
                    Height = 50.9M,
                    Depth = 50.9M,
                    WeightKg = 12
                }
            };

            // Act
            var result = sut.ProcessShippingRequest(parcels, false);

            // Assert
            result.Parcels.Count.Should().Be(4);

            var smallParcel = result.Parcels[0];
            smallParcel.ParcelSize.Should().Be(ParcelSize.Small);
            smallParcel.ItemCost.Should().Be(3);
            smallParcel.ExcessWeightCost.Should().Be(4);
            smallParcel.SubTotal.Should().Be(7);

            var mediumParcel = result.Parcels[1];
            mediumParcel.ParcelSize.Should().Be(ParcelSize.Medium);
            mediumParcel.ItemCost.Should().Be(8);
            mediumParcel.ExcessWeightCost.Should().Be(4);
            mediumParcel.SubTotal.Should().Be(12);

            var largeParcel = result.Parcels[2];
            largeParcel.ParcelSize.Should().Be(ParcelSize.Large);
            largeParcel.ItemCost.Should().Be(15);
            largeParcel.ExcessWeightCost.Should().Be(4);
            largeParcel.SubTotal.Should().Be(19);

            var xlParcel = result.Parcels[3];
            xlParcel.ParcelSize.Should().Be(ParcelSize.ExtraLarge);
            xlParcel.ItemCost.Should().Be(25);
            xlParcel.ExcessWeightCost.Should().Be(4);
            xlParcel.SubTotal.Should().Be(29);

            result.SpeedyShipping.Should().BeFalse();
            result.TotalCost.Should().Be(67);
        }

        [Theory]
        [InlineData(15, ParcelSize.ExtraLarge, 35)]
        [InlineData(25, ParcelSize.Heavy, 50)]
        public void ProcessShippingRequest_SingleParcelThatExceedsLimit_ReturnsCheapestOption(decimal parcelWeight, ParcelSize expectedParcelSize, decimal expectedTotal)
        {
            // Arrange
            var sut = new ParcelService();
            var parcels = new List<Parcel>
            {
                new Parcel
                {
                    Width = 100,
                    Height = 50.9M,
                    Depth = 50.9M,
                    WeightKg = parcelWeight
                }
            };

            // Act
            var result = sut.ProcessShippingRequest(parcels, false);

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

            var parcelOutput = result.Parcels[0];
            parcelOutput.ParcelSize.Should().Be(expectedParcelSize);
            parcelOutput.SubTotal.Should().Be(expectedTotal);
        }

        [Fact]
        public void ProcessShippingRequest_SingleHeavyParcelExceedsLimitBy10kg_SubTotalIncludesExcessWeightCost()
        {
            // Arrange
            var sut = new ParcelService();
            var parcels = new List<Parcel>
            {
                new Parcel
                {
                    Width = 100,
                    Height = 50.9M,
                    Depth = 50.9M,
                    WeightKg = 60
                }
            };

            // Act
            var result = sut.ProcessShippingRequest(parcels, false);

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

            result.Parcels.Count.Should().Be(1);

            var parcelOutput = result.Parcels[0];
            parcelOutput.ParcelSize.Should().Be(ParcelSize.Heavy);
            parcelOutput.ItemCost.Should().Be(50);
            parcelOutput.ExcessWeightCost.Should().Be(10);
            parcelOutput.SubTotal.Should().Be(60);

            result.TotalCost.Should().Be(60);
            result.SpeedyShipping.Should().BeFalse();
        }

        [Fact]
        public void ProcessShippingRequest_SingleParcelAllDimensionsAreLessThan10_IsSmallParcel()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new List<Parcel> {
                new Parcel
                {
                    Width = 9.9M,
                    Height = 9.9M,
                    Depth = 9.9M,
                    WeightKg = 1
                }
            };

            // Act
            var result = sut.ProcessShippingRequest(parcel, false);

            // Assert
            result.Parcels.Count.Should().Be(1);

            var parcelOutput = result.Parcels[0];
            parcelOutput.ParcelSize.Should().Be(ParcelSize.Small);
            parcelOutput.ItemCost.Should().Be(3);
            parcelOutput.ExcessWeightCost.Should().Be(0);
            parcelOutput.SubTotal.Should().Be(3);
        }

        [Fact]
        public void ProcessShippingRequest_SingleParcelAllDimensionAreGreaterThanOrEqualTo10ButLessThan50_IsMediumParcel()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new List<Parcel>
            {
                new Parcel
                {
                    Width = 10,
                    Height = 35.9M,
                    Depth = 49.9M,
                    WeightKg = 3
                }
            };

            // Act
            var result = sut.ProcessShippingRequest(parcel, false);

            // Assert
            result.Parcels.Count.Should().Be(1);

            var parcelOutput = result.Parcels[0];
            parcelOutput.ParcelSize.Should().Be(ParcelSize.Medium);
            parcelOutput.ItemCost.Should().Be(8);
            parcelOutput.ExcessWeightCost.Should().Be(0);
            parcelOutput.SubTotal.Should().Be(8);
        }

        [Fact]
        public void ProcessShippingRequest_SingleParcelOneDimensionIsGreaterThanOrEqualTo10ButLessThan50_IsMediumParcel()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new List<Parcel>
            {
                new Parcel
                {
                    Width = 10,
                    Height = 9.9M,
                    Depth = 9.9M,
                    WeightKg = 3
                }
            };

            // Act
            var result = sut.ProcessShippingRequest(parcel, false);

            // Assert
            result.Parcels.Count.Should().Be(1);

            var parcelOutput = result.Parcels[0];
            parcelOutput.ParcelSize.Should().Be(ParcelSize.Medium);
            parcelOutput.ItemCost.Should().Be(8);
            parcelOutput.ExcessWeightCost.Should().Be(0);
            parcelOutput.SubTotal.Should().Be(8);
        }

        [Fact]
        public void ProcessShippingRequest_SingleParcelAllDimensionAreGreaterThanOrEqualTo50ButLessThan100_IsLargeParcel()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new List<Parcel>
            {
                new Parcel
                {
                    Width = 50,
                    Height = 75.9M,
                    Depth = 99.9M,
                    WeightKg = 6
                }
            };

            // Act
            var result = sut.ProcessShippingRequest(parcel, false);

            // Assert
            result.Parcels.Count.Should().Be(1);

            var parcelOutput = result.Parcels[0];
            parcelOutput.ParcelSize.Should().Be(ParcelSize.Large);
            parcelOutput.ItemCost.Should().Be(15);
            parcelOutput.ExcessWeightCost.Should().Be(0);
            parcelOutput.SubTotal.Should().Be(15);
        }

        [Fact]
        public void ProcessShippingRequest_SingleParcelOneDimensionIsGreaterThanOrEqualTo50ButLessThan100_IsLargeParcel()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new List<Parcel>
            {
                new Parcel
                {
                    Width = 50,
                    Height = 49.9M,
                    Depth = 49.9M,
                    WeightKg = 6
                }
            };

            // Act
            var result = sut.ProcessShippingRequest(parcel, false);

            // Assert
            result.Parcels.Count.Should().Be(1);

            var parcelOutput = result.Parcels[0];
            parcelOutput.ParcelSize.Should().Be(ParcelSize.Large);
            parcelOutput.ItemCost.Should().Be(15);
            parcelOutput.ExcessWeightCost.Should().Be(0);
            parcelOutput.SubTotal.Should().Be(15);
        }

        [Fact]
        public void ProcessShippingRequest_SingleParcelOneDimensionIsGreaterThan100_IsExtraLargeParcel()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new List<Parcel>
            {
                new Parcel
                {
                    Width = 101,
                    Height = 50.9M,
                    Depth = 50.9M,
                    WeightKg = 10
                }
            };

            // Act
            var result = sut.ProcessShippingRequest(parcel, false);

            // Assert
            result.Parcels.Count.Should().Be(1);

            var parcelOutput = result.Parcels[0];
            parcelOutput.ParcelSize.Should().Be(ParcelSize.ExtraLarge);
            parcelOutput.ItemCost.Should().Be(25);
            parcelOutput.ExcessWeightCost.Should().Be(0);
            parcelOutput.SubTotal.Should().Be(25);
        }

        [Fact]
        public void ProcessShippingRequest_SingleParcelOneDimensionIsEqualTo100_IsExtraLargeParcel()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new List<Parcel>
            {
                new Parcel
                {
                    Width = 100,
                    Height = 50.9M,
                    Depth = 50.9M,
                    WeightKg = 10
                }
            };

            // Act
            var result = sut.ProcessShippingRequest(parcel, false);

            // Assert
            result.Parcels.Count.Should().Be(1);

            var parcelOutput = result.Parcels[0];
            parcelOutput.ParcelSize.Should().Be(ParcelSize.ExtraLarge);
            parcelOutput.ItemCost.Should().Be(25);
            parcelOutput.ExcessWeightCost.Should().Be(0);
            parcelOutput.SubTotal.Should().Be(25);
        }

        [Fact]
        public void ProcessShippingRequest_SingleParcelAllDimensionAreGreaterThanOrEqualTo100_ItemCostIs25()
        {
            // Arrange
            var sut = new ParcelService();
            var parcel = new List<Parcel>
            {
                new Parcel
                {
                    Width = 100,
                    Height = 125.9M,
                    Depth = 199.9M,
                    WeightKg = 10
                }
            };

            // Act
            var result = sut.ProcessShippingRequest(parcel, false);

            // Assert
            result.Parcels.Count.Should().Be(1);

            var parcelOutput = result.Parcels[0];
            parcelOutput.ParcelSize.Should().Be(ParcelSize.ExtraLarge);
            parcelOutput.ItemCost.Should().Be(25);
            parcelOutput.ExcessWeightCost.Should().Be(0);
            parcelOutput.SubTotal.Should().Be(25);
        }
    }
}
