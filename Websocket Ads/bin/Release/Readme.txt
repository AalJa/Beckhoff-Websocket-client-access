!!!CONFIGURATING Websocketserver

SocketData.txt
Example of local connection:             "ws://Localhost:7890"  
Example of global connection:           "ws://192.168.10.102:7890

!!!CONFIGURATING Beckhoff ads server
ADS.txt
127.0.0.1.1.1,851      

!!!USING Websocket Ads software

Copy Websocket Ads.exe program folder to same computer in which twincat  is running
Start Websocket Ads.exe


!!!AFTER SUCCESSFUL CONNECTION BELOW MESSAGES CAN BE SEEN IN CONSOLE WINDOW

Client Connected to Websocketserver:  ws://Localhost:7890  16/04/2023 15:08:18
Client connected to Ads server: IP: 127.0.0.1.1.1, Port: 851  16/04/2023 15:08:18

!!!EXAMPLES -> Variable name has to exist in PLC and has to be corresponding type

READ FROM BECKHOFF VARIABLES - EXAMPLES> Program."VariableName" has to exist in PLC and has to be corresponding type
// If variable access is succesful server will echo same message back + the valuefield with variable value.

//FORMAT OF READING SEND MESSAGES

{
  "AccessType": "read",
  "VariableType": "int",
  "VariableName": "HMI.iTest",
}

{
  "AccessType": "read",
  "VariableType": "bool",
  "VariableName": "HMI.bTest",
}

{
  "AccessType": "read",
  "VariableType": "float",
  "VariableName": "HMI.fTest",
}

{
  "AccessType": "read",
  "VariableType": "string",
  "VariableName": "HMI.sTest",
}

FORMAT OF SUCCESSFUL READ RESPONSES FROM SOCKET SERVER

{
  "AccessType": "read",
  "VariableType": "int",
  "VariableName": "HMI.iTest",
  "Value": 101,
}

{
  "AccessType": "read",
  "VariableType": "bool",
  "VariableName": "HMI.bTest",
  "Value": "FALSE",
}

{
  "AccessType": "read",
  "VariableType": "float",
  "VariableName": "HMI.fTest",
  "Value": 1001,
}

{
  "AccessType": "read",
  "VariableType": "string",
  "VariableName": "HMI.sTest",
  "Value": "John",
}

// WRITE TO BECKHOFF VARIABLES - EXAMPLES> 
// If variable access is succesful server will echo the same message back

{
  "AccessType": "read",
  "VariableType": "int",
  "VariableName": "HMI.fTest",
  "Value": 11,
}

{
  "AccessType": "read",
  "VariableType": "bool",
  "VariableName": "HMI.bTest",
  "Value": "TRUE",
}

{
  "AccessType": "read",
  "VariableType": "float",
  "VariableName": "HMI.fTest",
  "Value": 1000,
}

{
  "AccessType": "read",
  "VariableType": "string",
  "VariableName": "HMI.sTest",
  "Value": "Test",
}

FORMAT OF SUCCESFUL WRITE RESPONSES

{
  "AccessType": "read",
  "VariableType": "int",
  "VariableName": "HMI.iTest",
  "Value": 11,
}

{
  "AccessType": "read",
  "VariableType": "bool",
  "VariableName": "HMI.bTest",
  "Value": "TRUE",
}

{
  "AccessType": "read",
  "VariableType": "float",
  "VariableName": "HMI.fTest",
  "Value": 1000,
}

{
  "AccessType": "read",
  "VariableType": "string",
  "VariableName": "HMI.sTest",
  "Value": "Test",
}