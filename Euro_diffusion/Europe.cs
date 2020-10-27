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

        public Europe()
        {
            Map = new int[10, 10];
            ConsoleReader();
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
                if (Map[i, country.Yl] == 1 || Map[i, country.Yh] == 1)
                    return false;
            }
            for (int i = country.Yl + 1; i < country.Yh; i++)
            {
                if (Map[country.Xl, i] == 1 || Map[country.Xh, i] == 1)
                    return false;
            }
            return true;
        }
    }
}