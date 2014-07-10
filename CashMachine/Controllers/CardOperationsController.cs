using System;
using System.Web.Mvc;
using CashMachine.Data;
using CashMachine.Models;

namespace CashMachine.Controllers
{
    /// <summary>
    /// Controller for Card Operations.
    /// </summary>
    public class CardOperationsController : ControllerBase
    {
        /// <summary>
        /// Entry Point for Card Operations.
        /// </summary>
        public ActionResult Index()
        {
            // Checking if Card exists and is still valid:
            Card card = base.DataProvider.Cards.FindCard(base.Session.GetCardNumber(), base.Session.GetPin());
            if (card == null)
                return base.ToCardError(StringResources.WrongCardNumber);
            if (!card.IsValid)
                return base.ToCardError(StringResources.CardIsBlocked);
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
                Balance = card.Balance.ToString(),
                Today = DateTime.Today.ToShortDateString(),
            };
            return View(balance);
        }

        /// <summary>
        /// Returns Cash from the Card to the happy User. Or maybe not.
        /// </summary>
        public ActionResult GetCash()
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
