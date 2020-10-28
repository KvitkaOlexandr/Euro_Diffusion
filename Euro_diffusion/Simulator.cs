using System;
using System.Collections.Generic;
using System.Text;

namespace Euro_diffusion
{
    public class Simulator
    {
        public Europe Europe;

        public Simulator()
        {
            Europe = new Europe();
        }

        public void Start()
        {
            int days = 0;
            while (Europe.CompliteCountriesCount != Europe.CountryList.Count)
            {
                foreach (Country country in Europe.CountryList)
                {
                    foreach (City city in country.Cities)
                    {
                        city.PayBills();
                    }
                }
                foreach (Country country in Europe.CountryList)
                {
                    foreach (City city in country.Cities)
                    {
                        city.UpdateBalance();
                        if (city.Balance.Count == Europe.CountryList.Count)
                            country.CompleteCitiesCount++;
                    }
                    if (country.CompleteCitiesCount == country.Cities.Count)
                    {
                        country.CompletionDay = days;
                        Europe.CompliteCountriesCount++;
                    } 
                }
            }
            Europe.PrintResult();
        }
    }
}
