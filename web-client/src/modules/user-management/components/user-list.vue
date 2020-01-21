<template>
  <div class="user-list">
    <div>
      <div v-for="user in users" :key="user.Id">
        {{ user.FirstName }}
        <br>
        <button @click="openEditUserModal(user)">Edit</button>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState, mapActions } from "vuex";

export default {
  name: "UserList",
  props: {},
  methods: {},
  computed: mapState({
    users: state => state.userManagement.users
  }),
  methods: mapActions("userManagement", ["openEditUserModal"]),
  created() {
    // this.$store.dispatch('products/getAllProducts')
  }
};
</script>

<style scoped>
</style>
