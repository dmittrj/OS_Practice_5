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
                    break;
                case OS_ThreadStatus.Running:
                    Console.WriteLine("│ Space - выбрать                                                │");
                    Console.WriteLine("│     B - заблокировать    +/- - изменить приоритет              │");
                    Console.WriteLine("└────────────────────────────────────────────────────────────────┘");
                    break;
                case OS_ThreadStatus.Awaiting:
                    Console.WriteLine("│ Space - выбрать                                                │");
                    Console.WriteLine("│     B - разблокировать   +/- - изменить приоритет              │");
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
                default:
                    break;
            }
            Console.WriteLine();
            Console.WriteLine(OS_Threads[1].T_Context.i + " || " + OS_Threads[1].T_Context.j + " || " + OS_Threads[1].T_Context.k);
            OS_WheelInterrupt = false;
        }

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            for (int i = 0; i < OS_Threads.Length; i++)
            {
                OS_Threads[i] = new OS_Thread((OS_Task)i);
            }
            OS_Spinning();
            OS_ProcessManager processManager = new();
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
                    case ConsoleKey.Pause:
                        break;
                    case ConsoleKey.Escape:
                        break;
                    case ConsoleKey.Spacebar:
                        OS_Threads[OS_CurrentCursorPosition].T_isSelected = !OS_Threads[OS_CurrentCursorPosition].T_isSelected;
                        break;
                    case ConsoleKey.PageUp:
                        break;
                    case ConsoleKey.PageDown:
                        break;
                    case ConsoleKey.End:
                        break;
                    case ConsoleKey.Home:
                        break;
                    case ConsoleKey.LeftArrow:
                        break;
                    case ConsoleKey.UpArrow:
                        OS_CurrentCursorPosition--;
                        if (OS_CurrentCursorPosition < 0)
                            OS_CurrentCursorPosition = 0;
                        break;
                    case ConsoleKey.RightArrow:
                        break;
                    case ConsoleKey.DownArrow:
                        OS_CurrentCursorPosition++;
                        OS_CurrentCursorPosition %= 3;
                        break;
                    case ConsoleKey.Select:
                        break;
                    case ConsoleKey.Print:
                        break;
                    case ConsoleKey.Execute:
                        break;
                    case ConsoleKey.PrintScreen:
                        break;
                    case ConsoleKey.Insert:
                        break;
                    case ConsoleKey.Delete:
                        break;
                    case ConsoleKey.D0:
                        break;
                    case ConsoleKey.D1:
                        break;
                    case ConsoleKey.D2:
                        break;
                    case ConsoleKey.D3:
                        break;
                    case ConsoleKey.D4:
                        break;
                    case ConsoleKey.D5:
                        break;
                    case ConsoleKey.D6:
                        break;
                    case ConsoleKey.D7:
                        break;
                    case ConsoleKey.D8:
                        break;
                    case ConsoleKey.D9:
                        break;
                    case ConsoleKey.A:
                        break;
                    case ConsoleKey.B:
                        if (OS_Threads[OS_CurrentCursorPosition].T_Status == OS_ThreadStatus.Running)
                        {
                            OS_Threads[OS_CurrentCursorPosition].T_Status = OS_ThreadStatus.Awaiting;
                        }
                        else if (OS_Threads[OS_CurrentCursorPosition].T_Status == OS_ThreadStatus.Awaiting)
                        {
                            OS_Threads[OS_CurrentCursorPosition].T_Status = OS_ThreadStatus.Running;
                        }
                        break;
                    case ConsoleKey.C:
                        if (OS_Threads[OS_CurrentCursorPosition].T_Status == OS_ThreadStatus.None)
                        {
                            OS_Threads[OS_CurrentCursorPosition].T_Status = OS_ThreadStatus.Created;
                        }
                        break;
                    case ConsoleKey.D:
                        break;
                    case ConsoleKey.E:
                        break;
                    case ConsoleKey.F:
                        break;
                    case ConsoleKey.G:
                        break;
                    case ConsoleKey.H:
                        break;
                    case ConsoleKey.I:
                        break;
                    case ConsoleKey.J:
                        break;
                    case ConsoleKey.K:
                        break;
                    case ConsoleKey.L:
                        break;
                    case ConsoleKey.M:
                        break;
                    case ConsoleKey.N:
                        break;
                    case ConsoleKey.O:
                        break;
                    case ConsoleKey.P:
                        break;
                    case ConsoleKey.Q:
                        break;
                    case ConsoleKey.R:
                        break;
                    case ConsoleKey.S:
                        break;
                    case ConsoleKey.T:
                        break;
                    case ConsoleKey.U:
                        break;
                    case ConsoleKey.V:
                        break;
                    case ConsoleKey.W:
                        break;
                    case ConsoleKey.X:
                        break;
                    case ConsoleKey.Y:
                        break;
                    case ConsoleKey.Z:
                        break;
                    case ConsoleKey.LeftWindows:
                        break;
                    case ConsoleKey.RightWindows:
                        break;
                    case ConsoleKey.Applications:
                        break;
                    case ConsoleKey.Sleep:
                        break;
                    case ConsoleKey.NumPad0:
                        break;
                    case ConsoleKey.NumPad1:
                        break;
                    case ConsoleKey.NumPad2:
                        break;
                    case ConsoleKey.NumPad3:
                        break;
                    case ConsoleKey.NumPad4:
                        break;
                    case ConsoleKey.NumPad5:
                        break;
                    case ConsoleKey.NumPad6:
                        break;
                    case ConsoleKey.NumPad7:
                        break;
                    case ConsoleKey.NumPad8:
                        break;
                    case ConsoleKey.NumPad9:
                        break;
                    case ConsoleKey.Multiply:
                        break;
                    case ConsoleKey.Add:
                        break;
                    case ConsoleKey.Separator:
                        break;
                    case ConsoleKey.Subtract:
                        break;
                    case ConsoleKey.Decimal:
                        break;
                    case ConsoleKey.Divide:
                        break;
                    case ConsoleKey.F1:
                        break;
                    case ConsoleKey.F2:
                        break;
                    case ConsoleKey.F3:
                        break;
                    case ConsoleKey.F4:
                        break;
                    case ConsoleKey.F5:
                        break;
                    case ConsoleKey.F6:
                        break;
                    case ConsoleKey.F7:
                        break;
                    case ConsoleKey.F8:
                        break;
                    case ConsoleKey.F9:
                        break;
                    case ConsoleKey.F10:
                        break;
                    case ConsoleKey.F11:
                        break;
                    case ConsoleKey.F12:
                        break;
                    case ConsoleKey.F13:
                        break;
                    case ConsoleKey.F14:
                        break;
                    case ConsoleKey.F15:
                        break;
                    case ConsoleKey.F16:
                        break;
                    case ConsoleKey.F17:
                        break;
                    case ConsoleKey.F18:
                        break;
                    case ConsoleKey.F19:
                        break;
                    case ConsoleKey.F20:
                        break;
                    case ConsoleKey.F21:
                        break;
                    case ConsoleKey.F22:
                        break;
                    case ConsoleKey.F23:
                        break;
                    case ConsoleKey.F24:
                        break;
                    case ConsoleKey.BrowserBack:
                        break;
                    case ConsoleKey.BrowserForward:
                        break;
                    case ConsoleKey.BrowserRefresh:
                        break;
                    case ConsoleKey.BrowserStop:
                        break;
                    case ConsoleKey.BrowserSearch:
                        break;
                    case ConsoleKey.BrowserFavorites:
                        break;
                    case ConsoleKey.BrowserHome:
                        break;
                    case ConsoleKey.VolumeMute:
                        break;
                    case ConsoleKey.VolumeDown:
                        break;
                    case ConsoleKey.VolumeUp:
                        break;
                    case ConsoleKey.MediaNext:
                        break;
                    case ConsoleKey.MediaPrevious:
                        break;
                    case ConsoleKey.MediaStop:
                        break;
                    case ConsoleKey.MediaPlay:
                        break;
                    case ConsoleKey.LaunchMail:
                        break;
                    case ConsoleKey.LaunchMediaSelect:
                        break;
                    case ConsoleKey.LaunchApp1:
                        break;
                    case ConsoleKey.LaunchApp2:
                        break;
                    case ConsoleKey.Oem1:
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
                    case ConsoleKey.OemComma:
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
