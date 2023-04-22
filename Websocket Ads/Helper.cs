using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Websocket_Ads
{
    class Helper
    {
         
        public string AccessType;
        public string VariableType;
        public string VariableName;
        public bool bValue;
        public float fValue;
        public int iValue;
        public string sValue;

        public string SetIpAndPort(string Id)
        {
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string Ip = "Fail";

            if (Id.Equals("WebServerData"))
            {
                string Path = System.IO.Path.GetDirectoryName(strExeFilePath);
                Ip = File.ReadAllText(Path + @"\SocketData.txt");
            }
            if (Id.Equals("AdsServerData"))
            {
                string Path = System.IO.Path.GetDirectoryName(strExeFilePath);
                Ip = File.ReadAllText(Path + @"\ADS.txt");
            }
            return Ip;
        }
    }
}
