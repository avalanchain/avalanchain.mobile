﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avalanchain { 
    public class CryptocurrencyWallet : Account
    {
        public CryptocurrencyWallet()
        {
            this.Type = AccountType.Wallet;
        }
    }
}
