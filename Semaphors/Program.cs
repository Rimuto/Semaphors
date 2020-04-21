using System;
using System.Threading.Tasks;
using System.Threading;
using System.Text.RegularExpressions;

namespace Semaphors
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //Semaphore DED = new Semaphore(4, 4);
            //for (int i = 1; i < 3; i++)
            //{
                Reader reader = new Reader();
           // }
            Console.ReadLine();
        }
    }

    class Reader
    {
        // создаем семафор
        static Semaphore Ded = new Semaphore(1, 1);
        //static Semaphore Alaska = new Semaphore(0, 5);
        Thread myThread;
       // int count = 1;// счетчик снегурочег

        public Reader(/*int i*/)
        {
            myThread = new Thread(Lodar);
            myThread.Name = $"Снегурки";
            myThread.Start();
            myThread = new Thread(Snegovicheg);
            myThread.Name = $"Снеговичог";
            myThread.Start();
        }

        public void Lodar()
        {
            Random rand = new Random();
            Task[] tasks1 = new Task[5]
            {
              new Task(() => {Thread.Sleep(rand.Next(1000, 5000)); Console.WriteLine("Первая отдыхает на Аляске"); }),//не, ну а че, живет же на северном полюсе как-то.
              new Task(() =>{Thread.Sleep(rand.Next(1000, 5000)); Console.WriteLine("Вторая отдыхает на Аляске"); }),// В крыме для нее по-любому слишком жарко, а Аляска ниче так, тепленько и золото можно подобывать
              new Task(() => {Thread.Sleep(rand.Next(1000, 5000));  Console.WriteLine("Третья отдыхает на Аляске"); }),
              new Task(() =>{Thread.Sleep(rand.Next(1000, 5000)); Console.WriteLine("Четвертая отдыхает на Аляске"); }),
              new Task(() => {Thread.Sleep(rand.Next(1000, 5000));  Console.WriteLine("Пятая отдыхает на Аляске"); })
            };
            foreach (var t in tasks1)
                t.Start();
            Task.WaitAll(tasks1); // ожидаем завершения задач 
            Ded.Release();
            Ded.WaitOne();// Как только вернулась последняя, она будит ДЕДА
            Console.WriteLine($"ДЕД разбужен и поздравляет мелких");
            Thread.Sleep(5000);
            Ded.Release();
        }

        public void Snegovicheg()
        {
            int sum = 0;
            var results = new int[5];
            Random rand = new Random();
            Task[] tasks1 = new Task[5]
            {
              new Task<int>(() => {Thread.Sleep(50); Console.WriteLine("Снеговик-1 работает"); if (rand.Next(1000,5000)>2500){ results[0] = 1; return 1; } results[0] = 0; return 0; }),//не, ну а че, живет же на северном полюсе как-то.
              new Task<int>(() => {Thread.Sleep(50); Console.WriteLine("Снеговик-2 работает"); if (rand.Next(1000,5000)>2500){ results[1] = 1;return 1; } results[1] = 0;return 0;}),// В крыме для нее по-любому слишком жарко, а Аляска ниче так, тепленько и золото можно подобывать
              new Task<int>(() => {Thread.Sleep(50); Console.WriteLine("Снеговик-3 работает"); if (rand.Next(1000,5000)>2500){ results[2] = 1;return 1; } results[2] = 0;return 0;}),
              new Task<int>(() => {Thread.Sleep(50); Console.WriteLine("Снеговик-4 работает"); if (rand.Next(1000,5000)>2500){ results[3] = 1;return 1; } results[3] = 0;return 0;}),
              new Task<int>(() => {Thread.Sleep(50); Console.WriteLine("Снеговик-5 работает"); if (rand.Next(1000,5000)>2500){ results[4] = 1;return 1; } results[4] = 0;return 0;})
            };
            foreach (var t in tasks1)
                t.Start();
            Task.WaitAll(tasks1);
            foreach(int u in results)
            {
                sum += u;
            }
            if (sum >= 3)
            {
                Ded.WaitOne();
                Console.WriteLine($"ДЕД разбужен и разбирается с проблемами");
                Thread.Sleep(5000);
                Ded.Release();
            }
            sum = 0;
            for (int j = 0; j < 5; j++)
            {
                results[j] = 0;
            }


        }
    }
}
