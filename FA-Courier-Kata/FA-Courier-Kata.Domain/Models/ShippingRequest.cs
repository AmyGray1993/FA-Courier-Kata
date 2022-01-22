using System.Collections.Generic;

namespace FA_Courier_Kata.Domain.Models
{
    public class ShippingRequest
    {
        public ShippingRequest()
        {

        }

        public List<ParcelCost> Parcels = new List<ParcelCost>();

        public bool SpeedyShipping;

        public decimal TotalCost;
    }
}
