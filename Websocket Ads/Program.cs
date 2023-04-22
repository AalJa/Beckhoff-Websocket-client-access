using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;
using TwinCAT.Ads;
using System.Net.Sockets;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Runtime.InteropServices.ComTypes;
using System.IO;


namespace Websocket_Ads
{
   
    public class Echo : WebSocketBehavior
    {
       
        DataIn dataIn = new DataIn();
        Helper helper = new Helper();
       
        protected override void OnMessage(MessageEventArgs e) 
        {
            Console.WriteLine("Received message from client: " + e.Data);
            //Sessions.Broadcast(e.Data + " Brinter " + DateTime.Now); // Use this if messages are required to send for all connected client
            //dataIn.ParseIncomingData(e.Data);                        // Use with non json syntax -> not finished
            
            if (dataIn.ParseIncomingDataJson(dataIn.FindValueType(e.Data)))  // Use with json syntax
            {
                Send(Format_Data(e.Data));

               // else {Send(e.Data + helper.iValue); }
            }
            else
            { 
                Send("Failed" + e.Data + " Brinter  " + DateTime.Now); 
            }
        }
        public string Format_Data(string data)
        {
            string formattedData = "";
            if (!data.Contains("Value"))
            {
                string Aux = data.Replace("}", string.Empty);
                formattedData = Aux + "  \"Value\"" + ": " + 1 + " ," + "\n" + " }";
            } 
            else
            {
                formattedData = data;
            }

            return formattedData;
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Helper helper = new Helper();
            Ads ads = new Ads();
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string Path = System.IO.Path.GetDirectoryName(strExeFilePath);
            string SocketData = "";


            try
            {
                SocketData = File.ReadAllText(Path + @"\SocketData.txt");
                WebSocketServer wssv = new WebSocketServer(helper.SetIpAndPort("WebServerData"));
                // "ws://Localhost:7890"  //Toimii lokaalisti       // "ws://192.168.10.102:7890  // Toimii globaalisti

                //Endpoints
                wssv.AddWebSocketService<Echo>("/BrinterEcho");
                wssv.Start();
                Console.WriteLine("Client Connected to Websocketserver:  " + SocketData + "  " + DateTime.Now );
            }
            catch
            {
                Console.WriteLine("Can not connect Websocket server. Please check that server is running and your IP + port information [SocketData.txt] is correct ");
            }
          
          
            if(!ads.GetAdsConnectionInfo()) ads.Connect_To_Ads_Server();

            Console.ReadKey();
           //wssv.Stop();
        }
    }
}
