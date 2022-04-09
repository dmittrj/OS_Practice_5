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
        private const int MAX_NUMBER_FERMAT = 300;

        public OS_ThreadStatus T_Status;
        public OS_Task T_Task;
        public bool T_isSelected;
        public int T_Priority;
        public Thread T_Thread;
        public int T_Progress;
        private int Quantum;
        private DateTime StartTime;
        //private DateTime EndTime;
        public string T_Result;

        public OS_Thread(OS_Task task)
        {
            T_Status = OS_ThreadStatus.None;
            T_isSelected = false;
            T_Priority = 3;
            T_Task = task;
            T_Progress = 0;
            T_Result = "";
            Quantum = 0;
            T_Thread = new Thread(OS_PerformTask);
        }

        public async Task<Task> OS_Start(int quantum)
        {
            Quantum = quantum;
            T_Status = OS_ThreadStatus.Running;
            Program.OS_PrintTable();
            StartTime = DateTime.Now;
            T_Thread = new Thread(OS_PerformTask);
            T_Thread.Start();
            while (T_Status == OS_ThreadStatus.Running);
            return new Task(OS_PerformTask);
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
            return;
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
                        //T_Thread.Join();
                        isPrime = false;
                        break;
                    }
                    if (T_Status == OS_ThreadStatus.Awaiting) return;
                    if (DateTime.Now.Subtract(StartTime).TotalMilliseconds > Quantum)
                    {
                        T_Status = OS_ThreadStatus.Awaiting;
                        T_Thread.Join();
                        return;
                    };
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
            for (int i = 1; i < MAX_NUMBER_FERMAT; i++)
            {
                for (int j = 1; j < MAX_NUMBER_FERMAT; j++)
                {
                    for (int k = 0; k < MAX_NUMBER_FERMAT * MAX_NUMBER_FERMAT; k++)
                    {
                        if (i * i * i + j * j * j == k * k * k)
                        {
                            break;
                        }
                        if (i * i * i + j * j * j == k * k * k)
                        {
                            isFermatTheoremTrue = false;
                            break;
                        }
                        if (T_Status == OS_ThreadStatus.Awaiting) return;
                        if (DateTime.Now.Subtract(StartTime).TotalMilliseconds > Quantum)
                        {
                            T_Status = OS_ThreadStatus.Awaiting;
                            return;
                        };
                    }
                }
                T_Progress = i * 100 / MAX_NUMBER_FERMAT;
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
                if (T_Status == OS_ThreadStatus.Awaiting) return;
                if (DateTime.Now.Subtract(StartTime).TotalMilliseconds > Quantum)
                {
                    T_Status = OS_ThreadStatus.Awaiting;
                    return;
                };
            }
            T_Result = "вычислены " + factorials.Count.ToString() + " факториалов";
            T_Status = OS_ThreadStatus.Stopped;
        }
    }
}
