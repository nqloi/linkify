import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  {
    path: '/',
    component: () => import('@/layout/MainLayout.vue'), // Sử dụng MainLayout cho các route này
    children: [
      {
        path: '',
        name: 'Home',
        component: () => import('../views/Home/Home.vue')
      }
    ]
  },
  {
    path: '/auth',
    component: () => import('@/layout/AuthLayout.vue'),
    children: [
      {
        path: 'login',
        name: 'Login',
        component: () => import('@/views/Login/Login.vue')
      },
      {
        path: 'register',
        name: 'Register',
        component: () => import('@/views/Login/Login.vue')
      }
    ]
  }
  // Các route khác nếu cần
]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes
})

export default router
