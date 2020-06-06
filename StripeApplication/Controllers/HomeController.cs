using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StripeApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Charge()
        {
            ViewBag.Message = "Learn how to process payments with Stripe";
            return View(new StripeChargeModel());
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Charge(StripeChargeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }



            var chargeId = await ProcessPayment(model);
            // You should do something with the chargeId --> Persist it maybe?

            return View("Index");
        }



        private static async Task<string> ProcessPayment(StripeChargeModel model)
        {
            return await Task.Run(() =>
            {
                var myCharge = new StripeChargeCreateOptions
                {
                    // convert the amount of £12.50 to pennies i.e. 1250
                    Amount = (int)(model.Amount * 100),
                    Currency = "gbp",
                    Description = "Description for test charge",
                    Source = new StripeSourceOptions
                    {
                        TokenId = model.Token
                    }
                };

                var chargeService = new StripeChargeService("your private key here");
                var stripeCharge = chargeService.Create(myCharge);

                return stripeCharge.Id;
            });
        }


    }
}