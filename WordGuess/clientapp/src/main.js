import Vue from 'vue'
import axios from './plugins/axios'
import App from './App.vue'
import vuetify from './plugins/vuetify'
import global from './plugins/global'

Vue.config.productionTip = false

new Vue({
  vuetify,
  axios,
  global,
  render: h => h(App)
}).$mount('#app')
