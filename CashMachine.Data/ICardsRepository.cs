
namespace CashMachine.Data
{
    /// <summary>
    /// Repository of Cards.
    /// </summary>
    public interface ICardsRepository : IRepository<Card>
    {
        /// <summary>
        /// Looks for a Card with specified Number.
        /// </summary>
        /// <param name="number">Number of the Card.</param>
        /// <returns>Card Instance. Null if Card not found.</returns>
        Card FindCard(string number);

        /// <summary>
        /// Retrieves a Card with specified Number and Pin.
        /// </summary>
        /// <param name="number">Number of the Card.</param>
        /// <param name="hashedPin">Hashed Card's Pin.</param>
        /// <returns>Card Instance. Null if Card not found.</returns>
        Card FindCard(string number, string hashedPin);
    }
}
