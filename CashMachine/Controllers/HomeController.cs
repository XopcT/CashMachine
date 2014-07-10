using CashMachine.Data;
using CashMachine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CashMachine.Controllers
{
    /// <summary>
    /// Entry Page Controller.
    /// </summary>
    public class HomeController : ControllerBase
    {

        /// <summary>
        /// Entry Point.
        /// </summary>
        public ActionResult Index()
        {
            return View(new CardSearchRequest());
        }

        [HttpPost()]
        public ActionResult FindCard(CardSearchRequest request)
        {
            Card card = base.DataProvider.Cards.FindCard(request.Number);
            if (card == null)
                return View("CardError", new CardError() { ErrorMessage = "Card was not found" });
            else if (!card.IsValid)
                return View("CardError", new CardError() { ErrorMessage = "Sorry, your Card is blocked. Call your Bank's Support or try to find another one. Note, we've already called Police, just in case. Please stay near the Cash Machine." });
            return View("PinCode", new PinRequest());
        }
    }
}
