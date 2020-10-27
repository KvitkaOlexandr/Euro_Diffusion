using System;
using System.Collections.Generic;

namespace Euro_diffusion
{
    class Program
    {
        static void Main(string[] args)
        {
            var europe = new Europe();
            foreach (var country in europe.CountryList)
                Console.WriteLine(country.ToString());
        }
    }
}
