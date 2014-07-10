
namespace CashMachine.Data
{
    /// <summary>
    /// Data Repository.
    /// </summary>
    /// <typeparam name="DataType">Type of Data in Repository.</typeparam>
    public interface IRepository<DataType>
    {
        /// <summary>
        /// Saves specified Object.
        /// </summary>
        /// <param name="obj">Object to save.</param>
        void Save(DataType obj);

        /// <summary>
        /// Deletes specified Object.
        /// </summary>
        /// <param name="obj">Object to delete.</param>
        void Delete(DataType obj);
    }
}
