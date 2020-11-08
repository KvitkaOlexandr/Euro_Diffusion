using System;
using System.Collections.Generic;

namespace Euro_diffusion
{
    public class City
    {
        public const int INITIAL_BALANCE = 1000000;
        public const int PAY_PERCENT = 1000;
        public const int START_INCOME = 0;

        public string Country;
        public int X;
        public int Y;
        public Dictionary<string, Money> Balance;
        public List<City> Neighbors;
        public int CompletionDay;

        public City(string country, int x, int y, int initialBalance = INITIAL_BALANCE)
        {
            Country = country;
            X = x;
            Y = y;
            Balance = new Dictionary<string, Money>
            {
                { country, new Money(initialBalance, START_INCOME) }
            };
            Neighbors = new List<City>();
            CompletionDay = -1;
        }

        public void AddIncome(int amount, string country)
        {
            if (Balance.ContainsKey(country))
                Balance[country].CachedIncome += amount;
            else
                Balance.Add(country, new Money(0, amount));
        }

        public void UpdateBalance()
        {
            foreach (KeyValuePair<string, Money> currency in Balance)
                currency.Value.Update();
        }

        public void PayBills()
        {
            foreach (KeyValuePair<string, Money> currency in Balance)
            {
                if (currency.Value.Balance > PAY_PERCENT)
                {
                    int bill = currency.Value.Balance / PAY_PERCENT;
                    foreach (City neighbor in Neighbors)
                    {
                        currency.Value.Balance -= bill;
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
