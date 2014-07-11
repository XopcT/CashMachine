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
                Balance = card.Balance,
            };
            return View(balance);
        }

        /// <summary>
        /// Page to get Cash.
        /// </summary>
        public ActionResult GetCash()
        {
            // Checking if Card exists and is valid:
            Card card = base.DataProvider.Cards.FindCard(base.Session.GetCardNumber(), base.Session.GetPin());
            if (card == null)
                return base.ToCardError(StringResources.WrongCardNumber);
            if (!card.IsValid)
                return base.ToCardError(StringResources.CardIsBlocked);
            
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
            if(card.Balance > request.Amount)
                card.Balance -= request.Amount;
            base.DataProvider.Cards.Save(card);
            return base.ToCardOperations();
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
