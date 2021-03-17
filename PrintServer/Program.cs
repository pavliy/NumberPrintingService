using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PrintServer
{
    public class Program
    {
        private static void Main(string[] args)
        {
            TcpListener server = null;
            try
            {
                var port = 13000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                var bytes = new byte[256];

                // Enter the listening loop.
                while (true)
                {
                    Console.Write("Waiting for a connection... ");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        string data = Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received number: {0}", data);

                        // Ideally, I would go further and make this program send responses as well.
                        // For example, what if there is a failure and we want to process it carefully at UI side...
                        // The good point could be operating with codes.
                        // At this point - it's not super safe type of program for sure.
                    }

                    // Shutdown and end connection
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }
    }
}