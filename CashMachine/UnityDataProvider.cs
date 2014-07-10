using CashMachine.Data;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace CashMachine
{
    /// <summary>
    /// Data Provider via Unity Container.
    /// </summary>
    public class UnityDataProvider : IDataProvider
    {
        /// <summary>
        /// Initializes a new Instance of current Class.
        /// </summary>
        public UnityDataProvider()
        {
            this.container = new UnityContainer().LoadConfiguration("default");
        }

        #region Properties

        /// <summary>
        /// Retrieves the Cards Repository.
        /// </summary>
        public ICardsRepository Cards
        {
            get { return this.container.Resolve<ICardsRepository>(); }
        }

        #endregion

        #region Fields

        private readonly IUnityContainer container = null;

        #endregion


    }
}