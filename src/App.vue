<template>
  <v-fade-transition mode="out-in">
    <router-view />
  </v-fade-transition>
</template>

<script>
  // Styles
  import '@/styles/overrides.sass'

  export default {
    name: 'App',
    metaInfo: {
      title: 'App',
      titleTemplate: '医院管理系统',
      htmlAttrs: { lang: 'ch' },
      meta: [
        { charset: 'utf-8' },
        { name: 'viewport', content: 'width=device-width, initial-scale=1' },
      ],
    },
  }
</script>
<style>
body,html{
  padding: 0;
  margin: 0;
  background-color: #c5d5d0;
}
</style>
