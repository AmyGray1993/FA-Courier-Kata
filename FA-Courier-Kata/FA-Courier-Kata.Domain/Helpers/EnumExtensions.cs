using System;
using System.ComponentModel;
using System.Linq;

namespace FA_Courier_Kata.Domain.Helpers
{
    public static partial class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;

            return attr.Description;
        }
    }
}
