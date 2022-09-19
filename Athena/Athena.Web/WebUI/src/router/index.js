import Vue from 'vue'
import Router from 'vue-router'
import Backlog from '@/views/backlog/Backlog.vue'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Backlog',
      component: Backlog
    }
  ]
})
