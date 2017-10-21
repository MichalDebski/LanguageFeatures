using LanguageFeatures.Models;
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

            return View("Result",  (object)string.Format("Nazwa produktu to: {0}",productName));
        }
    }
}