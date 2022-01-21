using System.Collections.Generic;
using FA_Courier_Kata.Domain.Models;

namespace FA_Courier_Kata.Domain.Services.Interfaces
{
    public interface IParcelService
    {
        List<string> GetPostageInvoice(ParcelCost parcelCost, bool speedyShipping);

        ParcelCost GetParcelCost(Parcel parcel, bool speedyShipping);
    }
}
