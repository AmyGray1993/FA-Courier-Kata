# FA-Courier-Kata
 
Calculate the shipping price for parcels based on their dimensions and weight. If speedy shipping is applied, the total cost should double.

### Parcel Types
There are 5 types of parcel: 
- Small (all dimensions < 10cm)
- Medium (all dimensions < 50cm)
- Large (all dimensions < 100cm)
- XL (any dimension >= 100cm)
- Heavy (any size, max 50kg)


### Standard shipping costs
The standard shipping costs (without speedy shipping or excess weight charges) per parcel type are as follows:
- Small: $3
- Medium: $8
- Large: $15
- XL: $25

### Excess weight charges
An additional charge will be applied if the parcel exceeds the maximum weight limit for its given size category. 
The maximum weight limit, along with their excess weight charges, per parcel are as follows:
- Small: 1kg (+$2/kg over)
- Medium: 3kg (+$2/kg over)
- Large: 6kg (+$2/kg over)
- XL: 10kg (+$2/kg over)
- Heavy: 50kg (+$1/kg over)

### Speedy shipping
Speedy shipping can be applied against any order. Speedy shipping will double the price of the whole order and will be calculated after any discounts have been applied.

### Savings
We should attempt to calculate the cheapest option for the user, if for example they have entered dimensions that are categorized as an XL parcel, but additional charges for excess weight mean it would be cheaper to categorise the parcel as Heavy then that is what should be returned.

See the example below. Based on dimensions alone, the parcel matches the criteria of an XL parcel. However, it would be cheaper for the customer if it were categorised as a Heavy parcel:

      var parcel = new Parcel
      {
          Width = 100,
          Height = 50.9M,
          Depth = 50.9M,
          WeightKg = 25
      };

      var xlParcelCost = new ParcelCost
      {
          ParcelDetails = parcel,
          ParcelSize = ParcelSize.ExtraLarge,
          ItemCost = 25,
          ExcessWeightCost = 30, // 15 * 2
          SpeedyShipping = false
          // TotalCost = ItemCost + ExcessWeightCost = 55
      };

      var heavyParcelCost = new ParcelCost
      {
          ParcelDetails = parcel,
          ParcelSize = ParcelSize.Heavy,
          ItemCost = 50,
          ExcessWeightCost = 0,
          SpeedyShipping = false
          // TotalCost = ItemCost + ExcessWeightCost = 50
      };

