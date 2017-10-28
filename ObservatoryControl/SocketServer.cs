using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Diagnostics;

namespace ObservatoryCenter
{
    public class SocketServerClass : IDisposable
    {
        /// <summary>
        /// Server parameters
        /// </summary>
        IPAddress serverIP=IPAddress.Any;
        Int32 serverPort=1452;

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
        
        /// <summary>
        /// Start socket server
        /// </summary>
        public void ListenSocket()
        {
            try
            {
                listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                listenerSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                //listenerSocket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.ExclusiveAddressUse, true);
                
                listenerSocket.Bind(new IPEndPoint(serverIP, serverPort));
                listenerSocket.Listen(10);
                while (true)
                {
                    // Программа приостановлена. Ожидаем входящего соединения
                    Socket handler = listenerSocket.Accept();

                    //Входящее соединение необходимо обработать
                    initClientConnection(handler);
                }
            }
            catch (Exception Ex)
            {
                Logging.AddLog("Server connection error [" + Ex.Message+"]",LogLevel.Important,Highlight.Error);
            }
        }

        /// <summary>
        /// Handle incoming client connection
        /// </summary>
        /// <param name="curSocket"></param>
        public void initClientConnection(Socket curSocket)
        {
            Logging.AddLog("Connection from " + curSocket.RemoteEndPoint + " accepted",LogLevel.Activity);

            //Создаем объект для обработки клиента
            ClientManager NewClient=new ClientManager(ParentMainForm);
            
            //Добавляем его в список
            clientsList.Add(NewClient);
            
            //Передаем ему управление
            NewClient.CreateNewClientManager(curSocket);

            //Отображаем кол-во соединений в форме
            ParentMainForm.toolStripStatus_Connection.Text = "CONNECTIONS: " + clientsList.Count;
        }

        /// <summary>
        /// Static method for connecting to socket server as a client 
        /// not very good place for a such method, but...
        /// </summary>
        public static string ConnectToServerAndSendMessage(IPAddress ipAddr, Int32 port, string message, out int ErrorCode)
        {
            // Буфер для входящих данных
            byte[] bytes = new byte[1024];

            // Устанавливаем удаленную точку для сокета
            //IPAddress ipAddr = Dns.GetHostEntry("localhost").AddressList[0];
            //IPAddress localAddr = IPAddress.Parse("127.0.0.1"); 
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            Socket ServerSocketTemp = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            ServerSocketTemp.ReceiveTimeout =3000;

            try
            {
                // Соединяем сокет с удаленной точкой
                ServerSocketTemp.Connect(ipEndPoint);
                Logging.AddLog("Connected to " + ServerSocketTemp.RemoteEndPoint.ToString(), LogLevel.Debug);

                // Получаем первый ответ от сервера
                while (ServerSocketTemp.Available > 0)
                {
                    int bytesRecW = ServerSocketTemp.Receive(bytes);
                    string responseMessW = Encoding.UTF8.GetString(bytes, 0, bytesRecW);
                    Logging.AddLog("Welcome response from server: " + responseMessW, LogLevel.Chat);
                }

                Thread.Sleep(100);

                // Отправляем данные через сокет
                byte[] msg = Encoding.UTF8.GetBytes(message);
                Logging.AddLog("Sending message: " + message, LogLevel.Chat);
                int bytesSent = ServerSocketTemp.Send(msg);

                Thread.Sleep(200);

                // Получаем реакцию сервера на команду (второй ответ)
                string responseMessAll = "";
                while (ServerSocketTemp.Available > 0)
                {
                    int bytesRec = ServerSocketTemp.Receive(bytes);
                    string responseMess = Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    responseMessAll += responseMess;
                    Logging.AddLog("Response from server: " + responseMess, LogLevel.Chat);
                }

                // Освобождаем сокет
                ServerSocketTemp.Shutdown(SocketShutdown.Both);
                ServerSocketTemp.Close();

                ErrorCode = 0;
                return responseMessAll;
            }
            catch (Exception Ex)
            {
                Logging.LogExceptionMessage(Ex, "MakeClientConnectionToServer socket connection failed");
                ErrorCode = -1;
                return "Socket connection failed";
            }
        }


        /// <summary>
        /// Static method for connecting to socket server as a client 
        /// not very good place for a such method, but...
        /// </summary>
        public static string ConnectToServer(IPAddress ipAddr, Int32 port, out Socket ServerSocket, out int ErrorCode)
        {

            // Устанавливаем удаленную точку для сокета
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);
            ServerSocket = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            ServerSocket.ReceiveTimeout = 3000;

            try
            {
                // Соединяем сокет с удаленной точкой
                ServerSocket.Connect(ipEndPoint);
                Logging.AddLog("Connected to " + ServerSocket.RemoteEndPoint.ToString(), LogLevel.Debug);

                ErrorCode = 0;
                return "Connected to " + ServerSocket.RemoteEndPoint.ToString();
            }
            catch (Exception Ex)
            {
                string St = "Socket connection to " + ipAddr.ToString() + ":" + port.ToString() + " failed";
                Logging.LogExceptionMessage(Ex, St, false);
                ServerSocket = null;
                ErrorCode = -1;
                return St;
            }
        }

