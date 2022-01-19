using FA_Courier_Kata.Domain.Models;
using FA_Courier_Kata.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FA_Courier_Kata.Domain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelController : Controller
    {
        private readonly IParcelService _parcelService;

        public ParcelController(IParcelService parcelService)
        {
            _parcelService = parcelService;
        }

        [HttpPost]
        public decimal Post([FromBody] Parcel parcel)
        {
            return _parcelService.CalculateParcelPostage(parcel);
        }
    }
}
