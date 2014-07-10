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
            return base.RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Redirects to Pin Request.
        /// </summary>
        public ActionResult ToPinRequest()
        {
            return base.RedirectToAction("Index", "PinCode");
        }

        /// <summary>
        /// Redirects to Card Operations.
        /// </summary>
        public ActionResult ToCardOperations()
        {
            return base.RedirectToAction("Index", "CardOperations");
        }

        /// <summary>
        /// Redirects to Card Error Page.
        /// </summary>
        /// <param name="errorMessage">Error Message.</param>
        public ActionResult ToCardError(string errorMessage)
        {
            return base.RedirectToAction("CardError", "Home", new CardError() { ErrorMessage = errorMessage });
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