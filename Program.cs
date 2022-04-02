using System;

namespace OS_Practice_5
{
    class Program
    {
        public static int OS_CurrentCursorPosition = 0;
        public static OS_Thread[] OS_Threads = new OS_Thread[3];
        public static void OS_DrawTableLine(int position)
        {
            if (OS_CurrentCursorPosition == position)
            {
                Console.Write(" >");
            } 
            else
            {
                Console.Write("  ");
            }
            if (OS_Threads[position].T_isSelected)
            {
                Console.Write(" [*]");
            }
            else
            {
                Console.Write(" [ ]");
            }
            switch (OS_Threads[position].T_Status)
            {
                case OS_ThreadStatus.None:
                    Console.Write(" Не запущен");
                    break;
                case OS_ThreadStatus.Created:
                    break;
                case OS_ThreadStatus.Ready:
                    break;
                case OS_ThreadStatus.Running:
                    break;
                case OS_ThreadStatus.Awaiting:
                    break;
                case OS_ThreadStatus.Executed:
                    break;
                default:
                    break;
            }
            Console.WriteLine();
        }
        public static void OS_PrintTable()
        {
            Console.Clear();
            Console.Write("\n Потоки:\n");
            for (int i = 0; i < 3; i++)
            {
                OS_DrawTableLine(i);
            }
        }

        static void Main(string[] args)
        {
            for (int i = 0; i < OS_Threads.Length; i++)
            {
                OS_Threads[i] = new OS_Thread();
            }
            OS_PrintTable();
        }
    }
}
