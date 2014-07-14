using System.Data.Common;
using System.Linq;
using CashMachine.Data;
using System.Transactions;

namespace CashMachine.Storage
{
    /// <summary>
    /// Repository of Cards.
    /// </summary>
    public class CardsRepository : RepositoryBase, Data.ICardsRepository
    {
        /// <summary>
        /// Converts specified Entity into Card.
        /// </summary>
        /// <param name="source">Entity to convert.</param>
        /// <returns>Card Instance.</returns>
        private Data.Card Convert(Card source)
        {
            return new Data.Card()
            {
                Number = source.Number,
                Balance = source.Balance,
                WrongAttempts = source.WrongAttempts,
                IsValid = source.IsValid,
            };
        }

        /// <summary>
        /// Looks for a Card with specified Number.
        /// </summary>
        /// <param name="number">Number of the Card.</param>
        /// <returns>Card Instance. Null if Card not found.</returns>
        public Data.Card FindCard(string number)
        {
            using (StorageDataContext context = base.CreateDataContext())
            {
                return context.Cards
                    .Where(card => card.Number == number)
                    .Select(card => this.Convert(card))
                    .FirstOrDefault();
            }
        }

        /// <summary>
        /// Retrieves a Card with specified Number and Pin.
        /// </summary>
        /// <param name="number">Number of the Card.</param>
        /// <param name="hashedPin">Hashed Card's Pin.</param>
        /// <returns>Card Instance. Null if Card not found.</returns>
        public Data.Card FindCard(string number, string hashedPin)
        {
            using (StorageDataContext context = base.CreateDataContext())
            {
                return context.Cards
                    .Where(card => card.Number == number && card.HashedPin == hashedPin)
                    .Select(card => this.Convert(card))
                    .FirstOrDefault();
            }
        }

        /// <summary>
        /// Retrieves a Card's Balance.
        /// </summary>
        /// <param name="number">Number of the Card to get Balance for.</param>
        /// <returns>Card's Balance.</returns>
        public decimal GetBalance(string number)
        {
            using (StorageDataContext context = base.CreateDataContext())
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    // Gettings Card's Balance:
                    Card existing = context.Cards
                        .Where(card => card.Number == number)
                        .First();

                    // Creating Operation's Log:
                    Operation operation = new Operation()
                    {
                        CardId = existing.Id,
                        OperationCode = (int)OperationCodes.CheckBalance,
                        Timestamp = System.DateTime.Now,
                    };
                    context.Operations.InsertOnSubmit(operation);

                    // Saving Data:
                    context.SubmitChanges();
                    transaction.Complete();

                    return existing.Balance;
                }
            }
        }

        /// <summary>
        /// Removes Cash from the Card.
        /// </summary>
        /// <param name="cardNumber">Card to modify.</param>
        /// <param name="delta">Amount of money to remove.</param>
        public void GetCash(string cardNumber, decimal delta)
        {
            using (StorageDataContext context = base.CreateDataContext())
            {
                using(TransactionScope transaction = new TransactionScope())
                {
                    // Updating Card's Balance:
                    Card existing = context.Cards.Where(card => card.Number == cardNumber).First();
                    existing.Balance -= delta;

                    // Creating Operation's Log:
                    Operation operation = new Operation()
                    {
                        CardId = existing.Id,
                        Amount = delta,
                        OperationCode = (int)OperationCodes.GetCash,
                        Timestamp = System.DateTime.Now,
                    };
                    context.Operations.InsertOnSubmit(operation);

                    // Saving Data:
                    context.SubmitChanges();
                    transaction.Complete();
                }
            }
        }

        /// <summary>
        /// Saves specified Card.
        /// </summary>
        /// <param name="obj">Card to save.</param>
        public void Save(Data.Card obj)
        {
            using (StorageDataContext context = base.CreateDataContext())
            {
                Card existing = context.Cards
                    .Where(card => card.Number == obj.Number)
                    .First();
                existing.Balance = obj.Balance;
                existing.WrongAttempts = obj.WrongAttempts;
                existing.IsValid = obj.IsValid;
                context.SubmitChanges();
            }
        }

        /// <summary>
        /// Deletes specified Card.
        /// </summary>
        /// <param name="obj">Card to delete.</param>
        public void Delete(Data.Card obj)
        {
            using (StorageDataContext context = base.CreateDataContext())
            {
                Card existing = context.Cards
                    .Where(card => card.Number == obj.Number)
                    .First();
                context.Cards.DeleteOnSubmit(existing);
                context.SubmitChanges();
            }
        }
    
    }
}
