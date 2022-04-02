using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Practice_5
{
    enum OS_ThreadStatus
    {
        None, Created, Ready, Running, Awaiting, Executed
    }
    class OS_Thread
    {
        public OS_ThreadStatus T_Status;
        public bool T_isSelected;

        public OS_Thread()
        {
            T_Status = OS_ThreadStatus.None;
            T_isSelected = false;
        }
    }
}
