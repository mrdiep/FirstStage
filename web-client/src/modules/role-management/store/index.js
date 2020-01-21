import state from './state';
import * as actions from './actions';
import * as getters from './getters';
import * as mutations from './mutations';

import { getField, updateField } from 'vuex-map-fields';

export default {
  namespaced: true,
  state,
  getters: {...getters, getField},
  actions,
  mutations: {...mutations, updateField}
}
