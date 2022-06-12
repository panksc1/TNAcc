using System.Text.Json;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class AccContextSeed
    {
        public static async Task SeedAsync(AccContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Entities.Any())
                {
                    var entitiesData = File.ReadAllText("../Infrastructure/Data/SeedData/entities.json");

                    var entities = JsonSerializer.Deserialize<List<Entity>>(entitiesData);

                    foreach (var item in entities)
                    {
                        context.Entities.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Venues.Any())
                {
                    var venuesData = File.ReadAllText("../Infrastructure/Data/SeedData/venues.json");

                    var venues = JsonSerializer.Deserialize<List<Venue>>(venuesData);

                    foreach (var item in venues)
                    {
                        context.Venues.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Gigs.Any())
                {
                    var gigsData = File.ReadAllText("../Infrastructure/Data/SeedData/gigs.json");

                    var gigs = JsonSerializer.Deserialize<List<Gig>>(gigsData);

                    foreach (var item in gigs)
                    {
                        context.Gigs.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<AccContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}