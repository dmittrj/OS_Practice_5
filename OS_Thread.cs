using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OS_Practice_5
{
    enum OS_ThreadStatus
    {
        None, Created, Stopped, Running, Awaiting, Executed
    }
    enum OS_Task
    {
        Primes, Fermat, Factorials
    }
    class OS_Thread
    {
        private const int MAX_NUMBER = 500000;

        public OS_ThreadStatus T_Status;
        public OS_Task T_Task;
        public bool T_isSelected;
        public int T_Priority;
        public Thread T_Thread;
        public int T_Progress;
        public string T_Result;

        public OS_Thread(OS_Task task)
        {
            T_Status = OS_ThreadStatus.None;
            T_isSelected = false;
            T_Priority = 3;
            T_Task = task;
            T_Progress = 0;
            T_Result = "";
            T_Thread = new Thread(OS_PerformTask);
        }

        public void OS_Start()
        {
            T_Thread.Start();
        }

        private void OS_PerformTask()
        {
            switch (T_Task)
            {
                case OS_Task.Primes:
                    OS_PerformTask_Primes();
                    break;
                case OS_Task.Fermat:
                    OS_PerformTask_Fermat();
                    break;
                case OS_Task.Factorials:
                    OS_PerformTask_Factorials();
                    break;
                default:
                    break;
            }
        }

        private void OS_PerformTask_Primes()
        {
            List<int> primes = new List<int>();
            for (int i = 0; i < MAX_NUMBER; i++)
            {
                bool isPrime = true;
                for (int j = 2; j < i; j++)
                {
                    if (i % j == 0) {
                        if (T_Status == OS_ThreadStatus.Awaiting) return;
                        //T_Thread.Join();
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    primes.Add(i);
                }
                T_Progress = i * 100 / MAX_NUMBER;
            }
            T_Result = "найдено " + primes.Count.ToString() + " простых чисел";
            T_Status = OS_ThreadStatus.Stopped;
        }

        private void OS_PerformTask_Fermat()
        {
            bool isFermatTheoremTrue = true;
            for (int i = 1; i < 10000; i++)
            {
                for (int j = 1; j < 100; j++)
                {
                    for (int k = 0; k < 100000; k++)
                    {
                        if (i*i + j*j < k*k)
                        {
                            break;
                        }
                        if (i * i + j * j == k * k)
                        {
                            isFermatTheoremTrue = false;
                            break;
                        }
                    }
                }
                T_Progress = i * 100 / MAX_NUMBER;
            }
            if (isFermatTheoremTrue)
            {
                T_Result = "опровержения теоремы Ферма не найдено";
            } 
            else 
            {
                T_Result = "найдено опровержение теоремы Ферма";
            }
            T_Status = OS_ThreadStatus.Stopped;
        }

        private void OS_PerformTask_Factorials()
        {
            List<ulong> factorials = new List<ulong>();
            for (int i = 0; i < MAX_NUMBER; i++)
            {
                ulong mul = 1;
                for (int j = 1; j < i; j++)
                {
                    if (T_Status == OS_ThreadStatus.Awaiting) return;
                    mul *= (ulong)j;
                }
                factorials.Add(mul);
                T_Progress = i * 100 / MAX_NUMBER;
            }
            T_Result = "вычислены " + factorials.Count.ToString() + " факториалов";
            T_Status = OS_ThreadStatus.Stopped;
        }
    }
}
