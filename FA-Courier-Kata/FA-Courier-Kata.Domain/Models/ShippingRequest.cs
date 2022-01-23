using System.Collections.Generic;

namespace FA_Courier_Kata.Domain.Models
{
    public class ShippingRequest
    {
        public ShippingRequest()
        {

        }

        public List<ParcelCost> Parcels = new List<ParcelCost>();

        public List<string> PriceBreakdown = new List<string>();

        public bool SpeedyShipping;

        public decimal TotalCost;
    }
}
