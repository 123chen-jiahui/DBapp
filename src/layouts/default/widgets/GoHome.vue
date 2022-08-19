<template>
  <v-btn
    class="ml-2"
    min-width="0"
    text
    exact
    @click="GoHome"
  >
    <v-icon>mdi-view-dashboard</v-icon>
  </v-btn>
</template>

<script>
  export default {
    name: 'DefaultGoHome',
    methods: {
      GoHome () {
        this.$router.push({ name: 'Dashboard' })
      },
    },
  }
</script>
