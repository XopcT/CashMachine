using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashMachine.Models
{
    /// <summary>
    /// Contains Data for Card Balance.
    /// </summary>
    public class CardBalance
    {
        #region Properties
        /// <summary>
        /// Sets/retieves the Card Number.
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// Sets/retrieves the Card Balance.
        /// </summary>
        public decimal Balance { get; set; }

        #endregion
    }
}