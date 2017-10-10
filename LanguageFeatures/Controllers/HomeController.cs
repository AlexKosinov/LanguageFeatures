using LanguageFeatures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public string Index()
        {
            return "Navigate to a URL to show an example";
        }

        public ViewResult AutoProperty()
        {
            Product myProduct = new Product();
            myProduct.Name = "Kayak";
            string productName = myProduct.Name;
            return View("Result", (object)String.Format("Product name: {0}", productName));
        }

        public ViewResult CreateProduct()
        {
            Product myProduct = new Product
            {
                ProductID = 100,
                Name = "Kayak",
                Description = "A boat for one person",
                Price = 275M,
                Category = "Watersports"
            };
            return View("Result", (object)String.Format("Category: {0}", myProduct.Category));
        }

        public ViewResult CreateCollection()
        {
            string[] stringArray = { "apple", "orange", "plum" };
            List<int> intList = new List<int> { 10, 20, 30, 40 };
            Dictionary<string, int> myDick = new Dictionary<string, int>
            {
                { "apple", 10 }, { "orange", 20 }, { "plum", 30 }
            };
            return View("Result", (object)stringArray[1]);
        }

        public ViewResult UseExtension()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product {Name = "Soccer boll", Price = 19.50M },
                    new Product {Name = "Kayak", Price = 275M },
                    new Product {Name = "Lifejacket", Price = 48.95M },
                    new Product {Name = "Corner flag", Price = 14.95M }
                }
            };

            Product[] productArray =
            {
                new Product {Name = "Soccer boll", Price = 19.50M },
                new Product {Name = "Kayak", Price = 275M },
                new Product {Name = "Lifejacket", Price = 48.95M },
                new Product {Name = "Corner flag", Price = 14.95M }
            };

            decimal cartTotal = products.TotalPrice();
            decimal arrayTotal = productArray.TotalPrice();
            return View("Result", (object)String.Format("Cart total: {0}, array total: {1}", cartTotal, arrayTotal));
        }

        public ViewResult UseFilterExtensionMethod()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product {Name = "Soccer boll", Category = "Soccer", Price = 19.50M },
                    new Product {Name = "Kayak", Category = "Watersports", Price = 275M },
                    new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M },
                    new Product {Name = "Corner flag", Category = "Soccer", Price = 14.95M }
                }
            };

            Func<Product, bool> categoryFilter = prod => prod.Category == "Soccer";

            decimal total = 0;
            foreach (Product prod in products.Filter(categoryFilter))
            {
                total += prod.Price;
            }

            return View("Result", (object)String.Format("Total: {0}", total));
        }

        public ViewResult CreateAnonArray()
        {
            var oddsAndEnds = new[]
            {
                new {Name = "MVC", Category = "Pattern"},
                new {Name = "Hat", Category = "Clothing"},
                new {Name = "Apple", Category = "Fruit"}
            };
            StringBuilder result = new StringBuilder();
            foreach (var item in oddsAndEnds)
            {
                result.Append(item.Name).Append(" ");
            }
            return View("Result", (object)result.ToString());
        }

        public ViewResult FindProduct()
        {
            Product[] products = {
                new Product {Name = "Soccer boll", Category = "Soccer", Price = 19.50M },
                new Product {Name = "Kayak", Category = "Watersports", Price = 275M },
                new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M },
                new Product {Name = "Corner flag", Category = "Soccer", Price = 14.95M }
            };

            /*var foundProducts = from match in products
                                orderby match.Price descending
                                select new
                                (
                                    match.Name,
                                    match.Price
                                );*/

            var foundProducts = products.OrderByDescending(e => e.Price)
                .Take(3)
                .Select(e => new
                {
                    e.Name,
                    e.Price
                });

            //int count = 0;
            StringBuilder result = new StringBuilder();
            foreach (var p in foundProducts)
            {
                result.AppendFormat("Price: {0} ", p.Price);
                /*if(++count == 3)
                {
                    break;
                }*/
            }

            return View("Result", (object)result.ToString());
        }

        public ViewResult SumProduct()
        {
            Product[] products = {
                new Product {Name = "Soccer boll", Category = "Soccer", Price = 19.50M },
                new Product {Name = "Kayak", Category = "Watersports", Price = 275M },
                new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M },
                new Product {Name = "Corner flag", Category = "Soccer", Price = 14.95M }
            };

            var result = products.Sum(e => e.Price);

            products[2] = new Product { Name = "Stadium", Price = 79500M };

            return View("Result", (object)String.Format("Sum: {0:c}", result));
        }
    }
}