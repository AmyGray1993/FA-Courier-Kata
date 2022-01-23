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

        public ShippingRequest ProcessShippingRequest(List<Parcel> parcels, bool speedyShipping)
        {
            var parcelCosts = new List<ParcelCost>();
            var runningTotal = 0M;

            foreach (var parcel in parcels)
            {
                var parcelCost = GetParcelCost(parcel);
                parcelCosts.Add(parcelCost);
                runningTotal += parcelCost.SubTotal;
            }

            var shippingRequest = new ShippingRequest()
            {
                Parcels = parcelCosts,
                SpeedyShipping = speedyShipping,
                TotalCost = speedyShipping ? runningTotal * speedyShippingMultiplier : runningTotal
            };

            shippingRequest.PriceBreakdown = GetPriceBreakdown(shippingRequest);

            return shippingRequest;
        }

        #region Private Methods
        private ParcelCost GetParcelCost(Parcel parcel)
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

        private List<string> GetPriceBreakdown(ShippingRequest shippingRequest)
        {
            var priceBreakdown = new List<string>();

            foreach (var parcel in shippingRequest.Parcels)
            {
                priceBreakdown.Add($"{parcel.ParcelSize.GetDescription()}: ${parcel.SubTotal}.");
            }

            if (shippingRequest.SpeedyShipping)
            {
                priceBreakdown.Add($"Speedy Shipping: ${shippingRequest.TotalCost / speedyShippingMultiplier}.");
            }

            priceBreakdown.Add($"Total Cost: ${shippingRequest.TotalCost}.");

            return priceBreakdown;
        }
        #endregion
    }
}
