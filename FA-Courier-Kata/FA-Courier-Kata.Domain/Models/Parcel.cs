namespace FA_Courier_Kata.Domain.Models
{
    public class Parcel
    {
        public Parcel()
        {

        }

        public decimal Width { get; set; }

        public decimal Height { get; set; }

        public decimal Depth { get; set; }

        public decimal WeightKg { get; set; }
    }
}
