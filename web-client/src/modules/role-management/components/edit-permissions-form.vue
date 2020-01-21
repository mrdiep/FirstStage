<template>
  <div>
    Edit Permission {{currentRoleEdit.roleName}}

    <p>LIST PERMISSION</p>
    <div v-for="(value, name) in permissionCategories" :key="name">
      <label class="category-label">{{name}}</label>
        <b-form-checkbox
          v-for="(permission) in value"
          :key="permission.permissionName"
          @input="handleRoleChecker(permission.permissionName,$event)"
          :checked="permission.isSelected">
          {{permission.displayName}}
        </b-form-checkbox> 
    </div>
    <div class="button-group">
      <b-button size="sm" @click="savePermission()" variant="outline-primary">Save</b-button>
      <b-button size="sm" @click="showEditRoleModal(null)" variant="outline-primary">Close</b-button>
    </div>
  </div>
</template>
<script>

import { mapState, mapActions, mapGetters, mapMutations } from 'vuex'

export default {
  name: 'EditPermissionsForm',
  props: {},
  computed: {
    ... mapState({
      currentRoleEdit: state => state.roleManagement.currentRoleEdit,
    }), 
    ...mapGetters("roleManagement", { permissionCategories: "getPermissionCategories" })},
  methods: {
      ...mapActions('roleManagement', [ 'showEditRoleModal', 'savePermission']),
      ...mapMutations('roleManagement',['setPermission']),
      handleRoleChecker: function(permissionName, value) {
        this.setPermission({permissionName, value});
    }},
  created () {
    
  }
}
</script>

<style scoped>
.category-label {
  font-size: 14px;
  font-weight: bold;
  margin-top: 15px;
}

.button-group {
  display: flex;
  margin-top:20px;
}
</style>
