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
        
        /// <summary>
        /// Start socket server
        /// </summary>
        public void ListenSocket()
        {
            try
            {
                listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
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
                Logging.AddLog("Server connection errror [" + Ex.Message+"]",LogLevel.Debug,Highlight.Error);
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
        public static string MakeClientConnectionToServer(IPAddress ipAddr, Int32 port, string message, out int ErrorCode)
        {
            // Буфер для входящих данных
            byte[] bytes = new byte[1024];

            // Устанавливаем удаленную точку для сокета
            //IPAddress ipAddr = Dns.GetHostEntry("localhost").AddressList[0];
            //IPAddress localAddr = IPAddress.Parse("127.0.0.1"); 
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                // Соединяем сокет с удаленной точкой
                sender.Connect(ipEndPoint);
                Logging.AddLog("Connected to " + sender.RemoteEndPoint.ToString(), LogLevel.Activity);

                // Получаем первый ответ от сервера
                int bytesRecW = sender.Receive(bytes);

                string responseMessW = Encoding.UTF8.GetString(bytes, 0, bytesRecW);
                Logging.AddLog("Welcome response from server: " + responseMessW, LogLevel.Chat);

                // Отправляем данные через сокет
                byte[] msg = Encoding.UTF8.GetBytes(message);
                int bytesSent = sender.Send(msg);

                // Получаем ответ от сервера
                int bytesRec = sender.Receive(bytes);

                string responseMess = Encoding.UTF8.GetString(bytes, 0, bytesRec);
                Logging.AddLog("Response from server: " + responseMess, LogLevel.Chat);

                // Освобождаем сокет
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();

                ErrorCode = 0;
                return responseMess;
            }
            catch (Exception Ex)
            {
                StackTrace st = new StackTrace(Ex, true);
                StackFrame[] frames = st.GetFrames();
                string messstr = "";

                // Iterate over the frames extracting the information you need
                foreach (StackFrame frame in frames)
                {
                    messstr += String.Format("{0}:{1}({2},{3})", frame.GetFileName(), frame.GetMethod().Name, frame.GetFileLineNumber(), frame.GetFileColumnNumber());
                }

                string FullMessage = "MakeClientConnectionToServer socket connection failed!" + Environment.NewLine;
                FullMessage += Environment.NewLine + Environment.NewLine + "Debug information:" + Environment.NewLine + "IOException source: " + Ex.Data + " " + Ex.Message
                        + Environment.NewLine + Environment.NewLine + messstr;
                //MessageBox.Show(this, FullMessage, "Invalid value", MessageBoxButtons.OK);

                Logging.AddLog("MakeClientConnectionToServer socket connection failed! " + Ex.Message, LogLevel.Important, Highlight.Error);
                Logging.AddLog(FullMessage, LogLevel.Debug, Highlight.Error);
                ErrorCode = -1;
                return "Socket connection failed";
            }
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

        /// <summary>
        /// Back link to form
        /// </summary>
        public MainForm ParentMainForm;


        // Буфер для входящих данных
        byte[] bytes = new byte[1024];

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
            byte[] welcomeMessage = Encoding.UTF8.GetBytes("Connected to ObservatoryCenter\n\r");
            ClientSocket.Send(welcomeMessage);

            while (true)
            {
                // Получаем ответ от клиента
                int incomingMess_bytes = ClientSocket.Receive(bytes);

                string incomingMess = Encoding.UTF8.GetString(bytes, 0, incomingMess_bytes);
                Logging.AddLog("Message from сlient [" + ClientSocket.RemoteEndPoint + "]: " + incomingMess,LogLevel.Chat);

                string cmdOutputMess=SocketCommandInterpretator(incomingMess);

                byte[] cmdOutputMess_bytes = Encoding.UTF8.GetBytes(cmdOutputMess + "\n\r");
                ClientSocket.Send(cmdOutputMess_bytes);
            }


            //Отображаем кол-во соединений в форме
            //ParentMainForm.toolStripStatus_Connection.Text = "CONNECTION: " + clientsList.Count;
        }

        public string SocketCommandInterpretator(string cmd)
        {
            string msg = "";
            
            switch (cmd)
            {
                case "TheEnd":
                // Освобождаем сокет
                    Logging.AddLog("Client [" + ClientSocket.RemoteEndPoint + "] has ended connection", LogLevel.Chat);
                    ClientSocket.Shutdown(SocketShutdown.Both);
                    ClientSocket.Close();
                    msg = "";
                    break;
                default:
                    string cmd_output = "";
                    if (ParentMainForm.ObsControl.CommandParser.ParseSingleCommand(cmd, out cmd_output))
                    {
                        Logging.AddLog("Client [" + ClientSocket.RemoteEndPoint + "]: " + "command [" + cmd + "] successfully run. Output: " + cmd_output, LogLevel.Activity, Highlight.Normal);
                        msg = "Command [" + cmd + "] was run. Output: " + cmd_output;
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
