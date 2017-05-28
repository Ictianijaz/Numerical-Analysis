using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NA_PROJECT
{
    class poly
    {
        string fun;
        List<double> coef;

        public List<double> Coef
        {
            get { return coef; }
            set { coef = value; }
        }
        List<double> pow;
        List<double> coef1;
        List<double> pow1;
        public poly(string s)
        {
            fun = s;
            coef = new List<double>();
            pow = new List<double>();
            coef1 = new List<double>();
            pow1 = new List<double>();
            this.split_coefficients_power();
            this.first_derivative();
        }
        public double function(double value)
        {
            double temp = 0;
            for (int loop = 0; loop < pow.Count; loop++)
            {
                temp += coef[loop] * Math.Pow(value, pow[loop]);
            }
            return temp;
        }
        public void first_derivative()
        {
            double t1;
            double t2;
            for (int i = 0; i < pow.Count; i++)
            {
                t1 = coef[i];
                t2 = pow[i];
                if (t2 != 0)
                {
                    coef1.Add(t1 * t2);
                    pow1.Add(t2 - 1);
                }
            }
        }
        public double function_prime(double value)
        {
            double temp = 0;
            for (int loop = 0; loop < pow1.Count; loop++)
            {
                temp += coef1[loop] * Math.Pow(value, pow1[loop]);
            }
            return temp;
        }
        public void split_coefficients_power()
        {

            string s = fun;
            if (s.Length == 1)
            {
                coef.Add(double.Parse(s));
            }
            else
            {
                s = s.Replace('+', ' ');
                char[] arr = s.ToCharArray();
                s = "";
                s += arr[0];
                for (int i = 1; i < arr.Length; i++)
                {
                    if (arr[i] == '-' && arr[i - 1] != '^')
                        s += " " + arr[i];
                    else
                        s += arr[i];
                }
                char[] p = new char[] { ' ' };
                char[] sp = new char[] { '^' };
                string[] u = s.Split(p, StringSplitOptions.RemoveEmptyEntries);
                List<double> st = new List<double>();
                for (int loop = 0; loop < u.Length; loop++)
                {
                    if (u[loop].Contains("x^"))
                    {

                        string[] t = u[loop].Split(sp, StringSplitOptions.RemoveEmptyEntries);
                        u[loop] = t[0] + t[1];
                        t = u[loop].Split('x');
                        st.Add(double.Parse(t[0]));
                        st.Add(double.Parse(t[1]));
                    }
                    else
                        st.Add(double.Parse(u[loop]));
                }
                for (int j = 0; j < st.Count; j++)
                {
                    if (j % 2 == 0)
                        coef.Add(st[j]);
                    else
                        pow.Add(st[j]);
                }
            }
            if (coef.Count > pow.Count)
            {
                pow.Add(0);
            }
        }
    }
}
