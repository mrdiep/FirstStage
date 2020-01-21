<template>
  <div class="user-management">
    <LoginForm class="login-form"></LoginForm>
  </div>
</template>
<script>

import LoginForm from './login-form.vue'

export default {
  name: 'LoginContainer',
  components: { LoginForm }
}
</script>

<style scoped>
.login-form {
  margin: 60px auto;
      border: 1px solid #0099e2;
    border-radius: 10px;
    padding: 20px;
}
</style>
