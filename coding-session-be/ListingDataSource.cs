namespace Persistence
{
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Data;
  using Models;
  
  public class ListingDataSource
  {
    public async IAsyncEnumerable<Listing> GetListingsAsync(int amountOfListings)
    {
      for (var i = 0; i < amountOfListings; i++)
      {
        // Simulate asynchronous operation (e.g., fetching data from a database)
        await Task.Delay(100);
        yield return ListingGenerator.GenerateListing(i);
      }
    }
  
    public IEnumerable<Listing> GetListings(int amountOfListings)
    {
      var listings = new List<Listing>();
      for (var i = 0; i < amountOfListings; i++)
      {
        listings.Add(ListingGenerator.GenerateListing(i));
      }
      return listings;
    }
  }
}