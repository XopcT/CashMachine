using System;
using System.Linq;

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
            throw new NotImplementedException();
        }
    }
}
