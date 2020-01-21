<template>
  <div class='role-list'>
     <b-button size="sm" class="button-holder" @click.prevent="openAddRoleModal()" variant="link">Add Role</b-button>
    <b-list-group>
      <b-list-group-item
        v-bind:active='role === currentRoleEdit'
        v-for='role in roles'
        :key='role.Id'
        href='#some-link'>
        <div class='list-item'>
        {{ role.displayName }}
        <v-icon name='chevron-right'/>
        </div>
        
        <div class="button-holder">
          <b-button size="sm" class="button-holder" @click.prevent="openEditPermissionRoleModal(role)" variant="link">Edit Permission</b-button>
          <b-button size="sm" class="button-holder" @click.prevent="openEditBasicModal(role)" variant="link">Edit Basic Info</b-button>
        </div>
        
      </b-list-group-item>
    </b-list-group>
  </div>  
</template>

<script>
import { mapState, mapActions } from 'vuex';

export default {
  name: 'RoleList',
  props: {},
  computed: mapState({
    roles: state => state.roleManagement.roles,
    currentRoleEdit: state => state.roleManagement.currentRoleEdit
  }),
  methods: mapActions('roleManagement', ['openEditPermissionRoleModal', 'openEditBasicModal', 'openAddRoleModal']),
  created() {
    
  },
  components: {
    
  }
};
</script>

<style scoped>
.list-item {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
}

.active button {
  color: white
}
</style>
