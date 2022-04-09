using System;
using System.Threading.Tasks;

namespace OS_Practice_5
{
    class Program
    {
        public static int OS_CurrentCursorPosition = 0;
        public static OS_Thread[] OS_Threads = new OS_Thread[3];
        public static int OS_Wheel = 0;
        public static bool OS_WheelInterrupt = false;
        public static int[] cur_lefts = new int[3];
        public static int[] cur_tops = new int[3];

        public static void OS_DrawTableLine(int position)
        {
            if (OS_CurrentCursorPosition == position)
            {
                Console.Write("│>");
            }
            else
            {
                Console.Write("│ ");
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
                    Console.Write(" Не создан                                                 │");
                    break;
                case OS_ThreadStatus.Created:
                    switch (OS_Threads[position].T_Task)
                    {
                        case OS_Task.Primes:
                            Console.Write(" Поток 1: поиск простых чисел    ");
                            break;
                        case OS_Task.Fermat:
                            Console.Write(" Поток 2: док-во теоремы Ферма   ");
                            break;
                        case OS_Task.Factorials:
                            Console.Write(" Поток 3: нахождение факториалов ");
                            break;
                        default:
                            break;
                    }
                    for (int i = 0; i < OS_Threads[position].T_Priority; i++)
                    {
                        Console.Write("/");
                    }
                    for (int i = 0; i < 10 - OS_Threads[position].T_Priority; i++)
                    {
                        Console.Write(" ");
                    }
                    Console.Write("  Создан        │");
                    break;
                case OS_ThreadStatus.Stopped:
                    OS_Threads[position].T_Status = OS_ThreadStatus.Executed;
                    break;
                case OS_ThreadStatus.Running:
                    switch (OS_Threads[position].T_Task)
                    {
                        case OS_Task.Primes:
                            Console.Write(" Поток 1: поиск простых чисел    ");
                            break;
                        case OS_Task.Fermat:
                            Console.Write(" Поток 2: док-во теоремы Ферма   ");
                            break;
                        case OS_Task.Factorials:
                            Console.Write(" Поток 3: нахождение факториалов ");
                            break;
                        default:
                            break;
                    }
                    for (int i = 0; i < OS_Threads[position].T_Priority; i++)
                    {
                        Console.Write("/");
                    }
                    for (int i = 0; i < 10 - OS_Threads[position].T_Priority; i++)
                    {
                        Console.Write(" ");
                    }
                    Console.Write("  Запущен ");
                    (cur_lefts[position], cur_tops[position]) = Console.GetCursorPosition();
                    switch (OS_Wheel)
                    {
                        case 0:
                            Console.Write("/");
                            break;
                        case 1:
                            Console.Write("-");
                            break;
                        case 2:
                            Console.Write("\\");
                            break;
                        case 3:
                            Console.Write("|");
                            break;
                        default:
                            break;
                    }
                    Console.Write(" " + OS_Threads[position].T_Progress.ToString() + "% ");
                    if (OS_Threads[position].T_Progress < 10) Console.Write(" ");
                    Console.Write("│");
                    break;
                case OS_ThreadStatus.Awaiting:
                    switch (OS_Threads[position].T_Task)
                    {
                        case OS_Task.Primes:
                            Console.Write(" Поток 1: поиск простых чисел    ");
                            break;
                        case OS_Task.Fermat:
                            Console.Write(" Поток 2: док-во теоремы Ферма   ");
                            break;
                        case OS_Task.Factorials:
                            Console.Write(" Поток 3: нахождение факториалов ");
                            break;
                        default:
                            break;
                    }
                    for (int i = 0; i < OS_Threads[position].T_Priority; i++)
                    {
                        Console.Write("/");
                    }
                    for (int i = 0; i < 10 - OS_Threads[position].T_Priority; i++)
                    {
                        Console.Write(" ");
                    }
                    Console.Write("  Ожидает  ");
                    Console.Write(" " + OS_Threads[position].T_Progress.ToString() + "% ");
                    if (OS_Threads[position].T_Progress < 10) Console.Write(" ");
                    Console.Write("│");
                    break;
                case OS_ThreadStatus.Executed:
                    switch (OS_Threads[position].T_Task)
                    {
                        case OS_Task.Primes:
                            Console.Write(" Поток 1: поиск простых чисел    ");
                            break;
                        case OS_Task.Fermat:
                            Console.Write(" Поток 2: док-во теоремы Ферма   ");
                            break;
                        case OS_Task.Factorials:
                            Console.Write(" Поток 3: нахождение факториалов ");
                            break;
                        default:
                            break;
                    }
                    for (int i = 0; i < OS_Threads[position].T_Priority; i++)
                    {
                        Console.Write("/");
                    }
                    for (int i = 0; i < 10 - OS_Threads[position].T_Priority; i++)
                    {
                        Console.Write(" ");
                    }
                    Console.Write("  Выполнен      │");
                    break;
                case OS_ThreadStatus.Blocked:
                    switch (OS_Threads[position].T_Task)
                    {
                        case OS_Task.Primes:
                            Console.Write(" Поток 1: поиск простых чисел    ");
                            break;
                        case OS_Task.Fermat:
                            Console.Write(" Поток 2: док-во теоремы Ферма   ");
                            break;
                        case OS_Task.Factorials:
                            Console.Write(" Поток 3: нахождение факториалов ");
                            break;
                        default:
                            break;
                    }
                    for (int i = 0; i < OS_Threads[position].T_Priority; i++)
                    {
                        Console.Write("/");
                    }
                    for (int i = 0; i < 10 - OS_Threads[position].T_Priority; i++)
                    {
                        Console.Write(" ");
                    }
                    Console.Write("  Заблокирован  │");
                    break;
                default:
                    break;
            }
            Console.WriteLine();
        }