        /// <summary>
        /// Static method for connecting to socket server as a client 
        /// not very good place for a such method, but...
        /// </summary>
        public static string SendToServer(Socket ServerSocket, string message, out int ErrorCode)
        {
            try
            {
                // Отправляем данные через сокет
                byte[] msg = Encoding.UTF8.GetBytes(message);
                Logging.AddLog("Sending to "+ ServerSocket.RemoteEndPoint.ToString() + " message : " + message, LogLevel.Chat);

                int bytesSent = ServerSocket.Send(msg);

                ErrorCode = 0;
                return "Message to "+ ServerSocket.RemoteEndPoint.ToString() + " sent";
            }
            catch (Exception Ex)
            {
                Logging.LogExceptionMessage(Ex, "SendToServer ["+ message + "] failed");
                ErrorCode = -1;
                return null;
            }
        }

        /// <summary>
        /// Static method for connecting to socket server as a client 
        /// not very good place for a such method, but...
        /// </summary>
        public static string ReceiveFromServer(Socket ServerSocket, out int ErrorCode)
        {
            if (ServerSocket != null)
            {
                // Буфер для входящих данных
                byte[] bytes = new byte[1024];
                string responseMessAll = "";
                int bytesRecW = -1;
                try
                {
                    // Получаем первый ответ от сервера
                    while (ServerSocket.Available > 0)
                    {
                        bytesRecW = ServerSocket.Receive(bytes);
                        string responseMess = Encoding.UTF8.GetString(bytes, 0, bytesRecW);
                        responseMessAll += responseMess;
                        Logging.AddLog("Received message from server ["+ ServerSocket.RemoteEndPoint.ToString() + "]: " + responseMess, LogLevel.Chat);
                    }
                    if (bytesRecW >0)
                    {
                        ErrorCode = 0;
                        return responseMessAll;
                    }
                    else
                    {
                        ErrorCode = -10;
                        return null;
                    }
                }
                catch (Exception Ex)
                {
                    Logging.LogExceptionMessage(Ex, "ReceiveFromServer socket connection ["+ ServerSocket.RemoteEndPoint.ToString() + "] failed");
                    ErrorCode = -1;
                    return null;
                }
            }
            else
            {
                ErrorCode = -2;
                return null;
            }
        }

        /// <summary>
        /// Static method for connecting to socket server as a client 
        /// not very good place for a such method, but...
        /// </summary>
        public static int SendToServer2(Socket ServerSocket, string message, out string outmessage)
        {
            int ReturnCode = -1;
            string ReturnMess = "";
            try
            {
                // Отправляем данные через сокет
                byte[] msg = Encoding.UTF8.GetBytes(message);
                Logging.AddLog("Sending to " + ServerSocket.RemoteEndPoint.ToString() + " message : " + message, LogLevel.Chat);

                int bytesSent = ServerSocket.Send(msg);

                ReturnCode = 0;
                ReturnMess = "Message to " + ServerSocket.RemoteEndPoint.ToString() + " sent";
            }
            catch (Exception Ex)
            {
                ReturnMess = "SendToServer [" + message + "] failed";
                Logging.LogExceptionMessage(Ex, ReturnMess);
                ReturnCode = -1;
            }
            outmessage = ReturnMess;
            return ReturnCode;
        }



        /// <summary>
        /// Static method for connecting to socket server as a client 
        /// not very good place for a such method, but...
        /// </summary>
        public static string DisconnectFromServer(Socket ServerSocket, out int ErrorCode)
        {
            string st = "";
            try
            {
                st = ServerSocket.RemoteEndPoint.ToString();
                // Освобождаем сокет
                ServerSocket.Shutdown(SocketShutdown.Both);
                ServerSocket.Close();
                ServerSocket = null;
                ErrorCode = 0;
                return "Connection ["+ st + "] closed";
            }
            catch (Exception Ex)
            {
                Logging.LogExceptionMessage(Ex, "DisconnectFromServer [" + st + "] failed");
                ErrorCode = -1;
                return null;
            }

        }

        //public static TcpClient client = null;

        public static string ___ConnectTCPToServer_OLD(IPAddress ipAddr, Int32 port, out TcpClient client, out int ErrorCode)
        {

            string message = @"{""method"": ""set_connected"", ""params"": [true], ""id"": 1}" + "\r\n";

            // Устанавливаем удаленную точку для сокета
            TcpClient tcpClient = new TcpClient();
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);
            tcpClient.Connect(ipEndPoint);

