
namespace CashMachine.Data
{
    /// <summary>
    /// Repository of Cards.
    /// </summary>
    public interface ICardsRepository : IRepository<Card>
    {
        Card FindCard(string number);
    }
}
