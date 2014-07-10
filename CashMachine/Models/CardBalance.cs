using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CashMachine.Models
{
    public class CardBalance
    {
        #region Properties

        public string Balance { get; set; }

        public string CardNumber { get; set; }

        public string Today { get; set; }

        #endregion
    }
}