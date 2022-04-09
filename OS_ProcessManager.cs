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
        private Thread M_Thread;

        public OS_ProcessManager()
        {
            M_Thread = new Thread(OS_Manager);
            M_Thread.Start();
        }

        private async void OS_Manager()
        {
            while (true)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Program.OS_Threads[i].T_Status != OS_ThreadStatus.None &&
                        Program.OS_Threads[i].T_Status != OS_ThreadStatus.Created)
                    {
                        await Program.OS_Threads[i].OS_Start(Program.OS_Threads[i].T_Priority * 1000);
                        Program.OS_PrintTable();
                    };
                }
                //await Task.Delay(1);
            }
        }
    }
}
