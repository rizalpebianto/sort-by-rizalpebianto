using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selection_Sort
{
    class Program
    {
        static void Main(string[] args)
        {
            int array_size = 10;
            int[] array = new int[10] { 23, 43, 67, 41, 10, 75, 62, 78, 90, 11 };
            Console.WriteLine("Data Array Sebelum Pengurutan : ");
            for (int i = 0; i < array_size; i++)
            {
                Console.Write(array[i] + ", ");
            }
            Console.WriteLine();

            int tmp, min_key;

            for (int j = 0; j < array_size; j++)
            {
                min_key = j;

                for (int k = j; k < array_size; k++)
                {
                    if (array[k] < array[min_key])
                    {
                        min_key = k;
                    }
                }
                tmp = array[min_key];
                array[min_key] = array[j];
                array[j] = tmp;
            }

            Console.WriteLine();
            Console.WriteLine("Sesudah Pengurutan dengan metode Selection Sort : ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write(array[i] + ",");
            }

            Console.WriteLine();
            Console.ReadLine();

        }
    }
}
