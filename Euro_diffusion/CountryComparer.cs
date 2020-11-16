using System;
using System.Collections.Generic;
using System.Text;

namespace Euro_diffusion
{
    public class CountryComparer : IComparer<Country>
    {
        public int Compare(Country x, Country y)
        {
            if (x.CompletionDay == -1)
            {
                if (y.CompletionDay == -1)
                    return 0;
                else
                    return -1;
            }
            else
            {
                if (y.CompletionDay == -1)
                    return 1;
                else
                {
                    int retval = x.CompletionDay.CompareTo(y.CompletionDay);

                    if (retval != 0)
                        return retval;
                    else
                        return x.CompareTo(y);
                }
            }
        }
    }
}
