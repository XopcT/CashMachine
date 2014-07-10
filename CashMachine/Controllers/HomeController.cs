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
        /// Entry Point.
        /// </summary>
        public ActionResult Index()
        {
            return View(new CardSearchRequest());
        }

        /// <summary>
        /// Looks for the specified Card.
        /// </summary>
        /// <param name="request">Card Request.</param>
        [HttpPost()]
        public ActionResult FindCard(CardSearchRequest request)
        {
            // Looking for a valid Card with specified Number:
            Card card = base.DataProvider.Cards.FindCard(request.Number);
            if (card == null)
                return base.ToCardError(StringResources.WrongCardNumber);
            else if (!card.IsValid)
                return base.ToCardError(StringResources.CardIsBlocked);
            // Storing Card Number and redirecting to Pin Request Page:
            base.Session.SetCardNumber(request.Number);
            return base.ToPinRequest();
        }
        
        /// <summary>
        /// Shows Card Error Message.
        /// </summary>
        /// <param name="error">Error Message.</param>
        public ActionResult CardError(CardError error)
        {
            return View(error);
        }
    }
}
