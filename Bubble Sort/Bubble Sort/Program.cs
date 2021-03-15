using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bubble_Sort
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a = { 3, 0, 2, -1, 4, 1 };
            int t;
            Console.WriteLine("Urutan Data Awal :");
            foreach (int aa in a)
            Console.Write(aa + " ");

            //Metode bubble sort
            for (int p = 0; p <= a.Length-2; p++)
            {
                for (int i = 0; i <= a.Length-2; i++)
                {
                    if (a[i] > a[i + 1])
                    {
                        t = a[i + 1];
                        a[i + 1] = a[i];
                        a[i] = t;
                    }
                }
            }
                       

            Console.WriteLine("\n" + "Data Terurut dengan Bubble sort :");
            foreach (int aa in a)
            Console.Write(aa + " ");
            Console.Write("\n");

        }
    }
}
