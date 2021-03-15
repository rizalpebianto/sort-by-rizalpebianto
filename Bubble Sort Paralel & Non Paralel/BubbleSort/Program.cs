using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Kelompok 2

// Dian Ismiyanti(1706080002)
// Juliet R.A Lao      (1706080003)
// Yulia Dedo Ngara(1706080006)
// Puput Novita Sari(1706080007)
// Nur Ch.Sholichah(1706080008)
// Voni Djara(1706080009)
// Rizal Pebianto(1706080012)

namespace BubbleSort
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            int jumlahBaris = 500;
            int[,] matriksA = new int[jumlahBaris, jumlahBaris];
            int[,] matriksB = new int[jumlahBaris, jumlahBaris];
            GenerateMatriksData(matriksA);
            matriksB = matriksA;

            Console.WriteLine(" BUBBLE SORT");

            //Console.WriteLine("Matriks A : ");
            //CetakMatriks(matriksB);
            Console.WriteLine();

            for (int i = 0; i < 10; i++)
            {
                GenerateMatriksData(matriksA);
                matriksB = matriksA;
                sw.Reset();
                sw.Start();
                sort_nonParalel(matriksA);
                sw.Stop();
                Console.WriteLine(" Dengan Non Paralel Selesai dengan waktu : {0:N2} ms", sw.Elapsed.TotalMilliseconds);

                sw.Reset();

                sw.Start();
                sort_Paralel(matriksB);
                sw.Stop();
                Console.WriteLine(" Dengan Paralel Selesai dengan waktu : {0:N2} ms", sw.Elapsed.TotalMilliseconds);
                Console.WriteLine();
            }
            

            //Console.WriteLine();
            //CetakMatriks(matriksB);
            Console.ReadLine();
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

        private static void CetakMatriks(int[,] matriks)
        {
            string baris = string.Empty;
            for (int i = 0; i < matriks.GetLength(0); i++)
            {
                baris = string.Empty;
                for (int j = 0; j < matriks.GetLength(1); j++)
                {
                    baris += string.Format("{0, 4}", matriks[i, j]);
                }
                Console.WriteLine(baris);
            }
        }

        private static void sort_nonParalel(int[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = arr.GetLength(1) - 1; j > 0; j--)
                {

                    for (int k = 0; k < j; k++)
                    {
                        if (arr[i, k] > arr[i, k + 1])
                        {
                            int temp = arr[i, k];
                            arr[i, k] = arr[i, k + 1];
                            arr[i, k + 1] = temp;
                        }
                    }
                }
            }
        }

        private static void sort_Paralel(int[,] arr)
        {
            int jumlahBaris = arr.GetLength(0);
            int jumlahKolom = arr.GetLength(1);

            int prosessorCount = 2;
            int barisPerCore = jumlahBaris / prosessorCount;
            //Generate task sebanyak jumlah prosessor
            Task[] tasks = new Task[prosessorCount];

            for (int x = 0; x < tasks.Length; x++)
            {
                int beginIndex = x * barisPerCore;
                int jumlahBarisPerCore = barisPerCore;

                if (x == prosessorCount - 1)
                {
                    jumlahBarisPerCore = jumlahBaris - beginIndex;
                }

                //assign task dengan yang akan dikerjakan
                tasks[x] = new Task(() =>
                {
                    //int counter = 0;
                    for (int i = beginIndex; i < beginIndex + jumlahBarisPerCore; i++)
                    {
                        for (int j = arr.GetLength(1) - 1; j > 0; j--)
                        {

                            for (int k = 0; k < j; k++)
                            {
                                if (arr[i, k] > arr[i, k + 1])
                                {
                                    int temp = arr[i, k];
                                    arr[i, k] = arr[i, k + 1];
                                    arr[i, k + 1] = temp;
                                   // counter++;
                                }
                            }
                        }
                    }
                     //Console.WriteLine("Counter Pararel = {0}", counter);
                });

                tasks[x].Start();
            }

            Task.WaitAll(tasks);

        }
    }
}