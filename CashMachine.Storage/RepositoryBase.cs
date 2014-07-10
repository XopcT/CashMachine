
namespace CashMachine.Storage
{
    /// <summary>
    /// Base Class for Repositories.
    /// </summary>
    public class RepositoryBase
    {
        /// <summary>
        /// Creates Entity Framework's Data Context.
        /// </summary>
        /// <returns>Data Context Instance.</returns>
        internal StorageDataContext CreateDataContext()
        {
            return new StorageDataContext();
        }
    }
}
