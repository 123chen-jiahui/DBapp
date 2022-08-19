<template>
  <v-navigation-drawer
    id="default-drawer"
    v-model="drawer"
    :dark="dark"
    :right="$vuetify.rtl"
    :src="drawerImage ? image : ''"
    :mini-variant.sync="mini"
    mini-variant-width="80"
    app
    width="260"
  >
    <template
      v-if="drawerImage"
      #img="props"
    >
      <v-img
        :key="image"
        :gradient="gradient"
        v-bind="props"
      />
    </template>

    <div class="px-2">
      <default-drawer-header />

      <v-divider class="mx-3 mb-2" />

      <default-list :items="JudgeAccountType(items)" />
    </div>

    <!-- <template #append>
      <div class="pa-4 text-center">
        <app-btn
          class="text-none mb-4"
          color="white"
          href="https://vuetifyjs.com"
          small
          text
        >
          Documentation
        </app-btn>

        <app-btn
          block
          class="text-none"
          color="secondary"
          href="https://store.vuetifyjs.com/products/vuetify-material-dashboard-pro"
        >
          <v-icon left>
            mdi-package-up
          </v-icon>

          Upgrade to Pro
        </app-btn>
      </div>
    </template> -->

    <div class="pt-12" />
  </v-navigation-drawer>
</template>

<script>
  // Utilities
  import { get, sync } from 'vuex-pathify'
  import jwtDecode from 'jwt-decode'

  export default {
    name: 'DefaultDrawer',

    components: {
      DefaultDrawerHeader: () => import(
        /* webpackChunkName: "default-drawer-header" */
        './widgets/DrawerHeader'
      ),
      DefaultList: () => import(
        /* webpackChunkName: "default-list" */
        './List'
      ),
    },

    computed: {
      ...get('user', [
        'dark',
        'gradient',
        'image',
      ]),
      ...get('app', [
        'items',
        'version',
        'jwt',
      ]),
      ...sync('app', [
        'drawer',
        'drawerImage',
        'mini',
      ]),
    },

    methods: {
      JudgeAccountType (item) {
        const decode = jwtDecode(this.jwt)
        const prop = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
        const role = decode[prop]
        // console.log(decode[prop])
        // 各个用户索引
        const PatientItem = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
        const AdminItem = [0, 1, 3]
        const DoctorItem = [0, 1, 4]
        const MedicineTokenItem = [0, 1, 5]
        const ret = []
        // 赋值
        if (role === 'Patient') {
          PatientItem.forEach(function (ele) {
            ret.push(item[ele])
          })
        } else if (role === 'Admin') {
          AdminItem.forEach(function (ele) {
            ret.push(item[ele])
          })
        } else if (role === 'Doctor') {
          DoctorItem.forEach(function (ele) {
            ret.push(item[ele])
          })
        } else if (role === 'Medicine') {
          MedicineTokenItem.forEach(function (ele) {
            ret.push(item[ele])
          })
        } else {
          ret.push(item[item.length - 1])
        }
        return ret
      },
    },
  }
</script>

<style lang="sass">
#default-drawer
  .v-list-item
    margin-bottom: 8px

  .v-list-item::before,
  .v-list-item::after
    display: none

  .v-list-group__header__prepend-icon,
  .v-list-item__icon
    margin-top: 12px
    margin-bottom: 12px
    margin-left: 4px

  &.v-navigation-drawer--mini-variant
    .v-list-item
      justify-content: flex-start !important
</style>
