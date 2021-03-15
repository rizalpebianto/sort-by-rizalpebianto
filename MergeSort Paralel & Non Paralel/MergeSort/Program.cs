using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//KELOMPOK 2

//Dian Ismiyanti(1706080002)
// Juliet R.A Lao      (1706080003)
// Yulia Dedo Ngara(1706080006)
// Puput Novita Sari(1706080007)
// Nur Ch.Sholichah(1706080008)
// Voni Djara(1706080009)
// Rizal Pebianto(1706080012)


namespace MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            int jumlah_bariskolom = 5000;
            int[,] matriksA = new int[jumlah_bariskolom, jumlah_bariskolom];
            

            //kovernsi ke array 1 dimensi
            int[] matriksA_Convert = new int[jumlah_bariskolom * jumlah_bariskolom];
            int[] matriksB_Convert = new int[jumlah_bariskolom * jumlah_bariskolom];
            


            //Console.WriteLine("Matriks A ");
            //cetakMatriks(matriksB_Convert, jumlah_bariskolom);
            Console.WriteLine(" MERGE SORT");
            Console.WriteLine();

            for (int i = 0; i < 10; i++)
            {
                GenerateMatriksData(matriksA);
                convert1D(matriksA, matriksA_Convert, jumlah_bariskolom);
                convert1D(matriksA, matriksB_Convert, jumlah_bariskolom);
                sw.Reset();
                sw.Start();
                urutNonParalel(matriksA_Convert, jumlah_bariskolom);
                sw.Stop();
                Console.WriteLine(" Dengan Non Paralel dengan waktu : {0:N2} ms", sw.Elapsed.TotalMilliseconds);

                sw.Reset();

                sw.Start();
                urutParalel(matriksB_Convert, jumlah_bariskolom);
                sw.Stop();
                Console.WriteLine(" Dengan Paralel dengan waktu : {0:N2} ms", sw.Elapsed.TotalMilliseconds);
                Console.WriteLine();
            }
            

            //tampilkan yang sudah terurut
            //Console.WriteLine("Sorted Array is: ");
            //cetakMatriks(matriksB_Convert, jumlah_bariskolom);
            Console.ReadLine();
        }

        private static void cetakMatriks(int[] matriks, int jumlah)
        {
            for (int i = 0; i < jumlah * jumlah; i++)
            {
                Console.Write("{0, 4}", matriks[i]);
                if (i % jumlah == jumlah - 1)
                {
                    Console.WriteLine();
                }
            }
        }

        private static void convert1D(int[,] matriks, int[] matriks_convert, int jumlah)
        {
            int k = 0;
            for (int i = 0; i < jumlah; i++)
            {
                for (int j = 0; j < jumlah; j++)
                {
                    matriks_convert[k] = matriks[i, j];
                    k++;
                }
            }
        }

        private static void urutParalel(int[] matriks, int jumlah)
        {
            int prosessorCount = 2;
            int barisPerCore = jumlah / prosessorCount;
            Task[] tasks = new Task[prosessorCount];

            for (int x = 0; x < tasks.Length; x++)
            {
                int beginIndex = x * jumlah * jumlah / 2;
                int jumlah_elemen = (jumlah * jumlah / 2) - jumlah;

                if (jumlah % 2 != 0)
                {
                    beginIndex = x * jumlah * barisPerCore;
                    jumlah_elemen = jumlah * (barisPerCore - 1);
                }

                if (x == prosessorCount - 1)
                {
                    jumlah_elemen = (jumlah * jumlah) - jumlah;
                }

                //int counter = 0;
                tasks[x] = new Task(() =>
                {
                    for (int i = beginIndex; i <= jumlah_elemen; i = i + jumlah)
                    {
                        
                        int r = i + jumlah - 1;
                        mergeSort(matriks, i, r);
                        //counter++;
                    }
                    //Console.WriteLine("Counter Pararel = {1}", x , counter);
                });

                tasks[x].Start();
            }

            Task.WaitAll(tasks);
        }

        private static void urutNonParalel(int[] matriks, int jumlah)
        {
            int r = jumlah - 1;
            //int counter = 0;
            for (int i = 0; i <= jumlah * (jumlah - 1); i = i + jumlah)
            {
                mergeSort(matriks, i, r);
                r = r + jumlah;
                //counter++;
            }
            //Console.WriteLine("Counter Non Pararel = {0}", counter);
        }


        static public void merge(int[] arr, int p, int q, int r)
        {
            int i, j, k;
            int n1 = q - p + 1;
            int n2 = r - q;
            int[] L = new int[n1];
            int[] R = new int[n2];

            for (i = 0; i < n1; i++)
            {
                L[i] = arr[p + i];
            }
            for (j = 0; j < n2; j++)
            {
                R[j] = arr[q + 1 + j];
            }

            i = 0;
            j = 0;
            k = p;
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                {
                    arr[k] = L[i];
                    i++;
                }
                else
                {
                    arr[k] = R[j];
                    j++;
                }
                k++;
            }
            while (i < n1)
            {
                arr[k] = L[i];
                i++;
                k++;
            }
            while (j < n2)
            {
                arr[k] = R[j];
                j++;
                k++;
            }
        }

        static public void mergeSort(int[] arr, int p, int r)
        {
            if (p < r)
            {
                int q = (p + r) / 2;
                mergeSort(arr, p, q);
                mergeSort(arr, q + 1, r);
                merge(arr, p, q, r);
            }
        }

        private static void GenerateMatriksData(int[,] matriks)
        {
            Random rnd = new Random();
            for (int i = 0; i < matriks.GetLength(0); i++)
            {
                for (int j = 0; j < matriks.GetLength(1); j++)
                {
                    matriks[i, j] = rnd.Next(255);
                }
            }
        }
    }
}