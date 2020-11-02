using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Euro_diffusion
{
    public static class UI
    {
        public static List<string[]> ConsoleReader()
        {
            Console.WriteLine($"Enter number of countries from {Constants.MIN_COUNTRY_COUNT} to {Constants.MAX_COUNTRY_COUNT}");
            var isNum = int.TryParse(Console.ReadLine(), out int countryNumber);
            while (!isNum || countryNumber > Constants.MAX_COUNTRY_COUNT || countryNumber < Constants.MIN_COUNTRY_COUNT)
            {
                Console.WriteLine($"Number of countries should be integer ana belong between {Constants.MIN_COUNTRY_COUNT} and {Constants.MAX_COUNTRY_COUNT}");
                isNum = int.TryParse(Console.ReadLine(), out countryNumber);
            }
            Console.WriteLine($"Enter the country description in format 'name xl yl xh yh', where position pointers should be between {Constants.MIN_POS} and {Constants.MAX_POS},  and display lower left and upper right angles of country");
            var countryList = new List<string[]>();
            for (int i = 0; i < countryNumber; i++)
            {
                string[] countryString = Console.ReadLine().Split(' ');
                while (!CountryInputParser(countryString))
                    countryString = Console.ReadLine().Split(' ');
                countryList.Add(countryString);
            }
            return countryList;
        }

        public static bool CountryInputParser(string[] str)
        {
            if (str.Length != Constants.PARAMETER_COUNT)
            {
                Console.WriteLine("Wrong format. Use fomat 'name xl yl xh yh'");
                return false;
            }
            if (str[0].Length > Constants.MAX_COUNTRY_LENGTH || !Regex.IsMatch(str[0], @"^[a-zA-Z]+$"))
            {
                Console.WriteLine($"Country name should be alphabetic and and not exceed {Constants.MAX_COUNTRY_LENGTH} symbols");
                return false;
            }
            for (int i = 1; i < Constants.PARAMETER_COUNT; i++)
                if (!int.TryParse(str[i], out int position) || position > Constants.MAX_POS || position < Constants.MIN_POS)
                {
                    Console.WriteLine($"Position pointers should be integer and belong between {Constants.MIN_POS} and {Constants.MAX_POS}");
                    return false;
                }
            if (int.Parse(str[1]) > int.Parse(str[3]) || int.Parse(str[2]) > int.Parse(str[4]))
            {
                Console.WriteLine("Position pointers should display lower left and upper right angles of country");
                return false;
            }
            return true;
        }

        public static List<string> FileReader(string path = "D:\\!Kvitka\\training\\Еuro_diffusion\\Euro_diffusion\\Euro_diffusion\\Test_files\\test.txt")
        {
            using StreamReader sr = new StreamReader(path, System.Text.Encoding.Default);
            var buffer = new List<string>();
            string line;
            while ((line = sr.ReadLine()) != null)
                buffer.Add(line);
            return buffer;
        }

        public static List<List<string[]>> BufferReader(List<string> buffer)
        {
            var count = 0;
            var europeList = new List<List<string[]>>();
            while (count < buffer.Count)
            {
                var isNum = int.TryParse(buffer[count], out int countryNumber);
                if (isNum && countryNumber <= Constants.MAX_COUNTRY_COUNT && countryNumber >= Constants.MIN_COUNTRY_COUNT)
                {
                    var countryList = new List<string[]>();
                    for (int i = count + 1; i < count + countryNumber + 1; i++)
                    {
                        string[] countryString = buffer[i].Split(' ');
                        if (CountryInputParser(countryString))
                            countryList.Add(countryString);
                    }
                    count += countryNumber + 1;
                    europeList.Add(countryList);
                }
                else
                    count++;
            }
            return europeList;
        }

        public static int Mode()
        {
            Console.WriteLine("Choose test mode (1) or console mode (2)");
            var modeString = int.TryParse(Console.ReadLine(), out int mode);
            while (!modeString || mode < 1 || mode > 2)
            {
                modeString = int.TryParse(Console.ReadLine(), out mode);
            }
            return mode;
        }
    }
}
