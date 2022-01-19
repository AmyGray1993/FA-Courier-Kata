using FA_Courier_Kata.Domain.Models;
using FA_Courier_Kata.Domain.Services.Interfaces;

namespace FA_Courier_Kata.Domain.Services
{
    public class ParcelService : IParcelService
    {
        public ParcelService()
        {
        }

        public decimal CalculateParcelPostage(Parcel parcel)
        {
            return 0;
        }
    }
}
