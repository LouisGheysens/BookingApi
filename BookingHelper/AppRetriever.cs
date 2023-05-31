using Microsoft.Extensions.Configuration;

namespace BookingHelper;
public static class AppRetriever
{
    public static string GetConnectionString()
    {
        var configBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

        var config = configBuilder.Build();

        var connectionString = config.GetConnectionString("BookingDb");

        return string.IsNullOrWhiteSpace(connectionString) ? throw new Exception("Failed to load the connection string") : connectionString;
    }
}
