using System.ComponentModel.DataAnnotations;
using FA_Courier_Kata.Domain.Helpers;
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
        public List<string> Post([FromBody] Parcel parcel, [FromQuery][Required] bool speedyShipping)
        {
            var parcelCost = _parcelService.GetParcelCost(parcel, speedyShipping);

            return $"{parcelCost.ParcelSize.GetDescription()}: ${parcelCost.PostageCost}. Total Cost: ${parcelCost.PostageCost}"; ;
        }
    }
}
