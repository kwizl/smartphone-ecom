using AspnetRunBasics.Interfaces;
using AspnetRunBasics.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _client;

        public BasketService(HttpClient client)
        {
            _client = client;
        }

        public Task CheckoutBasket(BasketCheckoutModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<BasketModel> GetBasket(string userName)
        {
            throw new System.NotImplementedException();
        }

        public Task<BasketModel> UpdateBasket(BasketModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}
