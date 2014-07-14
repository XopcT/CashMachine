using System.Web.Mvc;
using CashMachine.Data;
using CashMachine.Models;

namespace CashMachine.Controllers
{
    /// <summary>
    /// Base Class for Controllers.
    /// </summary>
    public class ControllerBase : Controller
    {
        /// <summary>
        /// Redirects to Card Number Request.
        /// </summary>
        public ActionResult ToNumberRequest()
        {
            return base.RedirectToAction("NumberRequest", "Login");
        }

        /// <summary>
        /// Redirects to Pin Request.
        /// </summary>
        public ActionResult ToPinRequest()
        {
            return base.RedirectToAction("PinRequest", "Login");
        }

        /// <summary>
        /// Redirects to Card Operations.
        /// </summary>
        public ActionResult ToCardOperations()
        {
            return base.RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Redirects to Card Error Page.
        /// </summary>
        /// <param name="errorMessage">Error Message.</param>
        public ActionResult ToCardError(string errorMessage)
        {
            // Invalidating Card's Permission:
            base.Session.SetCardNumber(string.Empty);
            base.Session.SetPin(string.Empty);
            // Showing Error Message:
            return base.RedirectToAction("CardError", "Login", new CardError() { ErrorMessage = errorMessage });
        }

        /// <summary>
        /// Retrieves the Data Provider.
        /// </summary>
        public IDataProvider DataProvider
        {
            get { return dataProvider; }
        }

        #region Fields

        private static readonly UnityDataProvider dataProvider = new UnityDataProvider();

        #endregion


    }
}