            try
            {
                // Buffer to store the response bytes.
                Byte[] DataBuffer = new Byte[10000];
                string FullMessage = "";
                string responseData = "";

                // Get a client stream for reading and writing.
                NetworkStream stream = tcpClient.GetStream();

                Thread.Sleep(1000);


                // Read the first batch of the TcpServer response bytes.
                while (stream.DataAvailable)
                {
                    Int32 readbytes = stream.Read(DataBuffer, 0, DataBuffer.Length);
                    responseData = Encoding.ASCII.GetString(DataBuffer, 0, readbytes);
                    FullMessage += responseData;
                    Logging.AddLog("Received welcome message: " + responseData, LogLevel.Chat);
                }

                Thread.Sleep(1000);

                // Translate the passed message into ASCII and store it as a Byte array.
                DataBuffer = Encoding.ASCII.GetBytes(message);
                // Send the message to the connected TcpServer. 
                stream.Write(DataBuffer, 0, DataBuffer.Length);
                Logging.AddLog("Sent message: " + message, LogLevel.Chat);

                Thread.Sleep(1000);

                // Receive the TcpServer.response.

                // String to store the response ASCII representation.
                responseData = String.Empty;
                FullMessage = String.Empty;
                // Read the first batch of the TcpServer response bytes.
                while (stream.DataAvailable)
                {
                    Int32 readbytes = stream.Read(DataBuffer, 0, DataBuffer.Length);
                    responseData = Encoding.ASCII.GetString(DataBuffer, 0, readbytes);
                    FullMessage += responseData;
                    Logging.AddLog("Received message: " + responseData, LogLevel.Chat);
                }

                ErrorCode = 0;
                client = tcpClient;
                return "Connected to ";
            }
            catch (Exception Ex)
            {
                Logging.LogExceptionMessage(Ex, "ConnectTCPToServer failed");
                client = null;
                ErrorCode = -1;
                return "Socket connection failed";
            }
        }

        public void Dispose()
        {
            listenerSocket.Shutdown(SocketShutdown.Both);
            listenerSocket.Close();
            listenerSocket = null;
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Class for every client management
    /// </summary>
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

        /// <summary>
        /// Back link to form
        /// </summary>
        public MainForm ParentMainForm;

        private byte[] welcomeMsg = Encoding.UTF8.GetBytes("Connected to ObservatoryCenter\r\n");
        const string STOP_MESSAGE = "TheEnd";

        // Буфер для входящих данных
        byte[] incomingBuffer = new byte[1024];

        public ClientManager(MainForm MF)
        {
            ParentMainForm = MF;
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
            ClientSocket.Send(welcomeMsg);

            try
            {
                //запускаем бесконечный цикл
                while (true)
                {
                    // Получаем ответ от клиента (если нет данных, то висим)
                    // method will block run until data is available
                    int incomingMess_bytes = ClientSocket.Receive(incomingBuffer);

                    if (!(incomingMess_bytes == 2 && incomingBuffer[0] == 13 && incomingBuffer[1] == 10)) //  \r = 0D, 13  \n = 0A, 10
                    {
                        //Convert message from UTF8
                        string incomingMess = Encoding.UTF8.GetString(incomingBuffer, 0, incomingMess_bytes);
                        Logging.AddLog("Message from сlient [" + ClientSocket.RemoteEndPoint + "]: " + incomingMess, LogLevel.Chat);

                        string cmdOutputMess = SocketCommandInterpretator(incomingMess);

                        byte[] cmdOutputMess_bytes = Encoding.UTF8.GetBytes(cmdOutputMess + "\r\n");
                        ClientSocket.Send(cmdOutputMess_bytes);
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.AddLog("Socket client [" + ClientSocket.RemoteEndPoint + "] exception: " + ex.ToString(), LogLevel.Important, Highlight.Error);
            }

            //Отображаем кол-во соединений в форме
            //ParentMainForm.toolStripStatus_Connection.Text = "CONNECTION: " + clientsList.Count;
        }

        /// <summary>
        /// Stop client socket and thread
        /// </summary>
        public void StopClientThread()
        {
            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Close();
            //curThread.Abort(); // НЕ НУЖНО! Поток и так завершиться после BREAK в цикле
        }

        /// <summary>
        /// Receives Command and make neccesary action:
        /// - if it is ENDMESSAGE shutdown socket
        /// - else run CommandParser class 
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public string SocketCommandInterpretator(string cmd)
        {
            string msg = "";
            
            switch (cmd)
            {
                case STOP_MESSAGE:
                    // Освобождаем сокет
                    Logging.AddLog("Client [" + ClientSocket.RemoteEndPoint + "] has ended connection", LogLevel.Chat);
                    StopClientThread();
                    msg = STOP_MESSAGE;
                    break;
                default:
                    string cmd_output = "";
                    if (ParentMainForm.ObsControl.CommandParser.ParseSingleCommand2(cmd, out cmd_output))
                    {
                        Logging.AddLog("Client [" + ClientSocket.RemoteEndPoint + "]: " + "command [" + cmd + "] successfully run. Output: " + cmd_output, LogLevel.Activity, Highlight.Normal);
                        msg = "Command [" + cmd + "] was run. Output: " + Environment.NewLine  + cmd_output;
                    }
                    else
                    {
                        Logging.AddLog("Client [" + ClientSocket.RemoteEndPoint + "]: " + "Unknown command [" + cmd + "]", LogLevel.Important, Highlight.Error);
                        msg = "Unknown command [" + cmd + "]";
                    }
                    break;
            }


            return msg;
        }

    
    
    }
}
