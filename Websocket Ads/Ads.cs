using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.Ads;
using TwinCAT.TypeSystem;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;



namespace Websocket_Ads // Receive incoming already parsed data and send it to PLC
{
    internal class Ads
    {
        static TcAdsClient tcClient;
        static int Handle;
        static bool bAdsServerConnected = false;
        Helper helper = new Helper();


        public bool GetAdsConnectionInfo()
        {
            return bAdsServerConnected;
        }

        public void Connect_To_Ads_Server()
        {
            try
            {
                tcClient = new TcAdsClient();
                string FileData = helper.SetIpAndPort("AdsServerData");
                string IpAddr = FileData.Split(',')[0];                  //Extract Datatype out of the string
                string Value = FileData.Substring(IpAddr.Length + 1);    //Extract the latter body of the string
                Int32.TryParse(Value, out int iPort);                    //Find the portnumber from string
                tcClient.Connect(IpAddr, iPort);                         //Connect to Beckhoff Ads server
                bAdsServerConnected = true;
                Console.WriteLine("Client connected to Ads server: IP: " + IpAddr + ", Port: " + iPort + "  " + DateTime.Now);
            }
            catch
            {
                bAdsServerConnected = false;
                Console.WriteLine("Can not connect Ads server. Please check that server is running or your IP information [ADS.txt] is correct ");
            }
        }

        // WRITE ADS DATA REQUESTED_________________________________________________________________________________________________________________

        public string Write_boolData_To_Plc(string sWritevariable, bool bValue)
        {
            try
            {
                Handle = tcClient.CreateVariableHandle(sWritevariable);
                tcClient.WriteAny(Handle, bValue);
            }
            catch
            {
                return "Fail " + sWritevariable + " " + bValue;
            }
            return "Ok";
        }

        public string Write_uintData_To_Plc(string sWritevariable, int iValue)
        {
            try
            {
                Handle = tcClient.CreateVariableHandle(sWritevariable);
                tcClient.WriteAny(Handle, iValue);
            }
            catch
            {
                Console.WriteLine("Writing Ads data failed: " + "Variablename: " + sWritevariable + ", " + "Value: " + iValue);
            }
            return "Ok";
        }
        public string Write_float_Data_To_Plc(string sWritevariable, float fValue)
        {
            try
            {
                Handle = tcClient.CreateVariableHandle(sWritevariable);
                tcClient.WriteAny(Handle, fValue);
            }
            catch
            {
                Console.WriteLine("Writing Ads data failed: " + "Variablename: " + sWritevariable + ", " + "Value: " + fValue);
            }
            return "Ok";
        }
        public string Write_string_Data_To_Plc(string sWritevariable, string sValue)
        {

            try
            {
                Handle = tcClient.CreateVariableHandle(sWritevariable);
                AdsStream adsStream = new AdsStream(sValue.Length + 1);
                BinaryWriter writer = new BinaryWriter(adsStream, System.Text.Encoding.ASCII);
                writer.Write(sValue.ToCharArray());
                //add terminating zero
                writer.Write('\0');
                tcClient.Write(Handle, adsStream);

            }
            catch (Exception err)
            {
                Console.WriteLine("Writing Ads data failed: " + "Variablename: " + sWritevariable + ", " + "Value: " + sValue);
            }
            return "Ok";
        }

        // READ ADS DATA REQUESTED_________________________________________________________________________________________________________________

        public string Read_string_Data_From_Plc(string sReadvariable)
        {

            try
            {
                Handle = tcClient.CreateVariableHandle(sReadvariable);
                //length of the stream = length of string in sps + 1
                AdsStream adsStream = new AdsStream(31);
                BinaryReader reader = new BinaryReader(adsStream, System.Text.Encoding.ASCII);

                int length = tcClient.Read(Handle, adsStream);
                string result = new string(reader.ReadChars(length));
                //Just to remove carbage
                result = result.Substring(0, result.IndexOf('\0'));
                return result;

            }
            catch (Exception err)
            {

                Console.WriteLine("Writing Ads data failed: " + "Variablename: " + sReadvariable);
                return "";
            }

        }
        public bool Read_bool_Data_From_Plc(string sReadvariable)
        {
            try
            {
                int Handle = tcClient.CreateVariableHandle(sReadvariable);
                bool result = (bool)tcClient.ReadAny(Handle, typeof(bool)); // REAL
                return result;

            }
            catch (Exception err)
            {

                Console.WriteLine("Writing Ads data failed: " + "Variablename: " + sReadvariable);
                return false;
            }
        }
        public int Read_int_Data_From_Plc(string sReadvariable)
        {
          
                try
                {
                    Handle = tcClient.CreateVariableHandle(sReadvariable);
                    int result = (int)tcClient.ReadAny(Handle, typeof(int)); // REAL
                    return result;
                }
                catch (Exception err)
                {
                Console.WriteLine("Writing Ads data failed: " + "Variablename: " + sReadvariable);
                return -1;
                }
            
        }
        public float Read_float_Data_From_Plc(string sReadvariable)
        {

            {
                try
                {
                    int Handle = tcClient.CreateVariableHandle(sReadvariable);
                    float result = (float)tcClient.ReadAny(Handle, typeof(float)); // REAL
                    return result;
                }
                catch (Exception err)
                {
                    Console.WriteLine("Writing Ads data failed: " + "Variablename: " + sReadvariable);
                    return -1;
                }

            }
        }
    }
}
