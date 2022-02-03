namespace Discount.API.Entities
{
    public class Coupon
    {
        public int ID { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }
        
        public int Amount { get; set; }
    }
}