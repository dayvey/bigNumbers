using System;
using System.Numerics;

namespace operatiiCuNumereMari
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string a, b;
            string c;

            Console.WriteLine("Citeste numarul intai:");
            a = Console.ReadLine();

            Console.WriteLine("Citeste numarul al doilea:");
            b = Console.ReadLine();

            Console.WriteLine("Citeste operatia:");
            c = Console.ReadLine();

            int n = a.Length - 1;
            int m = b.Length - 1;

            switch (c[0])
            {
                case '+':
                    adunare(a, b, n, m);
                    break;
                case '-':
                    Console.WriteLine(scadere(a, b, n, m));
                    break;
                case '*':
                    Console.WriteLine(inmultire(a, b, n, m));
                    break;
                case '^':
                    string sum = inmultire(a, a, n, n);
                    int num = Convert.ToInt32(b);
                    if (num == 0)
                        Console.WriteLine("1");
                    else if (num == 1)
                        Console.WriteLine(a);
                    else
                    {
                        num -= 2;
                        while (num > 0)
                        {
                            sum = inmultire(sum, a, sum.Length - 1, n);
                            num--;
                        }
                        Console.WriteLine(sum);
                    }
                    break;
                case '/':
                    BigInteger a2 = BigInteger.Parse(a);
                    BigInteger b2 = BigInteger.Parse(b);
                    impartire(a2, b2, n, m);
                    break;
                //functioneaza numai daca pentru c dam "sqrt"
                case 's':
                    BigInteger a3 = BigInteger.Parse(a);
                    Console.WriteLine(rad(a3));
                    break;
            }
        }

        static BigInteger rad(BigInteger n)
        {
            if (n == 0) return 0;
            if (n > 0)
            {
                int bitLength = Convert.ToInt32(Math.Ceiling(BigInteger.Log(n, 2)));
                BigInteger root = BigInteger.One << (bitLength / 2);

                while (!isSqrt(n, root))
                {
                    root += n / root;
                    root /= 2;
                }

                return root;
            }

            throw new ArithmeticException("NaN");
        }

        static Boolean isSqrt(BigInteger n, BigInteger root)
        {
            BigInteger lowerBound = root * root;
            BigInteger upperBound = (root + 1) * (root + 1);

            return (n >= lowerBound && n < upperBound);
        }

        static void impartire (BigInteger a, BigInteger b, int n, int m)
        {
            Console.WriteLine(a / b);
        }
        static string inmultire(string a, string b, int n, int m)
        {
            n++;
            m++;

            int[] inmul = new int[n + m];

            int i_n1 = 0;
            int i_n2 = 0;
            int i;

            for (i = n - 1; i >= 0; i--)
            {
                int carry = 0;
                int n1 = a[i] - 48;

                i_n2 = 0;
           
                for (int j = m - 1; j >= 0; j--)
                {

                    int n2 = b[j] - 48;

                    int sum = n1 * n2 + inmul[i_n1 + i_n2] + carry;

                    carry = sum / 10;

                    inmul[i_n1 + i_n2] = sum % 10;

                    i_n2++;
                }

                if (carry > 0)
                    inmul[i_n1 + i_n2] += carry;

                i_n1++;
            }

            i = inmul.Length - 1;
            while (i >= 0 && inmul[i] == 0)
                i--;

            if (i == -1)
                return "0";

            string s = "";

            while (i >= 0)
                s += (inmul[i--]);

            return s;
        }

        static void adunare(string a, string b, int n, int m)
        {
            int i = n;
            int j = m;

            int c = 0;
            int[] sum = new int[1000];
            int k = 0;

            while (i > -1 && j > -1)
            {
                int num1 = Convert.ToInt32(a[i])-48;
                int num2 = Convert.ToInt32(b[j])-48;

                if (c != 0)
                {
                    sum[k]++;
                    c--;
                }

                if (num1 + num2 >= 10)
                {
                    sum[k] = sum[k] + (num1 + num2 - 10);
                    c++;
                }
                else sum[k] = sum[k] + (num1 + num2);

                i--; j--; k++;
            }

            if (i > -1)
            {
                if (c != 0)
                {
                    sum[k]++;
                    c--;
                }
                while (i > -1)
                {
                    sum[k] = sum[k] + Convert.ToInt32(a[i])-48;
                    k++; i--;
                }
            }
            else
            {
                if (c != 0)
                {
                    sum[k]++;
                    c--;
                }
                while (j > -1)
                {
                    sum[k] = sum[k] + Convert.ToInt32(b[j])-48;
                    j--; k++;
                }
            }

            for (i = k-1; i > -1; i--)
                Console.Write(sum[i]);

        }

        static string scadere(string a, string b, int n, int m)
        {
            int[] sum = new int[1000];
            int k = 0;
            int borrow = 0;

            while (m > -1)
            {
                if (a[n] - borrow == b[m])
                    sum[k] = 0;
                else
                {
                    int i = Convert.ToInt32(a[n]) - 48 - borrow;
                    borrow = 0;
                    int j = Convert.ToInt32(b[m]) - 48;

                    if (i > j)
                        sum[k] = i - j;
                    else
                    {
                        sum[k] = i + 10 - j;
                        borrow = 1;
                    }
                }
                n--; m--; k++;
            }

            while (n > -1)
            {
                if (borrow > 0)
                {
                    sum[k] = Convert.ToInt32(a[n]) - 48 - 1;
                    borrow = 0;
                }
                else sum[k] = Convert.ToInt32(a[n]) - 48;
                n--; k++;
            }
            n = sum.Length - 1;

            while (sum[n] == 0)
                n--;

            string s = "";

            while (n >= 0)
                s += (sum[n--]);

            n = 0;
            int c = 0;
            for (n = 0; n < s.Length && c == 0; n++)
                if (s[n] != '9')
                    c = 1;
            if (c == 1)
                return s;
            else
                return "negativ";
        }
    }
}