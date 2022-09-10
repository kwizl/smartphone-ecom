using System.Collections.Generic;

namespace Shopping.Aggregator.Models
{
    public class ShoppingModel
    {
        public string UserName { get; set; }
        
        public BasketModel BasketModel { get; set; }
        
        public IEnumerable<OrderResponseModel> Orders { get; set; }
    }
}
