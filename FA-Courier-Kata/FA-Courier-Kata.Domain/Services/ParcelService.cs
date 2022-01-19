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
            var width = parcel.Width;
            var height = parcel.Height;
            var depth = parcel.Depth;

            if (width < 10 && height < 10 && depth < 10)
            {
                return 3;
            }
            else if (width < 50 && height < 50 && depth < 50)
            {
                return 8;
            }
            else if (width < 100 && height < 100 && depth < 100)
            {
                return 15;
            }
            else
            {
                return 25;
            }
        }
    }
}
