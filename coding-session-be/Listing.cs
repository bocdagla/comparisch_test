namespace Models
{
    public enum ListingType
    {
        Flat, House
    }

    public enum DealType
    {
        Rent, Buy
    }

    public enum CoordinateAccuracy
    {
        Exact, Street, City, State, Country
    }

    public record Listing(
        ListingType ListingType,
        DealType DealType,
        string Title,
        int Price,
        float Rooms,
        string City,
        int Zipcode,
        Coordinates Coordinates
    );

    public record Coordinates(
        double Latitude,
        double Longitude,
        CoordinateAccuracy Accuracy
    );
}