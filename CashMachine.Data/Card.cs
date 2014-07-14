
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
        public decimal Balance { get; set; }

        /// <summary>
        /// Retrieves the Number of wrong Pin Enter Attempts.
        /// </summary>
        public int WrongAttempts { get; set; }

        /// <summary>
        /// Sets/retrieves whether the Card is valid or blocked.
        /// </summary>
        public bool IsValid { get; set; }

        #endregion

        /// <summary>
        /// Retrieves the maximum allowed Number of wrong Attempts.
        /// </summary>
        public const int MaximumAttempts = 4;

    }
}