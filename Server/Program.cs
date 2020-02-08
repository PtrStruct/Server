using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Server";
            Misc.Log(LogType.Info, "Press [Esc] to stop the server.");
            Server server = new Server();
            Console.ForegroundColor = ConsoleColor.White;



            //Misc.Log(LogType.Info, $"IsLittleEndian:  {BitConverter.IsLittleEndian.ToString()}");

            //Misc.Log(LogType.Info, "Server is starting...");
            //Misc.Log(LogType.Packet, "=> 0xFF 0x65 0xAB 0xCC");
            //Misc.Log(LogType.Packet, "<= 0xFF 0x65 0xAB 0xCC");
            //Misc.Log(LogType.Connection, "Client has connected!");
            //Misc.Log(LogType.Warning, "Server is experiencing an unusual amount of traffic.");
            //Misc.Log(LogType.Error, "Exception: Stack overflow.");

            server.Start();



            do
            {
                while (!Console.KeyAvailable)
                {
                    // Do something
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
