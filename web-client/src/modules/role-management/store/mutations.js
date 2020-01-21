export function createNewRole(state) {
    state.newRole = {
        roleName: 'new name',
        displayName: '',
        category: '',
        isEnabled: true 
    };
}

export function setEditModal (state, modalName) {
    state.showEditModal = modalName;
}

export function setEditRole (state, role) {
    state.currentRoleEdit = role;
}

export function setPermissions(state, { role, permissions }) {
    state.roles.find(x => x.id === role.id).permissions = permissions;
}

export function loadRoles (state, roles) {
    state.roles = roles;
}

export function setPermission(state, { permissionName, value }) {
    state.currentRoleEdit.permissions.find(x => x.permissionName == permissionName).isSelected = value;
}