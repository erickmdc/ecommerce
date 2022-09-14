using Dapper;
using Discount.Grpc.Entities;
using Npgsql;

namespace Discount.Grpc.Repositories;
public class DiscountRepository : IDiscountRepository
{
    private readonly IConfiguration _configuration;

    public DiscountRepository(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<Coupon> GetDiscount(string productName)
    {
        using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        var queryParams = new { ProductName = productName };

        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("SELECT id AS Id, product_name AS ProductName, description AS Description, amount AS Amount FROM Coupon WHERE product_name = @ProductName", queryParams);

        if (coupon == null)
            return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };

        return coupon;
    }

    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        var queryParams = new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount };

        var affected = await connection.ExecuteAsync("INSERT INTO Coupon (product_name, description, amount) VALUES (@ProductName, @Description, @Amount)", queryParams);

        return affected != 0;
    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        var queryParams = new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id };

        var affected = await connection.ExecuteAsync("UPDATE Coupon SET product_name=@ProductName, description = @Description, amount = @Amount WHERE Id = @Id", queryParams);

        return affected != 0;
    }

    public async Task<bool> DeleteDiscount(string productName)
    {
        using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        var queryParams = new { ProductName = productName };

        var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE product_name = @ProductName", queryParams);

        return affected != 0;
    }
}
