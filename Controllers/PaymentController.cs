using System.Collections.Generic;
using System.Configuration;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace store_front.Controllers
{
    public class PaymentController : Controller
    {
        /**
         * Display payment page
         *
         * (Shortcut PoC, at this point the user has a complete basket and has clicked "pay now")
         */
        public IActionResult Index()
        {

            var paymentIntent = CreatePaymentIntent();
            ViewBag.IntentSecret = paymentIntent.ClientSecret;

            return View();

        }

        /**
         * Create paymentIntent
         */
        private PaymentIntent CreatePaymentIntent()
        {
            var Metadata = new Dictionary<string, string>
            {
                {"order_id", "12345678"},
                {"customer_name", "John Doe"},
                {"customer_email", "jd@example.com"}
            };
            
            var options = new PaymentIntentCreateOptions
            {
                // Stripe works in pennies
                Amount = 10000,
                Currency = "gbp",
                
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                
                // Metadata can contain useful information such as order IDs, customer details etc.
                Metadata = Metadata,
                
                // The ReceiptEmail is used if you would like Stripe to automatically send Payment receipts
                // Email templates can be configured in Stripe
                ReceiptEmail = "jd@example.com"
            };
            
            var service = new PaymentIntentService();
            
            // Creates a PaymentIntent.
            // At this point the payment isn't confirmed (user has clicked "pay now" and is now required to enter card details).
            // The paymentIntentService's Create method returns https://stripe.com/docs/api/payment_intents/object
            // The PaymentIntent can be referred to at any time using its unique ID, overall it represents the state of a
            // transaction.
            // PaymentIntents can be confirmed, updated with new info (i.e. card failures, fraud etc.), and cancelled.
            var intent = service.Create(options);

            // We need the paymentIntent's clientSecret for the next step (attempting payment), so we return the object
            return intent;
        }

        /**
         * Confirm paymentIntent and redirect user
         */
        public IActionResult Confirmed()
        {
            return View();
        }
    }
}