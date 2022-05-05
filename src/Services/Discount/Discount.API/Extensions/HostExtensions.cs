using Npgsql;

namespace Discount.API.Extensions;
public static class HostExtensions
{
    public static IHost MigrateDatabase<TContext>(this IHost host, int retry = 0)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var configuration = services.GetRequiredService<IConfiguration>();
        var logger = services.GetRequiredService<ILogger<TContext>>();

        try
        {
            logger.LogInformation("Migration postgresql database.");

            using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            connection.Open();

            using var command = connection.CreateCommand();

            command.CommandText = "DROP TABLE IF EXISTS coupon";
            command.ExecuteNonQuery();

            command.CommandText = "CREATE TABLE coupon(id SERIAL PRIMARY KEY, product_name VARCHAR(24) NOT NULL, description TEXT, amount INT)";
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO Coupon(product_name, description, amount) VALUES('IPhone X', 'IPhone Discount', 150);";
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO Coupon(product_name, description, amount) VALUES('Samsung 10', 'Samsung Discount', 100);";
            command.ExecuteNonQuery();

            logger.LogInformation("Migrated postresql database.");
        }
        catch (NpgsqlException ex)
        {
            logger.LogError(ex, "An error occurred while migrating the postresql database");

            if (retry < 50)
            {
                retry++;
                Thread.Sleep(2000);
                MigrateDatabase<TContext>(host, retry);
            }
        }

        return host;
    }
}
