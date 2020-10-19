import colors from 'vuetify/es5/util/colors';
module.exports = {
  configureWebpack: {
    devtool: 'source-map',
  },
};
const isProd = () => process.env.PROD === 1;
const prodHost = '0.0.0.0';
const localHost = 'localhost';
const prodUrl = 'http://api:80/api';
const devUrl = 'http://localhost:5000/api';

/** @type {import("@nuxt/types").NuxtConfig} */
const config = {
  // Disable server-side rendering (https://go.nuxtjs.dev/ssr-mode)
  ssr: false,

  server: {
    port: 3000, // default: 3000
    host: isProd() ? prodHost : localHost, // default: localhost
  },

  // Global page headers (https://go.nuxtjs.dev/config-head)
  head: {
    titleTemplate: '%s - Group10.UI',
    title: 'Group10.UI',
    meta: [
      { charset: 'utf-8' },
      { name: 'viewport', content: 'width=device-width, initial-scale=1' },
      { hid: 'description', name: 'description', content: '' },
    ],
    link: [{ rel: 'icon', type: 'image/x-icon', href: '/favicon.ico' }],
  },

  // Global CSS (https://go.nuxtjs.dev/config-css)
  css: ['@/assets/css/main.css'],

  // Plugins to run before rendering page (https://go.nuxtjs.dev/config-plugins)
  plugins: [],

  // Auto import components (https://go.nuxtjs.dev/config-components)
  components: true,

  // Modules for dev and build (recommended) (https://go.nuxtjs.dev/config-modules)
  buildModules: [
    // https://go.nuxtjs.dev/eslint
    '@nuxtjs/eslint-module',
    // https://go.nuxtjs.dev/vuetify
    '@nuxtjs/vuetify',
  ],

  // Modules (https://go.nuxtjs.dev/config-modules)
  modules: ['@nuxt/http', '@nuxtjs/axios', '@nuxtjs/auth'],

  axios: {
    proxy: isProd(),
  },
  http: {
    proxy: true,
  },
  proxy: [isProd() ? prodUrl : devUrl],

  router: {
    middleware: ['auth'],
  },

  auth: {
    strategies: {
      local: {
        endpoints: {
          login: {
            url: '/api/account/login',
            method: 'post',
            propertyName: 'token',
          },
          user: {
            url: '/api/account/user',
            method: 'get',
            propertyName: 'user',
          },
          logout: false,
        },
        tokenRequired: true,
        tokenType: 'Bearer',
      },
      google: {
        client_id: process.env.GOOGLE_CLIENT_ID,
      },
    },
    rewriteRedirects: false,
    redirect: {
      home: '/home',
    },
  },

  // Vuetify module configuration (https://go.nuxtjs.dev/config-vuetify)
  vuetify: {
    customVariables: ['~/assets/variables.scss'],
    theme: {
      dark: false,
      themes: {
        dark: {
          primary: colors.blue.darken2,
          accent: colors.grey.darken3,
          secondary: colors.amber.darken3,
          info: colors.teal.lighten1,
          warning: colors.amber.base,
          error: colors.deepOrange.accent4,
          success: colors.green.accent3,
        },
      },
    },
  },

  // Build Configuration (https://go.nuxtjs.dev/config-build)
  build: {},
};
export default config;
