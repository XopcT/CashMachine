using System.Web;

namespace CashMachine.Controllers
{
    /// <summary>
    /// Contains Extension Methods for the Session.
    /// </summary>
    public static class SessionExtensions
    {
        /// <summary>
        /// Retrieves the Card Number.
        /// </summary>
        /// <param name="session">Session to get from.</param>
        /// <returns>Card Number.</returns>
        public static string GetCardNumber(this HttpSessionStateBase session)
        {
            object value = session[CardNumberKey];
            return value != null
                ? value.ToString()
                : string.Empty;
        }

        /// <summary>
        /// Sets the Card Number.
        /// </summary>
        /// <param name="session">Target Session.</param>
        /// <param name="value">Card Number to set.</param>
        public static void SetCardNumber(this HttpSessionStateBase session, string value)
        {
            session[CardNumberKey] = value;
        }

        /// <summary>
        /// Retrieves the Pin.
        /// </summary>
        /// <param name="session">Session to get from.</param>
        /// <returns>Pin.</returns>
        public static string GetPin(this HttpSessionStateBase session)
        {
            object value = session[Pin];
            return value != null
                ? value.ToString()
                : string.Empty;
        }

        /// <summary>
        /// Sets the Pin.
        /// </summary>
        /// <param name="session">Target Session.</param>
        /// <param name="value">Pin to set.</param>
        public static void SetPin(this HttpSessionStateBase session, string value)
        {
            session[Pin] = value;
        }

        #region Fields
        private const string CardNumberKey = "number";
        private const string Pin = "pin";
        #endregion
    }
}