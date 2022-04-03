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
        None, Created, Ready, Running, Awaiting, Executed
    }
    enum OS_Task
    {
        Primes, Summation, Factorials
    }
    class OS_Thread
    {
        private const int MAX_NUMBER = 200000;

        public OS_ThreadStatus T_Status;
        public OS_Task T_Task;
        public bool T_isSelected;
        public int T_Priority;
        public Thread T_Thread;

        public OS_Thread(OS_Task task)
        {
            T_Status = OS_ThreadStatus.None;
            T_isSelected = false;
            T_Priority = 1;
            T_Task = task;
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
                case OS_Task.Summation:
                    OS_PerformTask_Summation();
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
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    primes.Add(i);
                }
            }
            foreach (int item in primes)
            {
                Console.Write(item.ToString() + ", ");
            }
        }

        private void OS_PerformTask_Summation()
        {
            ulong sum = 0;
            for (int i = 0; i < MAX_NUMBER; i++)
            {
                if (i % 2 == 1 && i % 10 != 0)
                    sum += (ulong)i * (ulong)i;
                else
                    sum += (ulong)i;
            }
            Console.WriteLine(sum);
        }

        private void OS_PerformTask_Factorials()
        {

        }
    }
}
