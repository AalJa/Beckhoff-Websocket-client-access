// READ FROM BECKHOFF VARIABLES - EXAMPLES> Program.Variable name has to exist in PLC and has to be corresponding type
// If variable access is succesful server will echo same message back + the valuefield with variable value.



//FORMAT OF READING  VARIABLES

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

FORMAT OF SUCCESSFUL READ RESPONSES

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

// WRITE TO BECKHOFF VARIABLES - EXAMPLES> Variable name has to exist in PLC and has to be corresponding type
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