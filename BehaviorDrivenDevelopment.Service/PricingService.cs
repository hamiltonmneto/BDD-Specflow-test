

using System.Collections.Generic;
using System.Linq;
using static BehaviorDrivenDevelopment.Service.PricingService;

namespace BehaviorDrivenDevelopment.Service
{
    public interface IPricingService
    {
        decimal GetBasketTotalAmount(Basket basket);
    }
    public class PricingService : IPricingService
    {
        public decimal GetBasketTotalAmount(Basket basket)
        {
            if (!basket.Products.Any())
            {
                return 0;
            }

            var basketValue = basket.Products.Sum(x => x.Price);

            if (basket.User.IsLoggedIn)
            {
                return basketValue - (basketValue * 0.05m);
            }

            return basketValue;
        }

        public class Basket
        {
            public User User { get; set; }
            public List<Product> Products { get; } = new List<Product>();
        }

        public class Product
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
        }

        public class User
        {
            public bool IsLoggedIn { get; set; }
        }
    }
}
