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
        public Dictionary<String, Money> Balance;
        public Dictionary<String, Money> CachedIncome;
        public List<City> Neighbors;

        public City(String country, int x, int y, int initialBalance = INITIAL_BALANCE)
        {
            Country = country;
            X = x;
            Y = y;
            Balance = new Dictionary<String, Money>
            {
                { country, new Money(initialBalance) }
            };
            CachedIncome = new Dictionary<String, Money>();
            Neighbors = new List<City>();
        }

        public void AddIncome(int amount, String country)
        {
            if (CachedIncome.ContainsKey(country))
                CachedIncome[country].MoneyValue += amount;
            else
                CachedIncome.Add(country, new Money(amount));
        }

        public void UpdateBalance()
        {
            foreach (KeyValuePair<String, Money> currency in CachedIncome)
            {
                if (Balance.ContainsKey(currency.Key))
                    Balance[currency.Key].MoneyValue += currency.Value.MoneyValue;
                else
                    Balance.Add(currency.Key, currency.Value);
            }
            CachedIncome.Clear();
        }

        public void PayBills()
        {
            foreach (KeyValuePair<String, Money> currency in Balance)
            {
                if (currency.Value.MoneyValue > PAY_PERCENT)
                {
                    int bill = currency.Value.MoneyValue / PAY_PERCENT;
                    if (bill != 0)
                    {
                        foreach (City neighbor in Neighbors)
                        {
                            currency.Value.MoneyValue -= bill;
                            neighbor.AddIncome(bill, Country);
                        }
                    }
                }
            }
        }
    }
}
