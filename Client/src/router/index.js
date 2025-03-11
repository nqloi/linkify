import { useAuthStore } from '@/stores/authStore'
import { createRouter, createWebHistory, NavigationFailureType } from 'vue-router'

const routes = [
    {
        path: '/',
        component: () => import('@/layout/MainLayout.vue'),
        children: [
            {
                path: '',
                name: 'Home',
                component: () => import('../views/home/Home.vue'),
            },
            {
                path: '1',
                name: 'Home1',
                component: () => import('../views/home/Home.vue'),
            },
            {
                path: '2',
                name: 'Home2',
                component: () => import('../views/home/Home.vue'),
            },
        ],
        meta: {
            // isPublicPage: true
        },
    },
    {
        path: '/auth',
        component: () => import('@/layout/AuthLayout.vue'),
        children: [
            {
                path: 'login',
                name: 'Login',
                component: () => import('@/views/auth/Login.vue'),
            },
            {
                path: 'register',
                name: 'Register',
                component: () => import('@/views/auth/Register.vue'),
            },
            {
                path: '',
                redirect: '/404',
            },
        ],
        meta: {
            isPublicPage: true,
        },
    },
    {
        path: '/404',
        name: 'NotFound',
        component: () => import('@/views/common/NotFound.vue'),
        meta: {
            isPublicPage: true,
        },
    },
    {
        path: '/profile/:username',
        name: 'Profile',
        component: () => import('@/views/profile/Profile.vue'),
        meta: {
            isPublicPage: false,
        },
    },
]

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes,
})

router.beforeEach((to, from, next) => {
    // if (!to.matched?.length) {
    //   next('/404')
    // }

    const { isAuthenticated } = useAuthStore()

    if (to.matched.find((r) => r.path == '/auth') && isAuthenticated) {
        next('/')
    }

    if (to.meta.isPublicPage || isAuthenticated) {
        next()
    } else {
        next('auth/login')
    }
})

export default router
