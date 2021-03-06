﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Euro_diffusion
{
    public class Simulator
    {
        public Europe Europe;

        public Simulator(List<string[]> countryArr)
        {
            Europe = new Europe(countryArr);
        }

        public void Start()
        {
            int days = 0;
            if (Europe.CountryList.Count > 1)
            {
                while (Europe.CompleteCountriesCount != Europe.CountryList.Count)
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
                            if (city.Balance.Count == Europe.CountryList.Count && city.CompletionDay == -1)
                            {
                                city.CompletionDay = days;
                                country.CompleteCitiesCount++;
                            }        
                        }
                        if (country.CompleteCitiesCount == country.Cities.Count && country.CompletionDay == -1)
                        {
                            country.CompletionDay = days;
                            Europe.CompleteCountriesCount++;
                        }
                    }
                    days++;
                }
            }
            else 
                Europe.CountryList[0].CompletionDay = days;
            Europe.Sort();
            Europe.PrintResult();
        }
    }
}
