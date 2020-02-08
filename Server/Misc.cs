using System;
using System.ComponentModel;

namespace Server
{
    class Misc
    {
        public static void Log(LogType type, string message)
        {
            switch (type)
            {
                case LogType.Connection:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;

                case LogType.Packet:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                    
                case LogType.Info:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;

                case LogType.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

                case LogType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                default:
                    type = LogType.Info;
                    break;
            }


            Console.Write($"[{type}] ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(message + Environment.NewLine);
        }
    }

    public enum LogType
    {
        Connection,
        Packet,
        Info,
        Warning,
        Error,

    }
}
