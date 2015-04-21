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
        IPAddress serverIP=IPAddress.Any;
        Int32 serverPort=1400;

        public static Hashtable clientsList = new Hashtable();

        Socket listenerSocket;

        public MainForm ParentMainForm;


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
                // Это синхронное TCP приложение
                Socket handler = listenerSocket.Accept();
                
                //Входящее соединение необходимо обработать
                CreateNewClientManager(handler);
            }
        }

        public void CreateNewClientManager(Socket NewClient)
        {
            // Буфер для входящих данных
            byte[] bytes = new byte[1024];

            //Добавляем в массив
            clientsList.Add(clientsList.Count, NewClient);

            //Отображаем кол-во соединений в форме
            ParentMainForm.toolStripStatus_Connection.Text = "CONNECTION: " + clientsList.Count;

            Logging.Log("Connection from " + NewClient.RemoteEndPoint + " accepted");

            //Отправляем приветственное сообщение
            byte[] msg = Encoding.UTF8.GetBytes("Connected to ObservatoryCenter\n\r");
            NewClient.Send(msg);

            // Получаем ответ от клиента
            int bytesRec = NewClient.Receive(bytes);

            string responseMess=Encoding.UTF8.GetString(bytes, 0, bytesRec);
            Logging.Log("Response from сlient: " + responseMess);

            // Освобождаем сокет
            NewClient.Shutdown(SocketShutdown.Both);
            NewClient.Close();

            //Отображаем кол-во соединений в форме
            //ParentMainForm.toolStripStatus_Connection.Text = "CONNECTION: " + clientsList.Count;
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
}
