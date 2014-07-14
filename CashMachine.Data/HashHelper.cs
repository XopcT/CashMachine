using System.Security.Cryptography;
using System.Text;

namespace CashMachine.Data
{
    /// <summary>
    /// Contains Helper Methods to compute hashed Pin for a Card.
    /// </summary>
    public class HashHelper
    {
        /// <summary>
        /// Computes Hash for specified Card Number and Pin.
        /// </summary>
        /// <param name="number">Card Number.</param>
        /// <param name="pin">Card Pin.</param>
        /// <returns>Computed Hash String.</returns>
        public static string ComputeHash(string number, string pin)
        {
            string source = "AddSomeSalt" + pin + number;
            // Computing Hash Bytes:
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(source);
            byte[] hash = md5.ComputeHash(inputBytes);
            // Converting Hash to a String:
            StringBuilder builder = new StringBuilder();
            foreach (byte nextByte in hash)
                builder.Append(nextByte.ToString("X2"));
            return builder.ToString();
        }
    }
}
