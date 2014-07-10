
namespace CashMachine.Data
{
    /// <summary>
    /// Provides Data for an Application.
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// Retrieves the Cards Repository.
        /// </summary>
        ICardsRepository Cards { get; }
    }
}
