<template>
    <b-container fluid class="bv-example-row">
      <b-row>
        <b-col class="justify-content" cols="4">
          <RoleList></RoleList>
        </b-col>
        <b-col cols="8">
          <EditPermissionsForm v-if="isShowPermissionForm"></EditPermissionsForm>
          <EditBasicRoleForm v-if="isShowBasicRoleForm"></EditBasicRoleForm>
          <AddRoleForm v-if="isShowAddRoleForm"></AddRoleForm>
        </b-col>
      </b-row>
    </b-container>
</template>

<script>

import { mapState } from 'vuex'

import RoleList from './role-list.vue'
import EditPermissionsForm from './edit-permissions-form.vue'
import EditBasicRoleForm from './edit-basic-role-form.vue'
import AddRoleForm from './add-role-form.vue'

export default {
  name: 'RoleManagementContainer',
  props: {},
  methods: {},
  computed: mapState({
    isShowPermissionForm: state => state.roleManagement.showEditModal === 'permissionForm',
    isShowBasicRoleForm: state => state.roleManagement.showEditModal === 'basicForm',
    isShowAddRoleForm: state => state.roleManagement.showEditModal === 'newRole'
  }),
  created() {
    this.$store.dispatch('roleManagement/fetchRoles', {pageSize: 10, pageIndex: 1})
  },
  components: { RoleList, EditBasicRoleForm, EditPermissionsForm, AddRoleForm }
}
</script>

<style scoped>

</style>
