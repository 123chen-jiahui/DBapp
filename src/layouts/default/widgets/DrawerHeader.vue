<template>
  <div v-if="!mini">
    <v-list-item class="mb-0 justify-space-between pl-3">
      <!-- <v-list-item-avatar>
      <v-img
        :src="
          require('@/assets/vmd.svg')"
      />
    </v-list-item-avatar> -->
      <v-list-item-content class="pl-2">
        <v-list-item-title class="text-center text-h3">
          <strong class="font-weight-black">医院管理系统</strong>
        <!-- <span class="primary--text">管理</span> -->
        </v-list-item-title>
      </v-list-item-content>
    </v-list-item>
  </div>
</template>

<script>
  // Utilities
  import { get, sync } from 'vuex-pathify'

  export default {
    name: 'DefaultDrawerHeader',

    computed: {
      version: get('app/version'),
      mini: sync('app/mini'),
    },
  }
</script>
