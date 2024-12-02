namespace Api
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Persistence;

    [ApiController]
    [Route("[controller]")]
    public class ListingController : ControllerBase
    {
        private readonly ILogger<ListingController> _logger;
        private readonly Random _random = new Random();
        private readonly ListingDataSource _listingDataSource;
    
        public ListingController(ILogger<ListingController> logger)
        {
            _logger = logger;
            _listingDataSource = new ListingDataSource();
        }
    
        [HttpGet]
        public IEnumerable<Listing> Get() {
            var listings = _listingDataSource.GetListings(50);
            return listings.OrderBy(OrderByRelevance); 
        }


        private record UserPreferences(float Rooms, string City, int PriceMin, int PriceMax, ListingType ListingType);
        readonly UserPreferences USER_PREFERENCE = new(4, "Lugano", 250000, 300000, ListingType.House);
        
        private float OrderByRelevance(Listing listing) {
            // Lugano, 4 rooms, house, 290000
            float factor = 0;

            factor += GetFactorFromMinMax(listing.Rooms, USER_PREFERENCE.Rooms,USER_PREFERENCE.Rooms);
            factor += GetFactorFromMinMax(listing.Price, USER_PREFERENCE.PriceMin,USER_PREFERENCE.PriceMax);
            factor += (USER_PREFERENCE.City == listing.City)? 0: 100;
            factor += (USER_PREFERENCE.ListingType == listing.ListingType)? 0: 100;

            return factor;
        }

        private float GetFactorFromMinMax(float candidate, float min, float max) {
            if(min > candidate) {
                return candidate / min * 100;
            }
            else if (candidate > max) {
                return 100;
            }
            return 0f;
        }
    }
}