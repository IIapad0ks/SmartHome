using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.SmartHome
{
    public static class Utils
    {
        public static int GenerateRandomValue(int currentValue, int minValue, int maxValue, int minStep, int maxStep, ref bool isGrow)
        {
            int step = new Random().Next(minStep, maxStep);

            if ((isGrow && currentValue + step >= maxValue) || (!isGrow && currentValue - step <= minValue))
            {
                isGrow = !isGrow;
            }

            return isGrow ? currentValue + step : currentValue - step;   
        }
    }
}
