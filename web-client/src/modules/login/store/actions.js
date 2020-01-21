import axios from 'axios';
export async function doLogin({commit}, {username, password}) {
  var data = await axios.post(`https://localhost:5001/api/auth`, { username, password });

  localStorage.removeItem('accessToken');
  if (data.status === 200) {
    commit('setToken', data.data.accessToken);
    localStorage.setItem('accessToken', data.data.accessToken);
  }
}

export function fetchAccessToken({ commit }) {
  commit('setToken', localStorage.getItem('accessToken'));
}