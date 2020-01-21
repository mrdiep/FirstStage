<template>
  <div class="user-management">
    UserManagement
    <UserList></UserList>
    <EditUserForm v-if="isShowEditModal"></EditUserForm>
  </div>
</template>

<script>

import { mapState, mapActions, mapGetters } from 'vuex'

import UserList from './user-list.vue'
import EditUserForm from './edit-form.vue'

export default {
  name: 'UserManagementContainer',
  props: {},
  methods: {},
  computed: mapState({
    isShowEditModal: state => state.userManagement.isShowEditModal
  }),
  created() {
    this.$store.dispatch('userManagement/fetchUsers', {pageSize: 10, pageIndex: 1})
  },
  components: { UserList, EditUserForm }
}
</script>

<style scoped>

</style>
