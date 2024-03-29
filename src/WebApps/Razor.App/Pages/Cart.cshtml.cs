﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor.App.Models;
using Razor.App.Services;

namespace Razor.App;

public class CartModel : PageModel
{
    private readonly IBasketService _basketService;

    public CartModel(IBasketService basketService)
    {
        _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
    }

    public BasketModel Cart { get; set; } = new BasketModel();        

    public async Task<IActionResult> OnGetAsync()
    {
        var userName = "erick";
        Cart = await _basketService.GetBasket(userName);            

        return Page();
    }

    public async Task<IActionResult> OnPostRemoveToCartAsync(string productId)
    {
        var userName = "erick";
        var basket = await _basketService.GetBasket(userName);

        var item = basket.Items.Single(i => i.ProductId == productId);
        basket.Items.Remove(item);

        var updatedBasket = await _basketService.UpdateBasket(basket);
        Cart = updatedBasket;

        return RedirectToPage();
    }
}
