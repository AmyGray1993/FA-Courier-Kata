using System.Collections.Generic;
using FA_Courier_Kata.Domain.Helpers;
using FA_Courier_Kata.Domain.Models;
using FA_Courier_Kata.Domain.Models.Enums;
using FA_Courier_Kata.Domain.Services.Interfaces;

namespace FA_Courier_Kata.Domain.Services
{
    public class ParcelService : IParcelService
    {
        private readonly int speedyShippingMultiplier = 2;
        private readonly int standardExcessWeightChargePerKg = 2;
        private readonly int heavyExcessWeightChargePerKg = 1;
        private readonly int heavyParcelBaseRate = 50;

        public ParcelService()
        {
        }

        public List<string> GetPostageInvoice(ParcelCost parcelCost, bool speedyShipping)
        {
            var output = new List<string>
            {
                $"{parcelCost.ParcelSize.GetDescription()}: ${parcelCost.ItemCost}."
            };

            if (speedyShipping)
            {
                output.Add($"Speedy Shipping: ${parcelCost.SubTotal / speedyShippingMultiplier}.");
            }

            output.Add($"Total Cost: ${parcelCost.SubTotal}.");

            return output;
        }

        public ParcelCost GetParcelCost(Parcel parcel, bool speedyShipping)
        {
            var parcelSize = GetParcelSize(parcel);

            var basicParcelCost = new ParcelCost
            {
                ParcelDetails = parcel,
                ParcelSize = parcelSize,
                ItemCost = CalculateParcelSizeCost(parcelSize),
                ExcessWeightCost = CalculateExcessWeightCost(parcelSize, parcel.WeightKg),
            };

            if (basicParcelCost.SubTotal > heavyParcelBaseRate)
            {
                var heavyParcelCost = CalculateHeavyParcelRate(parcel);

                return heavyParcelCost.SubTotal < basicParcelCost.SubTotal
                    ? heavyParcelCost
                    : basicParcelCost;
            }

            return basicParcelCost;
        }

        private ParcelSize GetParcelSize(Parcel parcel)
        {
            var width = parcel.Width;
            var height = parcel.Height;
            var depth = parcel.Depth;

            if (width < 10 && height < 10 && depth < 10)
            {
                return ParcelSize.Small;
            }
            else if (width < 50 && height < 50 && depth < 50)
            {
                return ParcelSize.Medium;
            }
            else if (width < 100 && height < 100 && depth < 100)
            {
                return ParcelSize.Large;
            }
            else
            {
                return ParcelSize.ExtraLarge;
            }
        }

        private decimal CalculateParcelSizeCost(ParcelSize parcelSize)
        {
            return parcelSize switch
            {
                ParcelSize.Small => 3,
                ParcelSize.Medium => 8,
                ParcelSize.Large => 15,
                ParcelSize.ExtraLarge => 25,
                ParcelSize.Heavy => heavyParcelBaseRate,
                _ => 0,
            };
        }

        private decimal CalculateExcessWeightCost(ParcelSize parcelSize, decimal parcelWeight)
        {
            var excessWeightCharge = parcelSize == ParcelSize.Heavy
                ? heavyExcessWeightChargePerKg
                : standardExcessWeightChargePerKg;

            var weightLimit = parcelSize switch
            {
                ParcelSize.Small => 1,
                ParcelSize.Medium => 3,
                ParcelSize.Large => 6,
                ParcelSize.ExtraLarge => 10,
                ParcelSize.Heavy => heavyParcelBaseRate,
                _ => 0,
            };

            return parcelWeight > weightLimit
                ? (parcelWeight - weightLimit) * excessWeightCharge
                : 0;
        }

        private ParcelCost CalculateHeavyParcelRate(Parcel parcel)
        {
            return new ParcelCost
            {
                ParcelDetails = parcel,
                ParcelSize = ParcelSize.Heavy,
                ItemCost = CalculateParcelSizeCost(ParcelSize.Heavy),
                ExcessWeightCost = CalculateExcessWeightCost(ParcelSize.Heavy, parcel.WeightKg),
            };
        }
    }
}
