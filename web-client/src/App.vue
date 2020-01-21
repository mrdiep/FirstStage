<template>
  <div id="app">
    <app-router></app-router>
  </div>
</template>

<script>
import './style.scss'
import Vue from 'vue'
import BootstrapVue from 'bootstrap-vue'
import 'vue-awesome/icons';
import Icon from 'vue-awesome/components/Icon'
import InputField from './components/InputFieldComponent'
import CheckBoxField from './components/CheckBoxFieldComponent'

// globally (in your main .js file)
Vue.component('v-icon', Icon)
Vue.use(BootstrapVue)
Vue.component('InputField', InputField)
Vue.component('CheckBoxField', CheckBoxField)

import AppRouter from "./AppRouter";
export default {
  name: 'app',
  components: {
    AppRouter
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}
</style>
