using System;
using System.Collections.Generic;

namespace Euro_diffusion
{
    public class City
    {
        public const int INITIAL_BALANCE = 1000000;
        public const int PAY_PERCENT = 1000;

        public string Country;
        public int X;
        public int Y;
        public Dictionary<string, Money> Balance;
        public Dictionary<string, Money> CachedIncome;
        public List<City> Neighbors;
        public int CompletionDay;

        public City(string country, int x, int y, int initialBalance = INITIAL_BALANCE)
        {
            Country = country;
            X = x;
            Y = y;
            Balance = new Dictionary<string, Money>
            {
                { country, new Money(initialBalance) }
            };
            CachedIncome = new Dictionary<string, Money>();
            Neighbors = new List<City>();
            CompletionDay = -1;
        }

        public void AddIncome(int amount, string country)
        {
            if (CachedIncome.ContainsKey(country))
                CachedIncome[country].MoneyValue += amount;
            else
                CachedIncome.Add(country, new Money(amount));
        }

        public void UpdateBalance()
        {
            foreach (KeyValuePair<string, Money> currency in CachedIncome)
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
            foreach (KeyValuePair<string, Money> currency in Balance)
            {
                if (currency.Value.MoneyValue > PAY_PERCENT)
                {
                    int bill = currency.Value.MoneyValue / PAY_PERCENT;
                    foreach (City neighbor in Neighbors)
                    {
                        currency.Value.MoneyValue -= bill;
                        neighbor.AddIncome(bill, currency.Key);
                    }
                }
            }
        }

        public bool CheckNeighbors()
        {
            foreach (var city in Neighbors)
            {
                if (city.Country != Country)
                    return true;
            }
            return false;
        }
    }
}
