using System.Collections.Generic;
using FA_Courier_Kata.Domain.Models;

namespace FA_Courier_Kata.Domain.Services.Interfaces
{
    public interface IParcelService
    {
        ShippingRequest ProcessShippingRequest(List<Parcel> parcels, bool speedyShipping);
    }
}
