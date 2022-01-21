﻿using FA_Courier_Kata.Domain.Models;
using FA_Courier_Kata.Domain.Models.Enums;
using FA_Courier_Kata.Domain.Services.Interfaces;

namespace FA_Courier_Kata.Domain.Services
{
    public class ParcelService : IParcelService
    {
        public ParcelService()
        {
        }

        public ParcelCost GetParcelCost(Parcel parcel, bool speedyShipping)
        {
            var parcelSize = GetParcelSize(parcel);
            var parcelCost = CalculateParcelPostage(parcelSize);

            return new ParcelCost
            {
                ParcelDetails = parcel,
                ParcelSize = parcelSize,
                ItemCost = parcelCost,
                PostageCost = speedyShipping ? parcelCost : 0
            };
        }

        private decimal CalculateParcelPostage(ParcelSize parcelSize)
        {
            return parcelSize switch
            {
                ParcelSize.Small => 3,
                ParcelSize.Medium => 8,
                ParcelSize.Large => 15,
                ParcelSize.ExtraLarge => 25,
                _ => 0,
            };
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

    }
}
