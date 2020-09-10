using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace store_front.Controllers
{
    public class SubscriptionController : Controller
    {
        /**
         * Page with "Subscribe Now" button
         *
         * (Shortcut PoC, at this point the user has already been created as a customer and has confirmed their payment method)
         */
        [Route("Subscription/Index")]
        public IActionResult Index()
        {
            return View();
        }
        
        /**
         * Create a recurring subscription for the selected product item.
         *
         * "price_1HPqA0FnOG8jwOnxGyiUJqxv" - £100/monthly for "Test Product"
         *
         * Typically a subscription is created AFTER the customer has entered their payment method details
         * and has been charged for the initial payment, so:
         *
         * Checkout -> PaymentIntent Created -> PaymentIntent Confirmed -> Subscription Created -> Card billed at selected interval.
         *
         * Subscriptions can also be scheduled to start at a later date and can be managed fully from within the Stripe dashboard.
         */
        [HttpPost]
        [Route("Subscription/Create")]
        public IActionResult Create()
        {
            // var customer = Util.CreateCustomer();
            // Util.ChargeCustomerAndRecordPaymentMethod(customer);
            //
            // var product = Util.CreateProductForSubscription();
            //
            
            var options = new SubscriptionCreateOptions
            {
                // "here's one I made earlier"
                Customer = "cus_HzuApdn0CYYoT4",
                Items = new List<SubscriptionItemOptions>
                {
                    new SubscriptionItemOptions
                    {
                        // "here's one I made earlier"
                        Price = "price_1HPuN1FnOG8jwOnxcAEuadHe",
                    }
                }
            };
            
            var service = new SubscriptionService();
            service.Create(options);

            return RedirectToAction("Confirmed", "Payment");
        }
    }
}