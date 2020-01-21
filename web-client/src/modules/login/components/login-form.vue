<template>
  <b-form class="login-form">
    <h4 class="text-center">LOGIN</h4>
    <b-form-group
        label="Email address:"
        label-for="input-1"
        description="We'll never share your email with anyone else."
      >
      <b-form-input
          id="input-1"
          size="sm"
          v-model="username"
          type="email"
          required
          placeholder="Enter Email"
        ></b-form-input>
      </b-form-group>

      <b-form-group label="Password:" label-for="input-password">
        <b-input size="sm" type="password" id="input-password" aria-describedby="password-help-block"></b-input>
        <b-form-text id="password-help-block">
          Your caplock is on.
        </b-form-text>
   </b-form-group>
  <b-form-checkbox
        id="checkbox-remember-me"
        v-model="status"
        name="checkbox-remember-me"
      >
        Remember me
      </b-form-checkbox>
    <b-button class="btn-login"  @click="doLogin({ username, password})" variant="outline-primary" size="sm">Login</b-button>
    <p>{{!!token ? "Login success. Redirect now" : 'Error when login'}}</p>
  </b-form>
</template>
<script>

import { mapState, mapActions } from 'vuex'

export default {
  name: 'LoginForm',
  props: {},
  data: function () {
    return {
      username: 'user1',
      password: '123456',
      rememberMe: false
    }
  },
  computed: mapState({
    token: state => state.login.token
  }),
  methods: mapActions('login', [
    'doLogin'
  ]),
 
}
</script>

<style scoped>
.login-form {
  width: 400px;
}

.btn-login {
 margin-top: 20px;
}
</style>
