using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatBot_Caputo
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                                    ProtocolType.Tcp);
           
            IPAddress ipaddr = IPAddress.Any;

            IPEndPoint ipep = new IPEndPoint(ipaddr, 23000);
            
            listenerSocket.Bind(ipep);

            listenerSocket.Listen(5);
            Console.WriteLine("Il server sta ascoltando");
            Console.WriteLine("il server attende la connessione del client");
           
            Socket client = listenerSocket.Accept();

            Console.WriteLine("Client IP: " + client.RemoteEndPoint.ToString());

            
            byte[] buff = new byte[128];
            int receivedBytes = 0;

            int sendedsBytes = 0;
            string sendedsString;

            while (true)
            {
                
                receivedBytes = client.Receive(buff);
                Console.WriteLine("Numero di byte ricevuti: " + receivedBytes);
                
                string receivedString = Encoding.ASCII.GetString(buff, 0, receivedBytes);
                Console.WriteLine("Stringa ricevuta: " + receivedString);

                switch (receivedString.ToUpper())
                {
                    case "QUIT":
                        break;

                    case "CIAO":
                        sendedsString = "Ciao";
                        Array.Clear(buff, 0, buff.Length);
                        buff = Encoding.ASCII.GetBytes(sendedsString);
                        sendedsBytes = client.Send(buff);
                        break;

                    case "COME STAI?":
                        sendedsString = "Bene";
                        Array.Clear(buff, 0, buff.Length);
                        buff = Encoding.ASCII.GetBytes(sendedsString);
                        sendedsBytes = client.Send(buff);
                        break;

                    case "CHE FAI?":
                        sendedsString = "Niente";
                        Array.Clear(buff, 0, buff.Length);
                        buff = Encoding.ASCII.GetBytes(sendedsString);
                        sendedsBytes = client.Send(buff);
                        break;

                    default:
                        sendedsString = "Non importa";
                        Array.Clear(buff, 0, buff.Length);
                        buff = Encoding.ASCII.GetBytes(sendedsString);
                        sendedsBytes = client.Send(buff);
                        break;
                }
            }
        }
    }
}