        public static async void OS_Spinning()
        {
            while (true)
            {
                for (int i = 0; i < OS_Threads.Length; i++)
                {
                    switch (OS_Threads[i].T_Status)
                    {
                        case OS_ThreadStatus.None:
                            break;
                        case OS_ThreadStatus.Created:
                            break;
                        case OS_ThreadStatus.Stopped:
                            OS_Threads[i].T_Status = OS_ThreadStatus.Executed;
                            OS_PrintTable();
                            break;
                        case OS_ThreadStatus.Running:
                            if (OS_WheelInterrupt)
                                break;
                            Console.SetCursorPosition(cur_lefts[i], cur_tops[i]);
                            switch (OS_Wheel)
                            {
                                case 0:
                                    Console.Write("/");
                                    break;
                                case 1:
                                    Console.Write("-");
                                    break;
                                case 2:
                                    Console.Write("\\");
                                    break;
                                case 3:
                                    Console.Write("|");
                                    break;
                                default:
                                    break;
                            }
                            Console.Write(" " + OS_Threads[i].T_Progress.ToString() + "% ");
                            if (OS_Threads[i].T_Progress < 10) Console.Write(" ");
                            Console.Write("│");
                            break;
                        case OS_ThreadStatus.Awaiting:
                            break;
                        case OS_ThreadStatus.Executed:
                            break;
                        default:
                            break;
                    }

                }
                OS_Wheel++;
                OS_Wheel %= 4;
                await Task.Delay(300);
            }
        }


        public static void OS_PrintTable()
        {
            if (OS_WheelInterrupt) return;
            OS_WheelInterrupt = true;
            Console.Clear();
            Console.Write("\n┌───┬─ Потоки ──────────────────────── Приоритет ─ Состояние ────┐\n");
            for (int i = 0; i < 3; i++)
            {
                OS_DrawTableLine(i);
            }
            Console.WriteLine("├───┴────────────────────────────────────────────────────────────┤");
            OS_Wheel++;
            OS_Wheel %= 4;
            switch (OS_Threads[OS_CurrentCursorPosition].T_Status)
            {
                case OS_ThreadStatus.None:
                    Console.WriteLine("│ Space - выбрать                                                │");
                    Console.WriteLine("│     C - создать                                                │");
                    Console.WriteLine("└────────────────────────────────────────────────────────────────┘");
                    break;
                case OS_ThreadStatus.Created:
                    Console.WriteLine("│ Space - выбрать                                                │");
                    Console.WriteLine("│ Enter - запустить        +/- - изменить приоритет              │");
                    Console.WriteLine("└────────────────────────────────────────────────────────────────┘");
                    break;
                case OS_ThreadStatus.Stopped:
                    Console.WriteLine("│ Space - выбрать                                                │");
                    Console.WriteLine("│     D - удалить                                                │");
                    Console.WriteLine("├────────────────────────────────────────────────────────────────┤");
                    Console.Write("│ Результат: " + OS_Threads[OS_CurrentCursorPosition].T_Result);
                    for (int i = 0; i < 52 - OS_Threads[OS_CurrentCursorPosition].T_Result.Length; i++)
                    {
                        Console.Write(" ");
                    }
                    Console.WriteLine("│");
                    Console.WriteLine("└────────────────────────────────────────────────────────────────┘");
                    break;
                case OS_ThreadStatus.Running:
                    Console.WriteLine("│ Space - выбрать                                                │");
                    Console.WriteLine("│     B - заблокировать    +/- - изменить приоритет              │");
                    Console.WriteLine("└────────────────────────────────────────────────────────────────┘");
                    break;
                case OS_ThreadStatus.Awaiting:
                    Console.WriteLine("│ Space - выбрать                                                │");
                    Console.WriteLine("│     B - заблокировать   +/- - изменить приоритет               │");
                    Console.WriteLine("└────────────────────────────────────────────────────────────────┘");
                    break;
                case OS_ThreadStatus.Executed:
                    Console.WriteLine("│ Space - выбрать                                                │");
                    Console.WriteLine("│     D - удалить                                                │");
                    Console.WriteLine("├────────────────────────────────────────────────────────────────┤");
                    Console.Write("│ Результат: " + OS_Threads[OS_CurrentCursorPosition].T_Result);
                    for (int i = 0; i < 52 - OS_Threads[OS_CurrentCursorPosition].T_Result.Length; i++)
                    {
                        Console.Write(" ");
                    }
                    Console.WriteLine("│");
                    Console.WriteLine("└────────────────────────────────────────────────────────────────┘");
                    break;
                case OS_ThreadStatus.Blocked:
                    Console.WriteLine("│ Space - выбрать                                                │");
                    Console.WriteLine("│     B - разблокировать    +/- - изменить приоритет             │");
                    Console.WriteLine("└────────────────────────────────────────────────────────────────┘");
                    break;
                default:
                    break;
            }
            Console.WriteLine();
            OS_WheelInterrupt = false;
        }

