import axios from 'axios';
import * as Api from '../services/api';
import { debug } from 'util';

export async function fetchRoles({ commit, state }, query) {
  const dataResponse = await axios.get('https://localhost:5001/odata/AppRoles?$select=RoleName,DisplayName,Category,Id', { headers: { Authorization: 'Bearer ' + localStorage.accessToken } });
  if (dataResponse.status === 200) {
    commit('loadRoles', dataResponse.data.value);
  }
}

export function addRole({ commit }, role) {
  commit('pushRoleToList', role);
}

export async function openEditBasicModal({ commit }, role) {
  commit('setEditModal', 'basicForm');
  commit('setEditRole', role);
}

export async function openEditPermissionRoleModal({ commit }, role) {
  const allPermissions = await axios.get('https://localhost:5001/odata/AppPermissions', { headers: { Authorization: 'Bearer ' + localStorage.accessToken } });
  const dataResponse = await axios.get(`https://localhost:5001/odata/AppRoles(${role.id})/AppRolePermissions?$expand=permission`, { headers: { Authorization: 'Bearer ' + localStorage.accessToken } });
  if (dataResponse.status === 200) {
    var selectedSermissions = dataResponse.data.value.map(x => x.permission);
    const permissions = allPermissions.data.value.map(x => {
      x.isSelected = selectedSermissions.some(t => t.id === x.id);
      return x;
    })

    commit('setPermissions', { role, permissions});
    commit('setEditModal', 'permissionForm');
    commit('setEditRole', role);
  }
}

export async function savePermission({ getters }) {
  const model = getters.getPostPermissionModel;
  await axios.post(`https://localhost:5001/api/command`, {
    commandName: 'UpdatePermissionsForRoleCommand',
    commandData: model
  } ,{ headers: { Authorization: 'Bearer ' + localStorage.accessToken } });
}

export async function saveBasicInfo({state}) {
  const model = state.currentRoleEdit;

  await axios.post(`https://localhost:5001/api/command`, {
    commandName: 'UpdatePermissionsForRoleCommand',
    commandData: model
  } ,{ headers: { Authorization: 'Bearer ' + localStorage.accessToken } });

}

export async function requestCreateNewRole({state}) {

 var result = await Api.executeCommand('AddNewRoleCommand', state.newRole);
 debugger;
}

export function openAddRoleModal({ commit }) {
  commit('setEditModal', 'newRole');
  commit('createNewRole')
}

export function showEditRoleModal({ commit }, modalName) {
  commit('setEditModal', modalName);
  if (!modalName) {
    commit('setEditRole', null);
  }
}