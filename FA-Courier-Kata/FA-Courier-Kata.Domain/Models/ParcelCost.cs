using FA_Courier_Kata.Domain.Models.Enums;

namespace FA_Courier_Kata.Domain.Models
{
    public class ParcelCost
    {
        public ParcelCost()
        {

        }

        public Parcel ParcelDetails;

        public ParcelSize ParcelSize;

        public decimal ItemCost;

        public decimal ExcessWeightCost;

        public decimal SubTotal => ItemCost + ExcessWeightCost;
    }
}
