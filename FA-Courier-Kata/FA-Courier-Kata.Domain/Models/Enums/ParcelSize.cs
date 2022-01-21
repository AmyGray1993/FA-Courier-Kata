using System.ComponentModel;

namespace FA_Courier_Kata.Domain.Models.Enums
{
    public enum ParcelSize
    {
        [Description("Small Parcel")]
        Small,

        [Description("Medium Parcel")]
        Medium,

        [Description("Large Parcel")]
        Large,

        [Description("Extra Large Parcel")]
        ExtraLarge
    }
}
