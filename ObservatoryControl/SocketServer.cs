using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Collections;

namespace ObservatoryCenter
{
    public class SocketServerClass
    {
        /// <summary>
        /// Server parameters
        /// </summary>
        IPAddress serverIP=IPAddress.Any;
        Int32 serverPort=1400;

        /// <summary>
        /// Main socket listener
        /// </summary>
        Socket listenerSocket;

        /// <summary>
        /// Client list
        /// </summary>
        public List<ClientManager> clientsList = new List<ClientManager>();

        /// <summary>
        /// Back link to form
        /// </summary>
        public MainForm ParentMainForm;


        /// <summary>
        /// Conctructor
        /// </summary>
        public SocketServerClass(MainForm MF)
        {
            ParentMainForm=MF;
        }
        
        public void ListenSocket()
        {
            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenerSocket.Bind(new IPEndPoint(serverIP, serverPort));
            listenerSocket.Listen(200);
            while (true)
            {
                // Программа приостановлена. Ожидаем входящего соединения
                Socket handler = listenerSocket.Accept();
                
                //Входящее соединение необходимо обработать
                initClientConnection(handler);
            }
        }

        public void initClientConnection(Socket curSocket)
        {
            Logging.Log("Connection from " + curSocket.RemoteEndPoint + " accepted");

            //Создаем объект для обработки клиента
            ClientManager NewClient=new ClientManager();
            
            //Добавляем его в список
            clientsList.Add(NewClient);
            
            //Передаем ему управление
            NewClient.CreateNewClientManager(curSocket);

            //Отображаем кол-во соединений в форме
            ParentMainForm.toolStripStatus_Connection.Text = "CONNECTION: " + clientsList.Count;


        }

        public void SocketClient(IPAddress ipAddr, Int32 port, string message)
        {
            // Буфер для входящих данных
            byte[] bytes = new byte[1024];
            
            // Устанавливаем удаленную точку для сокета
            //IPAddress ipAddr = Dns.GetHostEntry("localhost").AddressList[0];
            //IPAddress localAddr = IPAddress.Parse("127.0.0.1"); 
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Соединяем сокет с удаленной точкой
            sender.Connect(ipEndPoint);

            Logging.Log("Connected to "+ sender.RemoteEndPoint.ToString(),2);
            byte[] msg = Encoding.UTF8.GetBytes(message);

            // Отправляем данные через сокет
            int bytesSent = sender.Send(msg);

            // Получаем ответ от сервера
            int bytesRec = sender.Receive(bytes);

            string responseMess=Encoding.UTF8.GetString(bytes, 0, bytesRec);
            Logging.Log("Response from server: " + responseMess);

            /*// Используем рекурсию для неоднократного вызова SendMessageFromSocket()
            if (message.IndexOf("<TheEnd>") == -1)
                SendMessageFromSocket(port);
            */
            // Освобождаем сокет
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }

    }

    /// <summary>
    /// Class for every client management
    /// </summary>
    public class ClientManager
    {
        /// <summary>
        /// Client socket
        /// </summary>
        Socket ClientSocket;

        /// <summary>
        /// Client thread
        /// </summary>
        Thread curThread;

        // Буфер для входящих данных
        byte[] bytes = new byte[1024];

        public ClientManager()
        {
        }

        public void CreateNewClientManager(Socket NewClient)
        {
            this.ClientSocket = NewClient;
            curThread = new Thread(startClientThread);
            curThread.Start();
        }

        /// <summary>
        /// Start unique thread for every client
        /// </summary>
        public void startClientThread()
        {

            //Отправляем приветственное сообщение
            byte[] msg = Encoding.UTF8.GetBytes("Connected to ObservatoryCenter\n\r");
            ClientSocket.Send(msg);

            while (true)
            {
                // Получаем ответ от клиента
                int bytesRec = ClientSocket.Receive(bytes);

                string responseMess = Encoding.UTF8.GetString(bytes, 0, bytesRec);
                Logging.Log("Message from сlient [" + ClientSocket.RemoteEndPoint + "]: " + responseMess);

                string cmdMess=CommandInterpretator(responseMess);

                byte[] msg2 = Encoding.UTF8.GetBytes(cmdMess + "\n\r");
                ClientSocket.Send(msg2);
            }


            //Отображаем кол-во соединений в форме
            //ParentMainForm.toolStripStatus_Connection.Text = "CONNECTION: " + clientsList.Count;
        }

        public string CommandInterpretator(string cmd)
        {
            string msg = "";
            
            switch (cmd)
            {
                case "TheEnd":
                // Освобождаем сокет
                    Logging.Log("Client [" + ClientSocket.RemoteEndPoint + "] has ended connection");
                    ClientSocket.Shutdown(SocketShutdown.Both);
                    ClientSocket.Close();
                    msg = "";
                    break;
                default:
                    Logging.Log("Client [" + ClientSocket.RemoteEndPoint + "]: " + "Unknown command [" + cmd + "]");
                    msg = "Client [" + ClientSocket.RemoteEndPoint + "]: Unknown command [" + cmd + "]";
                    break;
            }


            return msg;
        }

    
    
    }
}
