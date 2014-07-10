using System.Web.Mvc;
using CashMachine.Data;
using CashMachine.Models;

namespace CashMachine.Controllers
{
    /// <summary>
    /// Controller for Pin Page.
    /// </summary>
    public class PinCodeController : ControllerBase
    {
        /// <summary>
        /// Entry Point.
        /// </summary>
        public ActionResult Index()
        {
            string number = base.Session.GetCardNumber();
            Card card = base.DataProvider.Cards.FindCard(number);

            if (card == null)
                return base.ToCardError(StringResources.WrongCardNumber);
            if (!card.IsValid)
                return base.ToCardError(StringResources.CardIsBlocked);

            //  Checking for wrong Attempts:
            string attemptsMessage = string.Empty;
            if(card.WrongAttempts > 0)
                attemptsMessage = string.Format(StringResources.WrongPinMessageFormat, card.WrongAttempts, Card.MaximumAttempts);
            return View(new PinRequest() { PinErrorMessage = attemptsMessage });
        }
        
        /// <summary>
        /// Tries to access Card with specified Pin.
        /// </summary>
        /// <param name="request">Pin Request.</param>
        [HttpPost()]
        public ActionResult TryAccess(PinRequest request)
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
            if(base.DataProvider.Cards.FindCard(number, hashedPin) == null)
            {
                // Card with specified Number exists but Pin is wrong:
                return this.OnWrongAttempt(card);
            }

            // Card Number and Pin are valid. Granting Access to Operations:
            card.WrongAttempts = 0;
            base.DataProvider.Cards.Save(card);
            base.Session.SetPin(hashedPin);
            return base.ToCardOperations();
        }

        /// <summary>
        /// Handles wrong Pin Input.
        /// </summary>
        /// <param name="card">Card wrong Pin was entered for.</param>
        private ActionResult OnWrongAttempt(Card card)
        {
            // Increasing Number of Wrong Attempts for this Card:
            card.WrongAttempts++;
            // Checking if there still are some Attempts:
            if (card.WrongAttempts >= Card.MaximumAttempts)
                card.IsValid = false;
            base.DataProvider.Cards.Save(card);

            // Checking if Card is still Valid:
            if (card.IsValid)
                return base.ToPinRequest();
            else
            {
                // Invalidating Card Number:
                base.Session.SetCardNumber(string.Empty);
                return base.ToCardError(StringResources.CardWasBlocked);
            }
        }

    }
}
