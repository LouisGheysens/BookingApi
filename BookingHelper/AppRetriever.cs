using Microsoft.Extensions.Configuration;

namespace BookingHelper;
public static class AppRetriever
{
    public static string GetConnectionString()
    {
        var config = BuildAppSettings();

        var connectionString = config.GetConnectionString("BookingDb");

        return string.IsNullOrWhiteSpace(connectionString) ? throw new Exception("Failed to load the connection string") : connectionString;
    }

    public static string GetJwtKey()
    {
        var config = BuildAppSettings();

        var jwtKey = config.GetSection("Keys:JwtKey").Value;

        return string.IsNullOrWhiteSpace(jwtKey) ? throw new Exception("Failed to load the Json web token") : jwtKey;
    }

    private static IConfigurationRoot BuildAppSettings()
    {
        var configBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

        var config = configBuilder.Build();

        return config;
    }
}
