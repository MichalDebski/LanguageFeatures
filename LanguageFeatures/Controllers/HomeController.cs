using LanguageFeatures.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Przejście do adresu URL pokazującego przykład";
        }

        public ViewResult AutoProperty()
        {
            var myProduct = new Product();

            myProduct.Name = "Kajak";

            string productName = myProduct.Name;

            return View("Result", (object)string.Format("Nazwa produktu: {0}", productName));
        }

        public ViewResult CreateCollection()
        {
            var cart = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product {Name="Kajak",Price=275M},
                    new Product {Name="Kamizelka ratunkowa",Price=48.95M},
                    new Product {Name="Piłka nożna",Price=19.50M},
                    new Product {Name="Flaga narożna",Price=34.95M}
                }
            };

            decimal cartTotal = cart.TotalPrices();

            return View("Result",
                (object)string.Format("Razem: {0:c}", cartTotal));
        }

        public ViewResult UseExtensionEnumerable()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product {Name = "Kajak", Category = "Sporty wodne", Price = 275M},
                    new Product {Name = "Kamizelka ratunkowa", Category = "Sporty wodne", Price = 48.95M},
                    new Product {Name = "Piłka nożna", Category = "Piłka nożna", Price = 19.50M},
                    new Product {Name = "Flaga narożna", Category = "Piłka nożna", Price = 34.95M}
                }
            };

            decimal total = 0;

            foreach (var prod in products.Filter(p => p.Category == "Piłka nożna"))
            {
                total += prod.Price;
            }

            return View("Result", (object)string.Format("Razem: {0}", total));
        }

        public ViewResult CreateAnonArray()
        {
            var oddsAndEnds = new[]
            {
                new { Name = "MVC", Category = "Wzorzec"},
                new { Name = "Kapelusz", Category = "Odzież"},
                new { Name = "Jabłko", Category = "Owoc"}
            };

            StringBuilder sb = new StringBuilder();

            foreach (var item in oddsAndEnds)
            {
                sb.Append(item.Name).Append(" ");
            }

            return View("Result", (object)sb.ToString());
        }

        public ViewResult FindProducts()
        {
            Product[] products = {
                    new Product {Name = "Kajak", Category = "Sporty wodne", Price = 275M},
                    new Product {Name = "Kamizelka ratunkowa", Category = "Sporty wodne", Price = 48.95M},
                    new Product {Name = "Piłka nożna", Category = "Piłka nożna", Price = 19.50M},
                    new Product {Name = "Flaga narożna", Category = "Piłka nożna", Price = 34.95M}
                };

            //var foundProducts = from match in products
            //                    orderby match.Price descending
            //                    select new { match.Name, match.Price };

            var foundProducts = products
                .OrderByDescending(p => p.Price)
                .Take(3)
                .Select(p => new { p.Name, p.Price })
                .Reverse();

            int count = 0;
            StringBuilder result = new StringBuilder();
            foreach (var p in foundProducts)
            {
                result.AppendFormat("Cena: {0} ", p.Price);
                if (++count == 3) { break; }
            }

            return View("Result", (object)result.ToString());
        }
    }
}