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

        public decimal PostageCost;

        public decimal TotalCost => ItemCost + PostageCost;
    }
}
