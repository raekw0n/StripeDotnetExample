using System;
using Stripe;

namespace store_front
{
    public static class Util
    {
        public static Customer CreateCustomer()
        {
            var options = new CustomerCreateOptions
            {
                Email = CreateEmailAddress()
            };

            var service = new CustomerService();
            var customer = service.Create(options);

            return customer;
        }

        public static string CreateEmailAddress()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            
            var stringChars = new char[8];
            var random = new Random();

            for (var i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            
            var randomString = new string(stringChars);

            return "user@" + randomString + ".uk";
        }
    }
}