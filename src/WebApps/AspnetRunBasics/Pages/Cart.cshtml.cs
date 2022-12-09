using System;
using System.Linq;
using System.Threading.Tasks;
using AspnetRunBasics.Interfaces;
using AspnetRunBasics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics
{
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
            var userName = "swn";
            Cart = await _basketService.GetBasket(userName);            

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(string ProductID)
        {
            var userName = "swn";
            var basket = await _basketService.GetBasket(userName);

            var item = basket.Items.Single(x => x.ProductID == ProductID);
            basket.Items.Remove(item);

            await _basketService.UpdateBasket(basket);

            return RedirectToPage();
        }
    }
}