import axios from 'axios';

export async function fetchUsers({ commit, state }, query) {
  const usersResponse = await axios.get('https://localhost:5001/api/roles', { headers: { Authorization: 'Bearer ' + localStorage.accessToken } });
  if (usersResponse.status === 200) {
    commit('loadUser', usersResponse.data);
  }
}

export function addUser({ commit }, user) {
  commit('pushUserToList', { user });
}


export function openEditUserModal({ commit }, user) {
  commit('setEditModal', true);
  commit('setEditUser', user);
}


export function showEditUserModal({ commit }, isShow) {
  commit('setEditModal', isShow)
}