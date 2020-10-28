using System;
using System.Collections.Generic;
using System.Text;

namespace Euro_diffusion
{
    public class City
    {
        public const int INITIAL_BALANCE = 1000000;
        public const int PAY_PERCENT = 1000;

        public String Country;
        public int X;
        public int Y;
        public int Balance;
        public int CachedIncome;
        public List<City> Neighbors;

        public City(String country, int x, int y, int initialBalance = INITIAL_BALANCE)
        {
            Country = country;
            X = x;
            Y = y;
            Balance = initialBalance;
            CachedIncome = 0;
            Neighbors = new List<City>();
        }

        public bool IsEmpty()
        {
            return Balance == 0 && CachedIncome == 0;
        }

        public void AddIncome(int amount)
        {
            CachedIncome += amount;
        }

        public void UpdateBalance()
        {
            Balance += CachedIncome;
            CachedIncome = 0;
        }
    }
}
