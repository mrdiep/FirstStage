<!-- Router.vue -->
<template>
  <component :is="routedComponent"></component>
</template>


<script>

import UserManagementPage from './modules/user-management/components/container';
import RoleManagementPage from './modules/role-management/components/container';

import HelloWorld from './components/HelloWorld.vue'
import LoginPage from './modules/login/components/container'

const routes = {
  "/": HelloWorld,
  "/user-management": UserManagementPage,
  "/role-management": RoleManagementPage,
  "/login": LoginPage
};

export default {
  data() {
    return { current: window.location.pathname };
  },
  computed: {
    routedComponent() {
      return routes[this.current];
    }
  },
  mounted: function () {
    this.$store.dispatch('login/fetchAccessToken')
  },
  render(createElement) {
    return createElement(this.routedComponent);
  }
};
</script>
