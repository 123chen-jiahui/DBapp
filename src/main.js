import Vue from 'vue'
import App from './App.vue'
import router from './router'
import vuetify from './plugins/vuetify'
import './plugins'
import store from './store'
import { sync } from 'vuex-router-sync'
// import axios from 'axios'

Vue.config.productionTip = false
// axios.post('/auth/login_patient', {
//   Id: 1000000,
//   Password: '12345678',
// })
//   .then(function (response) {
//     console.log(response.data)
//     console.log(response)
//   })
//   .catch(function (error) {
//     if (error.response.status === 400) {
//       alert(error.response.data)
//     } else {
//       alert(error.message)
//     }
//     console.log(error)
//   })

sync(store, router)

new Vue({
  router,
  vuetify,
  store,
  render: h => h(App),
}).$mount('#app')
