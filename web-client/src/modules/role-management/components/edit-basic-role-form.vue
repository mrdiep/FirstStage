<template>
  <b-form>
    <h4 class="text-center">EDIT ROLE {{displayName.toUpperCase()}}</h4>

    <InputField label="Role Name" className="role-name" v-model="roleName"></InputField>
    <InputField label="Display Name" className="display-name"  v-model="displayName" ></InputField>
    <InputField label="Category" className="category-name"  v-model="category"></InputField>
    <CheckBoxField label="Enabled" v-model="isEnabled"></CheckBoxField>

    <b-button size="sm" @click="saveBasicInfo()" variant="outline-primary">Save</b-button>
    <b-button size="sm" @click="showEditRoleModal(null)" variant="outline-primary">Close</b-button>
  </b-form>
</template>
<script>

import { mapState, mapActions, mapGetters } from 'vuex'

import { createHelpers } from 'vuex-map-fields';

const { mapFields } = createHelpers({
  getterType: `roleManagement/getField`,
  mutationType: `roleManagement/updateField`,
});

export default {
  name: 'EditBasicRoleForm',
  props: {},
  computed: {
    ...mapFields([
      "currentRoleEdit.roleName",
      "currentRoleEdit.displayName",
      "currentRoleEdit.category",
      "currentRoleEdit.isEnabled"
    ])
  },
  methods: mapActions('roleManagement', ['showEditRoleModal', 'saveBasicInfo', 'changeBasicInfo']),
  created () {
    
  }
}
</script>

<style scoped>

</style>
