using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class FacadeControler
    {

        private FacadeControler() { }
        private static FacadeControler Instance = null;
        public static FacadeControler getFacadeControler()
        {
            if (Instance == null)
                Instance = new FacadeControler();
            return Instance;
        }
        public double calculate_lagrange_value(List<double> allX, List<double> allY, double x)
        {
            double num;
            double deno;
            double result = 0;
            for (int i = 0; i < allX.Count; i++)
            {
                num = 1;
                deno = 1;
                for (int j = 0; j < allX.Count; j++)
                {
                    if (i != j)
                    {
                        num *= (x - allX[j]);
                        deno *= (allX[i] - allX[j]);
                    }
                }
                result += allY[i] * (num / deno);
            }
            result = Math.Round(result, 4);
                return result;
        }
        public string calculate_lagrange_ploy(List<double> allX, List<double> allY, int degree)
        {
            string ploy = "ejaz";
            return ploy;
        }
    }
}
