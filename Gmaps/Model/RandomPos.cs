using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gmaps.Model
{
    class RandomPos
    {

        public RandomPos()
        { }

        public static double rLatitude()
        {

            Random r = new Random();

            double lat = r.NextDouble();
            int sign = r.Next(0, 2);

            if (sign == 0)
                lat = lat * (-1);

            lat = lat * 90;

            return lat;
        }

        public static double rLongitude()
        {

            Random r = new Random();

            double lon = r.NextDouble();
            int sign = r.Next(0, 2);

            if (sign == 0)
                lon = lon * (-1);

            lon = lon * 180;

            return lon;
        }

    }
}
