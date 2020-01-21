<template>
  <b-form-checkbox
    :id="labelFor"
    :checked="value"
    @input="handleInput($event)"
    :name="labelFor">
    {{label}}
  </b-form-checkbox>
</template>

<script>

export default {
  name: 'CheckBoxField',
  props: {
    label: String,
    className: String,
    value: Boolean,
  },
  methods: {
    handleInput (e) {
      this.$emit('input',e)
    }
  },
  computed: {
    labelFor: function() {
      return "input-" + this.className;
    }
  }
}
</script>

<style scoped>

</style>
