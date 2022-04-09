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
        None, Created, Stopped, Running, Awaiting, Executed, Blocked
    }
    enum OS_Task
    {
        Primes, Fermat, Factorials
    }
    struct OS_Context
    {
        public int i;
        public int j;
        public int k;
        public bool isPrime;
        public bool isFermatTheoremTrue;
        public List<int> primes;
        public bool active;
        public OS_Context(OS_Task task)
        {
            switch (task)
            {
                case OS_Task.Primes:
                    i = 0;
                    j = 2;
                    break;
                case OS_Task.Fermat:
                    i = 7;
                    j = 0;
                    break;
                case OS_Task.Factorials:
                    i = 0;
                    j = 0;
                    break;
                default:
                    i = 0;
                    j = 0;
                    break;
            }
            k = 0;
            isPrime = false;
            isFermatTheoremTrue = true;
            primes = new List<int>();
            active = false;
        }
    }
    class OS_Thread
    {
        private const int MAX_NUMBER = 500000;
        private const int MAX_NUMBER_FERMAT = 110;

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
        public OS_Context T_Context;

        public OS_Thread(OS_Task task)
        {
            T_Status = OS_ThreadStatus.None;
            T_isSelected = false;
            T_Priority = 3;
            T_Task = task;
            T_Context = new(task);
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

        private void OS_Interrupt(int i, int j, bool isPrime, List<int> primes)
        {
            //T_Context.task = OS_Task.Primes;
            T_Context.i = i;
            T_Context.j = j;
            T_Context.isPrime = isPrime;
            T_Context.primes = primes;
            T_Context.active = true;
        }

        private void OS_Interrupt(int i, int j, int k, bool isFermatTheoremTrue)
        {
            T_Context.i = i;
            T_Context.j = j;
            T_Context.k = k;
            T_Context.isFermatTheoremTrue = isFermatTheoremTrue;
            T_Context.primes = new List<int>();
            T_Context.primes.Add(1111);
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
            List<int> primes = T_Context.primes;
            for (int i = T_Context.i; i < MAX_NUMBER; i++)
            {
                T_Context.i = 0;
                bool isPrime = T_Context.isPrime;
                for (int j = T_Context.j; j < i; j++)
                {
                    T_Context.isPrime = true;
                    T_Context.j = 2;
                    if (DateTime.Now.Subtract(StartTime).TotalMilliseconds > Quantum)
                    {
                        T_Status = OS_ThreadStatus.Awaiting;
                        OS_Interrupt(i, j, isPrime, primes);
                        //T_Thread.Join();
                        return;
                    };
                    if (i % j == 0) {
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
            bool isFermatTheoremTrue = T_Context.isFermatTheoremTrue;
            if (T_Context.i == 1)
            {
                int yyyy = 7;
            }
            for (int i = T_Context.i; i < MAX_NUMBER_FERMAT; i++)
            {
                T_Context.i = 1;
                for (int j = T_Context.j; j < MAX_NUMBER_FERMAT; j++)
                {
                    T_Context.j = 1;
                    for (int k = T_Context.k; k < MAX_NUMBER_FERMAT * MAX_NUMBER_FERMAT; k++)
                    {
                        T_Context.k = 0;
                        if (DateTime.Now.Subtract(StartTime).TotalMilliseconds > Quantum)
                        {
                            //T_Thread.Interrupt();
                            OS_Interrupt(i, j, k, isFermatTheoremTrue);
                            //T_Thread = new Thread(OS_PerformTask);
                            T_Status = OS_ThreadStatus.Awaiting;
                            return;
                        }
                        if (i * i * i + j * j * j == k * k * k)
                        {
                            break;
                        }
                        if (i * i * i + j * j * j == k * k * k)
                        {
                            isFermatTheoremTrue = false;
                            break;
                        }
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
