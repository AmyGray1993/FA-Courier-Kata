using FA_Courier_Kata.Domain.Models.Enums;

namespace FA_Courier_Kata.Domain.Models
{
    public class ParcelCost
    {
        public ParcelCost()
        {

        }

        public readonly int SpeedyShippingMultiplier = 2;

        public Parcel ParcelDetails;

        public ParcelSize ParcelSize;

        public bool SpeedyShipping;

        public decimal ItemCost;

        public decimal ExcessWeightCost;

        public decimal TotalCost => SpeedyShipping ? (ItemCost + ExcessWeightCost) * SpeedyShippingMultiplier : (ItemCost + ExcessWeightCost);
    }
}
