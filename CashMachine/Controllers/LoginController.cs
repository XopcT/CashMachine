using System.Web.Mvc;
using CashMachine.Data;
using CashMachine.Models;

namespace CashMachine.Controllers
{
    /// <summary>
    /// Handles Card Login Operations.
    /// </summary>
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// Shows the Card Number Request.
        /// </summary>
        public ActionResult NumberRequest()
        {
            return View(new CardSearchRequest());
        }

        /// <summary>
        /// Processes the Card Number Request.
        /// </summary>
        /// <param name="request">Request Parameters.</param>
        [HttpPost()]
        public ActionResult NumberRequest(CardSearchRequest request)
        {
            Card card = base.DataProvider.Cards.FindCard(request.Number);
            if(card == null)
                return base.ToCardError(StringResources.WrongCardNumber);
            if(!card.IsValid)
                return base.ToCardError(StringResources.CardIsBlocked);
            
            base.Session.SetCardNumber(request.Number);
            return base.ToPinRequest();
        }

        /// <summary>
        /// Shows the Pin Request.
        /// </summary>
        public ActionResult PinRequest()
        {
            if (base.DataProvider.Cards.FindCard(base.Session.GetCardNumber()) == null)
                return base.ToCardError(StringResources.WrongCardNumber);
            return View(new PinRequest());
        }

        /// <summary>
        /// Processes the Pin Request.
        /// </summary>
        /// <param name="request">Request Parameters.</param>
        [HttpPost()]
        public ActionResult PinRequest(PinRequest request)
        {
            string number = base.Session.GetCardNumber();
            string hashedPin = HashHelper.ComputeHash(number, request.Pin);

            // Checking if Card exists and valid:
            Card card = base.DataProvider.Cards.FindCard(number);
            if (card == null)
                return base.ToCardError(StringResources.WrongCardNumber);
            if (!card.IsValid)
                return base.ToCardError(StringResources.CardIsBlocked);

            // Checking if Pin is correct:
            if (base.DataProvider.Cards.FindCard(number, hashedPin) == null)
            {
                // Card with specified Number exists but Pin is wrong:
                return this.OnWrongAttempt(card);
            }
            // Card Number and Pin are valid. Granting Access to Operations:
            base.Session.SetPin(hashedPin);
            card.WrongAttempts = 0;
            base.DataProvider.Cards.Save(card);
            return base.ToCardOperations();
        }

        /// <summary>
        /// Handles wrong Pin Input.
        /// </summary>
        /// <param name="card">Card wrong Pin was entered for.</param>
        private ActionResult OnWrongAttempt(Card card)
        {
            try
            {
                // Increasing Number of Wrong Attempts for this Card:
                card.WrongAttempts++;
                // Checking if Card is still valid:
                if (card.WrongAttempts < Card.MaximumAttempts)
                {
                    base.ModelState.AddModelError("Pin", string.Format(StringResources.WrongPinMessageFormat, card.WrongAttempts, Card.MaximumAttempts));
                    return View("PinRequest", new PinRequest());
                }
                else
                {
                    card.IsValid = false;
                    return base.ToCardError(StringResources.CardWasBlocked);
                }
            }
            finally
            {
                base.DataProvider.Cards.Save(card);
            }
        }

        /// <summary>
        /// Shows Card Error.
        /// </summary>
        public ActionResult CardError(CardError error)
        {
            return View(error);
        }
    }
}
