using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OS_Practice_5
{
    class OS_ProcessManager
    {
        private readonly Thread M_Thread;
        int Quantum;
        int PrioritySum;

        public OS_ProcessManager()
        {
            Quantum = 1000;
            PrioritySum = 1;
            M_Thread = new Thread(OS_Manager);
            M_Thread.Start();
        }

        private async void OS_Manager()
        {
            while (true)
            {
                Quantum = 1000;
                PrioritySum = 0;
                foreach (OS_Thread item in Program.OS_Threads)
                {
                    if (item.T_Status == OS_ThreadStatus.Awaiting)
                    {
                        Quantum += 500 * item.T_Priority + 1000;
                        PrioritySum += item.T_Priority;
                    }
                }
                for (int i = 0; i < 3; i++)
                {
                    if (Program.OS_Threads[i].T_Status == OS_ThreadStatus.Awaiting)
                    {
                        await Program.OS_Threads[i].OS_Start(Quantum / PrioritySum * Program.OS_Threads[i].T_Priority);
                        Program.OS_PrintTable();
                    };
                }
                await Task.Delay(1);
            }
        }
    }
}
