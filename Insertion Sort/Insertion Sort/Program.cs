using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insertion_Sort
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] num = {9,6,7,4,8,0,11,16};

            //Metode Insertion Sort

            for (int i = 0; i < num.Length; i++)
			{
			    int key = num[i];
                int j = i - 1;
                while (j >= 0 && num[j] > key)
	            {
	                num[j + 1] = num[j];
                    j--;
	            }
                num[j+1] = key;
			}

            foreach (var item in num)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();

        }
    }
}
