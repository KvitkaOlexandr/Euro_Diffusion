using System;
using System.Collections.Generic;
using System.Text;

namespace Euro_diffusion
{
    public class Money
    {
        public int Balance;
        public int CachedIncome;

        public Money(int startBalance, int cachedIncome)
        {
            Balance = startBalance;
            CachedIncome = cachedIncome;
        }

        public void Update()
        {
            Balance += CachedIncome;
            CachedIncome = 0;
        }
    }
}
