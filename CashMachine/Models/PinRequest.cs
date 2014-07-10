
namespace CashMachine.Models
{
    /// <summary>
    /// Contains Data for a Pin Request.
    /// </summary>
    public class PinRequest
    {
        /// <summary>
        /// Sets/retrieves an Error Message with Number of wrong Attempts.
        /// </summary>
        public string PinErrorMessage { get; set; }

        /// <summary>
        /// Sets/retrieves the Card's Pin.
        /// </summary>
        public string Pin { get; set; }

        
    }
}