using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.Ads.TypeSystem;
using TwinCAT.TypeSystem;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json;
using System.Runtime.Remoting.Messaging;
using System.Linq.Expressions;
using TwinCAT.PlcOpen;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;

namespace Websocket_Ads
{
    internal class DataIn
    {
        public bool bResult;
        public float fResult;
        public int iResult;
        public string sResult;
        public string sType;
     

        Ads ads = new Ads();
        public string ParseIncomingData(string sDataIn)  // Parsing without Json structure
        {
            // string sDataIn = "abc,def,10";
            string osDatatype = sDataIn.Split(',')[0]; //Extracting Datatype out of the string
            string osAux = sDataIn.Substring(osDatatype.Length + 1);
            string osVariableName = osAux.Split(',')[0]; 
            string osValue = (osAux.Substring(osVariableName.Length + 1)); 
            Int32.TryParse(osValue, out int iValue);
            return "Ok";
        }
        public string FindValueType(string sDataIn)  // Check which variable type is required to handle
        {
            string sModifiedDataIn = "";
            if (sDataIn.Contains("bool")) { sModifiedDataIn = sDataIn.Replace("Value", "bValue");  }
            if (sDataIn.Contains("int")) { sModifiedDataIn = sDataIn.Replace("Value", "iValue"); }
            if (sDataIn.Contains("float")) { sModifiedDataIn = sDataIn.Replace("Value", "fValue"); }
            if (sDataIn.Contains("string")) { sModifiedDataIn = sDataIn.Replace("Value", "sValue"); }

            return sModifiedDataIn;
        }

            public bool ParseIncomingDataJson(string sDataIn) // Parsing json format file
        {
            try 
            { 
                Helper result = JsonConvert.DeserializeObject<Helper>(sDataIn);
                if(result.AccessType.Equals("write"))
                    switch (result.VariableType)
                    {
                        case "bool": ads.Write_boolData_To_Plc(result.VariableName, result.bValue);
                            break;

                        case "int":
                            ads.Write_uintData_To_Plc(result.VariableName, result.iValue);
                            break;
                        case "float":
                            ads.Write_float_Data_To_Plc(result.VariableName, result.fValue);
                            break;
                        case "string":
                            ads.Write_string_Data_To_Plc(result.VariableName, result.sValue);
                            break;
                        default: return false;
                    }
                if (result.AccessType.Equals("read"))
                {
                    switch (result.VariableType)
                    {
                        case "bool":
                            bResult = ads.Read_bool_Data_From_Plc(result.VariableName); sType = "bool";
                            break;

                        case "int":
                            iResult = ads.Read_int_Data_From_Plc(result.VariableName);  sType = "int";
                            break;
                        case "float":
                           fResult = ads.Read_float_Data_From_Plc(result.VariableName); sType = "float";
                            break;
                        case "string":
                            sResult = ads.Read_string_Data_From_Plc(result.VariableName); sType = "string";
                            break;
                    }
                }

                    return true;
            }

            catch(Exception e)
            {
                
            }
            return false;
        }
    }
}
