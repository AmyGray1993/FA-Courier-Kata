using FA_Courier_Kata.Domain.Models;

namespace FA_Courier_Kata.Domain.Services.Interfaces
{
    public interface IParcelService
    {
        decimal CalculateParcelPostage(Parcel parcel);
    }
}
