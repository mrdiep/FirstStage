<template>
  <div class="edit-user-form">
    Edit user {{currentUserEdit.FirstName}}
    <button @click="showEditUserModal(false)">Close</button>
  </div>
</template>
<script>

import { mapState, mapActions } from 'vuex'

export default {
  name: 'EditUserForm',
  props: {},
  methods: {
  },
  computed: mapState({
    currentUserEdit: state => state.userManagement.currentUserEdit
  }),
  methods: mapActions('userManagement', [
    'showEditUserModal'
  ]),
  created () {
    
  }
}
</script>

<style scoped>

</style>
