﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitDemo
{
    class Program
    {
        public static async void AsynchronousOperation2()
        {
            Console.WriteLine("Inside AsynchronousOperation2 Before AsyncMethod2, Thread Id: " + Thread.CurrentThread.ManagedThreadId);
            int count = await AsyncMethod2();
            Console.WriteLine("Inside AsynchronousOperation2 After AsyncMethod2, Thread Id: " + Thread.CurrentThread.ManagedThreadId);
        }

        public static async Task<int> AsyncMethod2()
        {
            int count = 0;
            Console.WriteLine("Inside AsyncMethod2, Thread Id: " + Thread.CurrentThread.ManagedThreadId);
            await Task.Run(() =>
            {
                Console.WriteLine("Executing a long running task which takes 10 seconds to complete, Thread Id: " + Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(5000);
                count = 10;
            });

            Console.WriteLine("Inside AsyncMethod2 task completed, Thread Id: " + Thread.CurrentThread.ManagedThreadId);
            return count;
        }

        public static async void AsynchronousOperation()
        {
            Console.WriteLine("Inside AsynchronousOperation Before AsyncMethod, Thread Id: " + Thread.CurrentThread.ManagedThreadId);
            Task<int> _task = AsyncMethod();

            Console.WriteLine("Inside AsynchronousOperation After AsyncMethod, Thread Id: " + Thread.CurrentThread.ManagedThreadId);

            Method2();

            Console.WriteLine("Inside AsynchronousOperation After AsyncMethod Before Await, Thread Id: " + Thread.CurrentThread.ManagedThreadId);

            int count = await _task;

            Console.WriteLine("Inside AsynchronousOperation After AsyncMethod After Await Before DependentMethod, Thread Id: " + Thread.CurrentThread.ManagedThreadId);

            DependentMethod(count);

            Console.WriteLine("Inside AsynchronousOperation After AsyncMethod After Await After DependentMethod, Thread Id: " + Thread.CurrentThread.ManagedThreadId);
        }

        public static async Task<int> AsyncMethod()
        {
            Console.WriteLine("Inside AsyncMethod, Thread Id: " + Thread.CurrentThread.ManagedThreadId);
            int count = 0;

            await Task.Run(() =>
            {
                Console.WriteLine("Executing a long running task which takes 10 seconds to complete, Thread Id: " + Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(5000);
                count = 10;
            });

            Console.WriteLine("Completed AsyncMethod, Thread Id: " + Thread.CurrentThread.ManagedThreadId);

            return count;
        }

        public static void Method2()
        {
            Console.WriteLine("Inside Method2, Thread Id: " + Thread.CurrentThread.ManagedThreadId);
        }

        public static void DependentMethod(int count)
        {
            Console.WriteLine("Inside DependentMethod, Thread Id: " + Thread.CurrentThread.ManagedThreadId + ". Total count is " + count);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Started Main method, Thread Id: " + Thread.CurrentThread.ManagedThreadId);

            AsynchronousOperation();

            //AsynchronousOperation2();

            Console.WriteLine("Completed Main method, Thread Id: " + Thread.CurrentThread.ManagedThreadId);

            Console.ReadKey();
        }

        public static void SynchronousOperation()
        {
            SynchronousMethod();

            Method2();
        }

        public static void SynchronousMethod()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(500);
                Console.WriteLine("Synchronous Method");
            }
        }
    }
}
