namespace Data
{
    using Models;
    
    public class ListingGenerator
    {
        private static readonly Random _random = new Random();
    
        private static readonly List<LocationData> _cities = new List<LocationData>
        {
            new LocationData(8000, "Zurich", 47.3667, 47.3767, 8.5167, 8.5267), // 8000 Zurich
            new LocationData(8003, "Zurich", 47.3767, 47.3867, 8.5267, 8.5367), // 8003 Zurich
            new LocationData(8004, "Zurich", 47.3867, 47.3967, 8.5367, 8.5467), // 8004 Zurich
            new LocationData(8005, "Zurich", 47.3967, 47.4067, 8.5467, 8.5567), // 8005 Zurich
            new LocationData(8037, "Zurich", 47.3867, 47.3967, 8.5167, 8.5267), // 8037 Zurich
            new LocationData(8038, "Zurich", 47.3967, 47.4067, 8.5267, 8.5367), // 8038 Zurich
            new LocationData(7000, "Chur", 46.8533, 46.8633, 9.5233, 9.5333), // Chur
            new LocationData(4000, "Basel", 47.5584, 47.5684, 7.5884, 7.5984), // Basel
            new LocationData(3000, "Bern", 46.9480, 46.9580, 7.4380, 7.4480), // Bern
            new LocationData(1000, "Lausanne", 46.5200, 46.5300, 6.6300, 6.6400), // Lausanne
            new LocationData(1200, "Geneva", 46.2000, 46.2100, 6.1000, 6.1100), // Geneva
            new LocationData(6000, "Luzern", 47.0500, 47.0600, 8.3000, 8.3100), // Luzern
            new LocationData(8000, "St. Gallen", 47.4200, 47.4300, 9.3000, 9.3100), // St. Gallen
            new LocationData(6900, "Lugano", 46.0000, 46.0100, 8.9500, 8.9600), // Lugano
        };
    
        public static Listing GenerateListing(int index)
        {
            ListingType listingType = _random.NextSingle() < 0.8 ? ListingType.Flat : ListingType.House;
            DealType dealType = _random.NextSingle() < 0.5 ? DealType.Buy : DealType.Rent;
            string title = "Title " + index;
            int basePrice = _random.Next(8, 60) * 100;
            int price = dealType == DealType.Buy ? basePrice * 100 : basePrice;
            float rooms = _random.Next(2, 12) / 2.0f;
    
            var randomCity = _cities[_random.Next(_cities.Count)];
            int zipcode = randomCity.ZipCode;
            string city = randomCity.City;
            // Generate random coordinates for Switzerland
            double latitude = _random.NextDouble() * (randomCity.MaxLatitude - randomCity.MinLatitude) + randomCity.MinLatitude; // Latitude range for Switzerland
            double longitude = _random.NextDouble() * (randomCity.MaxLongitude - randomCity.MinLongitude) + randomCity.MinLongitude; // Longitude range for Switzerland
            CoordinateAccuracy accuracy = (CoordinateAccuracy)_random.Next(5);
            var coord = new Coordinates(latitude, longitude, accuracy);
    
            return new Listing(listingType, dealType, title, price, rooms, city, zipcode, coord);
        }
    }

    internal record LocationData(
        int ZipCode,
        string City,
        double MinLatitude,
        double MaxLatitude,
        double MinLongitude,
        double MaxLongitude
    );
}