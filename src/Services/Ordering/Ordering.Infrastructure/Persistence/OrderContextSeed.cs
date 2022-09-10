using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        // Method Creates database and seeds the data
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order() { AddressLine = "Kajiado North", CVV = "123", CardName = "MARTIN KARIUKI", CardNumber = "12343859430821",
                    Country = "Kenya", CreatedBy = "root", CreatedDate = DateTime.Now, EmailAddress = "karis.njoroge@gmail.com",
                    Expiration = "07/28", FirstName = "Martin", LastModifiedBy = "root", LastModifiedDate = DateTime.Now,
                    LastName = "Njoroge", PaymentMethod = 2, State = "Rift Valley",  TotalPrice = 350, UserName = "swn", ZipCode = "20303" }
            };
        }
    }
}
