using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Задача
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Func<Task<int[]>, int[]>func2 = new Func<Task<int[]>, int[]> (ArraySum);
            Task<int[]> task2 = task1.ContinueWith<int[]>(func2);
            
            Action<Task<int[]>> action = new Action<Task<int[]>>(MaxArray);
            Task task3 = task2.ContinueWith(action);
            task1.Start();
            Console.ReadKey();
        }
        static int[] GetArray(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random random = new Random();
            Console.Write("Массив: ");
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0, 100);
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine();
            return array;
        }
        static int[] ArraySum(Task<int[]> task)
        {
            int[] array = task.Result;
            int sum = 0;
            for (int i = 0;i < array.Length;i++)
            {
                sum += array[i];
            }
            Console.WriteLine("Сумма: {0}", sum);
            return array;
        }
        static void MaxArray(Task<int[]> task)
        {
            int[] array = task.Result;
            int max = 0;
            for (int i = 0;i < array.Length; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }
            }
            Console.WriteLine("Максимальное число: {0}", max);
        }
    }
}
