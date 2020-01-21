import axios from 'axios';

export function executeCommand(commandName, commandData) {
  return axios.post(`https://localhost:5001/api/command`, {
    commandName,
    commandData
  } ,{ headers: { Authorization: 'Bearer ' + localStorage.accessToken } })
}