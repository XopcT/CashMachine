using System.Web.Mvc;
using CashMachine.Data;

namespace CashMachine.Controllers
{
    /// <summary>
    /// Base Class for Controllers.
    /// </summary>
    public class ControllerBase : Controller
    {
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