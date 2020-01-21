<template>
    <b-form-group
    v-bind:label="label"
    v-bind:label-for="labelFor">

    <b-form-input
      v-bind:id="labelFor"
      size="sm"
      v-bind:value="value"
      @input="handleInput"
      v-bind:placeholder="label">
    </b-form-input>
    <p v-if="showRequiredError">This field is required.</p>
</b-form-group>
</template>

<script>

export default {
  name: 'InputField',
  props: {
    label: String,
    className: String,
    value: String,
    isRequired: Boolean
  },
  methods: {
    handleInput (e) {
      this.$emit('input',e)
    }
  },
  computed: {
    labelFor: function() {
      return "input-" + this.className;
    },
    showRequiredError: function() {
      console.log(this.value)
       return !this.value;
     }
  }
}
</script>

<style scoped>

</style>
