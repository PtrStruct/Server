using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class ServerConstants
    {
        public static string IP_ADDRESS = "localhost";
        public static int PORT = 7777;
    }

    public enum PacketType
    {
        ConnectionRequest = 1,
        FatalError = 0x2,
        ConnectionApproved = 0x3,
        PlayerAppearance = 4,
        SetInventory = 5,
        RequestWorldInformation = 6
    }
}