        static void Main()
        {
            Console.CursorVisible = false;
            for (int i = 0; i < OS_Threads.Length; i++)
            {
                OS_Threads[i] = new OS_Thread((OS_Task)i);
            }
            OS_Spinning();
            _ = new OS_ProcessManager();
            while (true)
            {
                OS_PrintTable();
                ConsoleKey key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.Enter:
                        if (OS_Threads[OS_CurrentCursorPosition].T_Status != OS_ThreadStatus.None)
                        {
                            OS_Threads[OS_CurrentCursorPosition].T_Status = OS_ThreadStatus.Awaiting;
                            //OS_Threads[OS_CurrentCursorPosition].OS_Start();
                        }
                        break;
                    case ConsoleKey.Spacebar:
                        OS_Threads[OS_CurrentCursorPosition].T_isSelected = !OS_Threads[OS_CurrentCursorPosition].T_isSelected;
                        break;
                    case ConsoleKey.UpArrow:
                        OS_CurrentCursorPosition--;
                        if (OS_CurrentCursorPosition < 0)
                            OS_CurrentCursorPosition = 0;
                        break;
                    case ConsoleKey.DownArrow:
                        OS_CurrentCursorPosition++;
                        OS_CurrentCursorPosition %= 3;
                        break;
                    case ConsoleKey.B:
                        if (OS_Threads[OS_CurrentCursorPosition].T_Status == OS_ThreadStatus.Running)
                        {
                            OS_Threads[OS_CurrentCursorPosition].T_Status = OS_ThreadStatus.Blocked;
                        }
                        else if (OS_Threads[OS_CurrentCursorPosition].T_Status == OS_ThreadStatus.Awaiting)
                        {
                            OS_Threads[OS_CurrentCursorPosition].T_Status = OS_ThreadStatus.Blocked;
                        }
                        else if (OS_Threads[OS_CurrentCursorPosition].T_Status == OS_ThreadStatus.Blocked)
                        {
                            OS_Threads[OS_CurrentCursorPosition].T_Status = OS_ThreadStatus.Awaiting;
                        }
                        break;
                    case ConsoleKey.C:
                        if (OS_Threads[OS_CurrentCursorPosition].T_Status == OS_ThreadStatus.None)
                        {
                            OS_Threads[OS_CurrentCursorPosition].T_Status = OS_ThreadStatus.Created;
                        }
                        break;
                    case ConsoleKey.D:
                        if (OS_Threads[OS_CurrentCursorPosition].T_Status == OS_ThreadStatus.Executed)
                        {
                            OS_Threads[OS_CurrentCursorPosition].T_Status = OS_ThreadStatus.None;
                        }
                        break;
                    case ConsoleKey.OemPlus:
                        if (OS_Threads[OS_CurrentCursorPosition].T_Status != OS_ThreadStatus.None)
                        {
                            OS_Threads[OS_CurrentCursorPosition].T_Priority++;
                            if (OS_Threads[OS_CurrentCursorPosition].T_Priority > 5)
                                OS_Threads[OS_CurrentCursorPosition].T_Priority = 5;
                            //OS_Threads[OS_CurrentCursorPosition].T_Thread.Priority = (System.Threading.ThreadPriority)(OS_Threads[OS_CurrentCursorPosition].T_Priority - 1);
                        }
                        break;
                    case ConsoleKey.OemMinus:
                        if (OS_Threads[OS_CurrentCursorPosition].T_Status != OS_ThreadStatus.None)
                        {
                            OS_Threads[OS_CurrentCursorPosition].T_Priority--;
                            if (OS_Threads[OS_CurrentCursorPosition].T_Priority < 1)
                                OS_Threads[OS_CurrentCursorPosition].T_Priority = 1;
                        }
                        break;
                }
            }
        }
    }
}