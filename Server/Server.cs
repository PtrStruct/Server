using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        private static Socket _serverSocket = new Socket(AddressFamily.InterNetwork,
                                                  SocketType.Stream,
                                                  ProtocolType.Tcp);

        private static List<Socket> _clients = new List<Socket>();

        private static byte[] _buffer = new byte[65000];


        public Server()
        {

        }


        public void Start()
        {
            Misc.Log(LogType.Info, "Server is starting...");


            _serverSocket.Bind(new IPEndPoint(IPAddress.Any, ServerConstants.PORT));
            _serverSocket.Listen(5);
            _serverSocket.BeginAccept(AcceptCallback, null);

            Misc.Log(LogType.Info, "Server has started!");

        }


        private static void AcceptCallback(IAsyncResult result)
        {
            Misc.Log(LogType.Connection, "Client connected!");

            //capture the accepted client.
            var client = _serverSocket.EndAccept(result);

            //Add the captured client to the list of clients.
            _clients.Add(client);

            //Start receiving data from the accepted client.
            //pass in the client to the callback
            client.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceiveCallback, client);

            //Since the server socket is done dealing with accepting this client,
            //let the server socket begin accepting new clients
            _serverSocket.BeginAccept(AcceptCallback, null);
        }

        private static void ReceiveCallback(IAsyncResult result)
        {
            //A reference to the client socket.
            var client = (Socket)result.AsyncState;

            int received = client.EndReceive(result);

            var temp = new byte[received];
            Array.Copy(_buffer, temp, received);


            //Packet Structure
            //Offset  |  Type  |  Description
            //   0       Int32    Message Length
            //   1       Byte     Message Type
            //   2        *       Payload
            //----------------------------------
            // new byte[] { 0, 0, 1 }
    
            switch (_buffer[2])
            {
                case (int)PacketType.ConnectionRequest:

                    var k = (byte) PacketType.ConnectionApproved;

                    //Approved packet

                    //byte[] connectPacket = { 3, 0, 3, 2 this is the player slot };
                    byte[] connectPacket = { 3, 0, 3, 0 };
                    client.BeginSend(connectPacket, 0, connectPacket.Length, SocketFlags.None, SendCallback, client);
                    Misc.Log(LogType.Packet, "Sent connection approved Packet");
                    break;

                case (int)PacketType.PlayerAppearance:
                    client.BeginSend(_buffer, 0, _buffer.Length, SocketFlags.None, SendCallback, client);
                    Misc.Log(LogType.Packet, "Echo Player Appearance..");
                    break;

                case (int)PacketType.SetInventory:
                    Misc.Log(LogType.Packet, "Client Set Inventory");

                    client.BeginSend(_buffer, 0, _buffer.Length, SocketFlags.None, SendCallback, client);
                    Misc.Log(LogType.Packet, "Echo Set Inventory..");
                    break;

                //case (int)PacketType.RequestWorldInformation:
                //    Misc.Log(LogType.Packet, "Client Requested World Information");

                //    var worldInformation = new byte[] { 86, 0, 7, 2 };

                //    client.BeginSend(worldInformation, 0, worldInformation.Length, SocketFlags.None, SendCallback, client);
                //    Misc.Log(LogType.Packet, "Send World Information");
                //    break;

                default:
                    Misc.Log(LogType.Warning, $"Unknown packet type: {_buffer[2]}");
                    client.BeginSend(_buffer, 0, _buffer.Length, SocketFlags.None, SendCallback, client);

                    break;
            }

            Misc.Log(LogType.Packet, "Packet received.");



            client.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceiveCallback, client);

        }


        private static void SendCallback(IAsyncResult result)
        {
            Socket socket = (Socket)result.AsyncState;
            socket.EndSend(result);
        }
    }
}
