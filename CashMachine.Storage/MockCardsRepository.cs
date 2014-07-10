using System.Collections.Generic;
using System.Linq;
using CashMachine.Data;

namespace CashMachine.Storage
{
    /// <summary>
    /// Mock Cards Repository.
    /// </summary>
    public class MockCardsRepository : ICardsRepository
    {
        public Card FindCard(string number)
        {
            return this.cards.Where(card => card.Number == number).FirstOrDefault();
        }

        private readonly IEnumerable<Card> cards = new Card[]
        {
            new Card(){ Number="123", IsValid=false},
            new Card(){ Number="111", IsValid=true },
        };
    }
}
