using System;
using System.Collections.Generic;

namespace Euro_diffusion
{
    public class Europe
    {
        public List<Country> CountryList;
        public int[,] Map;
        public int CompliteCountriesCount;

        public Europe(List<string[]> countryArr)
        {
            Map = new int[12, 12];
            CountryList = new List<Country>();
            foreach (var countryString in countryArr)
            {
                Country country = StringToCountry(countryString);
                if (CheckNewCountry(country))
                    AddCountry(country);
            }
            SetNeighbors();
            CheckNeighbors();
            CompliteCountriesCount = 0;
        }

        private void AddCountry(Country country)
        {
            CountryList.Add(country);
            for (int i = country.Xl; i <= country.Xh; i++)
            {
                for (int j = country.Yl; j <= country.Yh; j++)
                {
                    Map[i, j] = 1;
                }
            }
        }

        private bool CheckNewCountry(Country newCountry)
        {
            foreach (Country country in CountryList)
            {
                if (country.Equals(newCountry))
                {
                    Console.WriteLine($"Country {newCountry.Name} already in list");
                    return false;
                }
                if (!CheckMap(newCountry))
                {
                    Console.WriteLine($"Country {newCountry.Name} encroaches on {country.Name}'s territory");
                    return false;
                }
            }
            return true;
        }

        private Country StringToCountry(string[] parameters)
        {
            string name = parameters[0];
            List<int> position = new List<int>();
            for (int i = 1; i < Constants.PARAMETER_COUNT; i++)
                position.Add(int.Parse(parameters[i]));
            return new Country(name, position);
        }

        private bool CheckMap(Country country)
        {
            for (int i = country.Xl; i <= country.Xh; i++)
            {
                for (int j = country.Yl; j <= country.Yh; j++)
                {
                    if (Map[i, j] == 1)
                        return false;
                }
            }
            return true;
        }

        private void SetNeighbors()
        {
            foreach (Country country in CountryList)
            {
                foreach (City city in country.Cities)
                {
                    if (Map[city.X, city.Y] == 1)
                    {
                        if (Map[city.X, city.Y + 1] == 1)
                            city.Neighbors.Add(GetCity(city.X, city.Y + 1));
                        if (Map[city.X + 1, city.Y] == 1)
                            city.Neighbors.Add(GetCity(city.X + 1, city.Y));
                        if (Map[city.X, city.Y - 1] == 1)
                            city.Neighbors.Add(GetCity(city.X, city.Y - 1));
                        if (Map[city.X - 1, city.Y] == 1)
                            city.Neighbors.Add(GetCity(city.X - 1, city.Y));
                    }
                }
            }
        }

        public City GetCity(int x, int y)
        {
            foreach (Country country in CountryList)
            {
                if (country.Xl <= x && country.Yh >= y)
                {
                    foreach (City city in country.Cities)
                    {
                        if (city.X == x && city.Y == y)
                        {
                            return city;
                        }
                    }
                }
            }
            return null;
        }

        public void CheckNeighbors()
        {
            var removable = new List<Country>();
            var count = CountryList.Count;
            foreach (var country in CountryList)
            {
                var hasNaighbors = false;
                int i = country.Xl;
                while (!hasNaighbors && i <= country.Xh)
                {
                    var city1 = GetCity(i, country.Yl);
                    var city2 = GetCity(i, country.Yh);
                    if (city1.CheckNeighbors() || city2.CheckNeighbors())
                        hasNaighbors = true;
                    i++;
                }
                i = country.Yl + 1;
                while (!hasNaighbors && i <= country.Yh - 1)
                {
                    var city1 = GetCity(country.Xl, i);
                    var city2 = GetCity(country.Xh, i);
                    if (city1.CheckNeighbors() || city2.CheckNeighbors())
                        hasNaighbors = true;
                    i++;
                }
                if (!hasNaighbors && count > 1)
                {
                    Console.WriteLine($"{country.Name} has no neighbors");
                    removable.Add(country);
                    count--;
                }
            }
            foreach (var country in removable)
                CountryList.Remove(country);
        }

        public void Sort()
        {
            CountryList.Sort(new CountryComparer());
        }

        public void PrintResult()
        {
            foreach (Country country in CountryList)
                Console.WriteLine(country.Result());
        }
    }
}