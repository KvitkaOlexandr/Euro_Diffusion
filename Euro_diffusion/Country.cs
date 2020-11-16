using System;
using System.Collections.Generic;

namespace Euro_diffusion
{
    public class Country: IComparable
    {
        private const int COORDINATES_COUNT = 4;

        public string Name;
        public int Xl;
        public int Yl;
        public int Xh;
        public int Yh;
        public List<City> Cities;
        public int CompleteCitiesCount;
        public int CompletionDay;

        public Country(string name, List<int> position)
        {
            if (position.Count != COORDINATES_COUNT)
                throw new ArgumentException($"Country should have {COORDINATES_COUNT} coordinates");
            Name = name;
            Xl = position[0];
            Yl = position[1];
            Xh = position[2];
            Yh = position[3];
            Cities = new List<City>();
            for (int i = Xl; i <= Xh; i++)
                for (int j = Yl; j <= Yh; j++)
                    Cities.Add(new City(Name, i, j));
            CompleteCitiesCount = 0;
            CompletionDay = -1;
        }

        public override bool Equals(object obj)
        {
            return obj is Country country && Name.Equals(country.Name) &&
                Xl == country.Xl &&
                Yl == country.Yl &&
                Xh == country.Xh &&
                Yh == country.Yh;
        }

        public override string ToString()
        {
            return $"{Name} {Xl} {Yl} {Xh} {Yh}";
        }

        public void AddCompleteCity(int day)
        {
            CompleteCitiesCount++;
            if (CompleteCitiesCount == Cities.Count)
                CompletionDay = day;
        }

        public string Result()
        {
            return $"{Name} {CompletionDay}";
        }

        public int CompareTo(object obj)
        {
            if (obj == null) 
                throw new ArgumentException("Object is null");
            if (obj is Country country)
                return Name.CompareTo(country.Name);
            else
                throw new ArgumentException("Object is not a Country");
        }
    }
}