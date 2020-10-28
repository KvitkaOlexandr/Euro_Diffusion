using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;

namespace Euro_diffusion
{
    public class Europe
    {
        public const int MIN_COUNTRY_COUNT = 1;
        public const int MAX_COUNTRY_COUNT = 20;
        public const int PARAMETER_COUNT = 5;
        public const int MAX_COUNTRY_LENGTH = 25;
        public const int MIN_POS = 1;
        public const int MAX_POS = 10;

        public List<Country> CountryList;
        public int[,] Map;
        public int CompliteCountriesCount;

        public Europe()
        {
            Map = new int[12, 12];
            ConsoleReader();
            CompliteCountriesCount = 0;
        }

        private void ConsoleReader()
        {
            Console.WriteLine($"Enter number of countries from {MIN_COUNTRY_COUNT} to {MAX_COUNTRY_COUNT}");
            var isNum = int.TryParse(Console.ReadLine(), out int countryNumber);
            while (!isNum || countryNumber > MAX_COUNTRY_COUNT || countryNumber < MIN_COUNTRY_COUNT)
            {
                Console.WriteLine($"Number of countries should be integer ana belong between {MIN_COUNTRY_COUNT} and {MAX_COUNTRY_COUNT}");
                isNum = int.TryParse(Console.ReadLine(), out countryNumber);
            }
            Console.WriteLine($"Enter the country description in format 'name xl yl xh yh', where position pointers should be between {MIN_POS} and {MAX_POS},  and display lower left and upper right angles of country");
            CountryList = new List<Country>();
            for (int i = 0; i < countryNumber; i++)
            {
                String[] countryString = Console.ReadLine().Split(' ');
                while (!CountryInputParser(countryString))
                    countryString = Console.ReadLine().Split(' ');
                Country country = StringToCountry(countryString);
                if (CheckNewCountry(country))
                    AddCountry(country);
            }
            SetNeighbors();
        }

        private bool CountryInputParser(String[] str)
        {
            if (str.Length != PARAMETER_COUNT)
            {
                Console.WriteLine("Wrong format. Use fomat 'name xl yl xh yh'");
                return false;
            }  
            if (str[0].Length > MAX_COUNTRY_LENGTH || !Regex.IsMatch(str[0], @"^[a-zA-Z]+$"))
            {
                Console.WriteLine($"Country name should be alphabetic and and not exceed {MAX_COUNTRY_LENGTH} symbols");
                return false;
            }
            for (int i = 1; i < PARAMETER_COUNT; i++)
                if (!int.TryParse(str[i], out int position) || position > MAX_POS || position < MIN_POS)
                {
                    Console.WriteLine($"Position pointers should be integer and belong between {MIN_POS} and {MAX_POS}");
                    return false;
                }
            if (int.Parse(str[1]) > int.Parse(str[3]) || int.Parse(str[2]) > int.Parse(str[4]))
            {
                Console.WriteLine("Position pointers should display lower left and upper right angles of country");
                return false;
            }
            return true;
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

        private Country StringToCountry(String[] parameters)
        {
            String name = parameters[0];
            List<int> position = new List<int>();
            for (int i = 1; i < PARAMETER_COUNT; i++)
                position.Add(int.Parse(parameters[i]));
            return new Country(name, position);
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
                            city.Neighbors.Add(GetCity(city.X + 1, city.Y));
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

        public void PrintResult()
        {
            foreach (Country country in CountryList)
                Console.WriteLine(country.Result());
        }
    }
}