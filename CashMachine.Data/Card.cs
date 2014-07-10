
namespace CashMachine.Data
{
    /// <summary>
    /// Contains Data for a Card.
    /// </summary>
    public class Card
    {
        #region Properties

        /// <summary>
        /// Sets/retrieves the Card's Number.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Sets/retrieves the Card's Balance.
        /// </summary>
        public float Balance { get; set; }

        /// <summary>
        /// Sets/retrieves whether the Card is valid or blocked.
        /// </summary>
        public bool IsValid { get; set; }

        #endregion
    }
}