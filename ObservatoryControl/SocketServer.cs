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
        
        public void ListenSocket()
        {
            try
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
            catch (Exception Ex)
            {
                Logging.AddLog("Server connection errror [" + Ex.Message+"]",0,Highlight.Error);
            }
        }

        public void initClientConnection(Socket curSocket)
        {
            Logging.AddLog("Connection from " + curSocket.RemoteEndPoint + " accepted",1);

            //Создаем объект для обработки клиента
            ClientManager NewClient=new ClientManager(ParentMainForm);
            
            //Добавляем его в список
            clientsList.Add(NewClient);
            
            //Передаем ему управление
            NewClient.CreateNewClientManager(curSocket);

            //Отображаем кол-во соединений в форме
            ParentMainForm.toolStripStatus_Connection.Text = "CONNECTIONS: " + clientsList.Count;


        }

        public void MakeClientConnectionToServer(IPAddress ipAddr, Int32 port, string message)
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
                Logging.AddLog("Connected to " + sender.RemoteEndPoint.ToString(), 2);
                byte[] msg = Encoding.UTF8.GetBytes(message);

                // Отправляем данные через сокет
                int bytesSent = sender.Send(msg);

                // Получаем ответ от сервера
                int bytesRec = sender.Receive(bytes);

                string responseMess = Encoding.UTF8.GetString(bytes, 0, bytesRec);
                Logging.AddLog("Response from server: " + responseMess,2);

                // Освобождаем сокет
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
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

                Logging.AddLog("MakeClientConnectionToServer socket connection failed! " + Ex.Message, 0, Highlight.Error);
                Logging.AddLog(FullMessage,1,Highlight.Error);
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
            byte[] msg = Encoding.UTF8.GetBytes("Connected to ObservatoryCenter\n\r");
            ClientSocket.Send(msg);

            while (true)
            {
                // Получаем ответ от клиента
                int bytesRec = ClientSocket.Receive(bytes);

                string responseMess = Encoding.UTF8.GetString(bytes, 0, bytesRec);
                Logging.AddLog("Message from сlient [" + ClientSocket.RemoteEndPoint + "]: " + responseMess,1);

                string cmdMess=SocketCommandInterpretator(responseMess);

                byte[] msg2 = Encoding.UTF8.GetBytes(cmdMess + "\n\r");
                ClientSocket.Send(msg2);
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
                    Logging.AddLog("Client [" + ClientSocket.RemoteEndPoint + "] has ended connection",1);
                    ClientSocket.Shutdown(SocketShutdown.Both);
                    ClientSocket.Close();
                    msg = "";
                    break;
                default:
                    if (ParentMainForm.ObsControl.CommandParser.ParseSingleCommand(cmd))
                    {
                        Logging.AddLog("Client [" + ClientSocket.RemoteEndPoint + "]: " + "command [" + cmd + "] successfully run", 1, Highlight.Normal);
                        msg = "Command [" + cmd + "] was run";
                    }
                    else
                    {
                        Logging.AddLog("Client [" + ClientSocket.RemoteEndPoint + "]: " + "Unknown command [" + cmd + "]", 1, Highlight.Error);
                        msg = "Unknown command [" + cmd + "]";
                    }
                    break;
            }


            return msg;
        }

    
    
    }
}
