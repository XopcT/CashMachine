using System.Web.Mvc;
using CashMachine.Data;
using CashMachine.Models;

namespace CashMachine.Controllers
{
    /// <summary>
    /// Controller for entry Page with Card Number Request.
    /// </summary>
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// Entry Point for Card Operations.
        /// </summary>
        public ActionResult Index()
        {
            string cardNumber = base.Session.GetCardNumber();
            string pin = base.Session.GetPin();
            // Checking for Card Permission:
            if (string.IsNullOrEmpty(base.Session.GetCardNumber()))
                return base.ToNumberRequest();
            if (string.IsNullOrEmpty(base.Session.GetPin()))
                return base.ToPinRequest();
            return View();
        }

        /// <summary>
        /// Checks Card Balance.
        /// </summary>
        public ActionResult CheckBalance()
        {
            // Checking if Card exists and is still valid:
            Card card = base.DataProvider.Cards.FindCard(base.Session.GetCardNumber(), base.Session.GetPin());
            if (card == null)
                return base.ToCardError(StringResources.WrongCardNumber);
            if (!card.IsValid)
                return base.ToCardError(StringResources.CardIsBlocked);
            CardBalance balance = new CardBalance()
            {
                CardNumber = card.Number,
                Balance = base.DataProvider.Cards.GetBalance(card.Number),
            };
            return View(balance);
        }

        /// <summary>
        /// Page to get Cash.
        /// </summary>
        public ActionResult GetCash()
        {
            return View(new CashAmountRequest());
        }

        /// <summary>
        /// Returns Cash from the Card to the happy User. Or maybe not.
        /// </summary>
        [HttpPost()]
        public ActionResult GetCash(CashAmountRequest request)
        {
            Card card = base.DataProvider.Cards.FindCard(base.Session.GetCardNumber(), base.Session.GetPin());
            if (card == null)
                return base.ToCardError(StringResources.WrongCardNumber);
            if (!card.IsValid)
                return base.ToCardError(StringResources.CardIsBlocked);
            if (card.Balance >= request.Amount)
            {
                base.DataProvider.Cards.GetCash(card.Number, request.Amount);
                return View("Success");
            }
            else
            {
                return View("Fail");
            }
        }

        /// <summary>
        /// Shows the Operation Success Page.
        /// </summary>
        public ActionResult Success()
        {
            return View();
        }

        /// <summary>
        /// Shows the Operation Failure Page.
        /// </summary>
        public ActionResult Fail()
        {
            return View();
        }

        /// <summary>
        /// Exits Card Operations.
        /// </summary>
        public ActionResult Exit()
        {
            base.Session.SetCardNumber(string.Empty);
            base.Session.SetPin(string.Empty);
            return base.ToNumberRequest();
        }
    }
}
