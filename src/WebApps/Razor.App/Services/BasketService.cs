﻿using Razon.App.Extensions;
using Razor.App.Models;

namespace Razor.App.Services;

public class BasketService : IBasketService
{
    private readonly HttpClient _httpClient;

    public BasketService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task CheckoutBasket(BasketCheckoutModel model)
    {
        var response = await _httpClient.PostAsJson("/Basket/Checkout", model);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Something went wrong when calling api.");
        }
    }

    public async Task<BasketModel> GetBasket(string userName)
    {
        var response = await _httpClient.GetAsync($"/Basket/{userName}");
        return await response.ReadContentAs<BasketModel>();
    }

    public async Task<BasketModel> UpdateBasket(BasketModel model)
    {
        var response = await _httpClient.PostAsJson("/Basket", model);

        if (response.IsSuccessStatusCode)
        {
            return await response.ReadContentAs<BasketModel>();
        }
        else
        {
            throw new Exception("Something went wrong when calling api.");
        }
    }
}
