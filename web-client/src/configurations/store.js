import Vue from 'vue';
import Vuex from 'vuex';

import userManagement from '../modules/user-management/store';
import roleManagement from '../modules/role-management/store';

import login from '../modules/login/store';

Vue.use(Vuex)

const debug = process.env.NODE_ENV !== 'production'

export default new Vuex.Store({
  modules: {
    userManagement,
    roleManagement,
    login
  },
  strict: debug,
  plugins: []
})