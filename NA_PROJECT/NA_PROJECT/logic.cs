using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace NA_PROJECT
{
    class logic
    {
        static List<double> x0_values = new List<double>() { 0.7, -2, 2.9, 1.5, -3, 4.3, 3.7, 1.4, -2, 0.5 };
        public static double iterative_solution(int index, int prec)
        {
            double x0 = x0_values[index];
            double x1 = 0;
            bool flag = true;
            do
            {
                x1 = iteration_fun(x0, index);
                if (Math.Round(x1, prec) == Math.Round(x0, prec))
                    flag = false;
                x0 = x1;
            } while (flag);
            return Math.Round(x1, prec);
        }
        static double iteration_fun(double value, int index)
        {
            if (index == 0)
                return (Math.Sqrt(1 / (1 + value)));
            else if (index == 1)
                return (-1 * Math.Sqrt(((2 * value) - 5) / value));
            else if (index == 2)
                return (Math.Sqrt(((9 * value) - 1) / value));
            else if (index == 3)
                return (Math.Sqrt((value + 5) / (value + 1)));
            else if (index == 4)
                return (-1 * Math.Sqrt(((3 * value) - 5) / value));
            else if (index == 5)
                return (Math.Sqrt(100 / (1 + value)));
            else if (index == 6)
                return ((Math.Log10(value) + 7) / 2);
            else if (index == 7)
                return (Math.Sin(value) + 0.5);
            else if (index == 8)
                return ((Math.Pow(2, value) - 3));
            else
                return ((1 + Math.Cos(value)) / 3);
        }
        public static double calculate_lagrange_value(List<double> allX, List<double> allY, double x)
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
        public static double calculate_newtonDD_value(List<double> allx, List<double> ally, double key)
        {
            double[] allX = allx.ToArray();
            double[] allY = ally.ToArray();
            double answer = allY[0];
            int count = ally.Count;
            int i;
            double temp1 = 0;
            double temp2 = 0;
            int j = 1;
            do
            {
                for (i = 0; i < count - 1; i++)
                {
                    allY[i] = ((allY[i + 1] - allY[i]) / (allX[i + j] - allX[i]));
                }
                temp1 = 1;
                for (i = 0; i < j; i++)
                {
                    temp1 *= (key - allX[i]);
                }
                temp2 += (allY[0] * temp1);
                count--;
                j++;
            } while (count != 1);
            answer += temp2;
            return answer;
        }
        public static double Bisection_solution(string s, double x0, double x1, int prec, ref bool fal)
        {
            poly p = new poly(s);
            if(p.Coef.Count == 1)
                return p.Coef[0];
            double r = 0;
            bool x_0 = true;
            bool flag = true;
            int myCount = 0;
            do
            {
                if (p.function(x0) < 0)
                    x_0 = false;
                else
                    x_0 = true;
                flag = true;
                r = (x0 + x1) / 2;
                if (Math.Round(x0, prec) == Math.Round(r, prec) || Math.Round(x1, prec) == Math.Round(r, prec))
                {
                    flag = false;
                }
                if (p.function(r) < 0)
                {
                    if (x_0)
                        x1 = r;
                    else
                        x0 = r;
                }
                else
                {
                    if (x_0)
                        x0 = r;
                    else
                        x1 = r;
                }
                if (myCount > 100)
                {
                    fal = false;
                    break;
                }
            } while (flag);
            return Math.Round(r, prec);
        }
        public static double newton_solution(string s, double x0, int prec, ref bool fal)
        {
            poly p = new poly(s);
            if (p.Coef.Count == 1)
                return p.Coef[0];
            double x1;
            bool flag = true;
            int myCount = 0;
            do
            {
                x1 = x0 - (p.function(x0) / p.function_prime(x0));
                if (Math.Round(x1, prec) == Math.Round(x0, prec))
                    flag = false;
                x0 = x1;
                if (myCount > 100)
                {
                    fal = false;
                    break;
                }
                myCount++;
            } while (flag);
            return Math.Round(x1, prec);
        }
        public static double regula_falsi_solution(string s, double x0, double x1, int prec, ref bool fal)
        {
            poly p = new poly(s);
            if (p.Coef.Count == 1)
                return p.Coef[0];
            double r = 0;
            bool x_0 = true;
            bool flag = true;
            double f_x0 = 0;
            double f_x1 = 0;
            int myCounter = 0;
            do
            {
                f_x0 = p.function(x0);
                f_x1 = p.function(x1);
                if (f_x0 < 0)
                    x_0 = false;
                else
                    x_0 = true;
                flag = true;
                r = x0 - (((x1 - x0) / (f_x1 - f_x0)) * f_x0);
                if (Math.Round(x0, prec) == Math.Round(r, prec) || Math.Round(x1, prec) == Math.Round(r, prec))
                {
                    flag = false;
                }
                if (p.function(r) < 0)
                {
                    if (x_0)
                        x1 = r;
                    else
                        x0 = r;
                }
                else
                {
                    if (x_0)
                        x0 = r;
                    else
                        x1 = r;
                }
                if (myCounter > 100)
                {
                    fal = false;
                    break;
                }
                myCounter++;
            } while (flag);
            return Math.Round(r, prec);
        }
        public static bool checkPoly(string s)
        {
            bool flage = true;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == 'X')
                    s = s.Replace('X','x');
                if (char.IsLetter(s[i]))
                {
                    if (s[i] != 'x')
                    {
                        flage = false;
                        break;
                    }
                }
                if (s[i] == 'x' && (i == 0 || i == s.Length - 1))
                {
                    flage = false;
                    break;
                }
                else if (s[i] == 'x' && (s[i - 1] == '-' || s[i - 1] == '+' || s[i + 1] != '^'))
                {
                    flage = false;
                    break;
                }
            }
            return flage;
        }
    }
}
