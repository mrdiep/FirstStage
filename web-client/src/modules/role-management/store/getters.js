import _ from 'lodash';

export function getPostPermissionModel(state) {
  if (!state.currentRoleEdit || !state.currentRoleEdit.permissions) return null;

  const permissions = state.currentRoleEdit.permissions;
  const permissionIds = permissions.filter(x => x.isSelected).map(x => x.id)
  const saveModel = {
   roleId: state.currentRoleEdit.id,
   permissionIds
 };

 return saveModel;
}

export function getPermissionCategories(state) {
  if (!state.currentRoleEdit || !state.currentRoleEdit.permissions)
    return [];

  return _.groupBy(state.currentRoleEdit.permissions, x => x.category);
}