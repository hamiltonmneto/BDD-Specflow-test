using BehaviorDrivenDevelopment.Service;
using FluentAssertions;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using static BehaviorDrivenDevelopment.Service.PricingService;

namespace BehaviorDrivenDevelopment.Tests.Behavior.Steps
{
    [Binding]
    public class LoggedInUsersHasDiscountSteps
    {
        private User _user;
        private Basket _basket;
        private readonly IPricingService _pricingService = new PricingService();

        [Given(@"a user that is not logged in")]
        public void GivenAUserThatIsNotLoggedIn()
        {
            _user = new User { IsLoggedIn = false };
        }

        [Given(@"an empty basket")]
        public void GivenAnEmptyBasket()
        {
            _basket = new Basket { 
                User = _user
            };
        }
        
        [Given(@"a user that is logged in")]
        public void GivenAUserThatIsLoggedIn()
        {
            _user = new User { IsLoggedIn = true };

        }

        [When(@"a (.*) that costs (.*)GBP is added tp the basket")]
        public void WhenAT_ShirtThatCostsGBPIsAddedTpTheBasket(string itemName, decimal price)
        {
            _basket.Products.Add(new Product {
                Name = itemName,
                Price = price
            });
            
        }
       
        
        [Then(@"the basket value is (.*) GBP")]
        public void ThenTheBasketValueIsGBP(decimal expectedBasketValue)
        {
            var basketValue = _pricingService.GetBasketTotalAmount(_basket);
            basketValue.Should().Be(expectedBasketValue);
        }
    }
}
