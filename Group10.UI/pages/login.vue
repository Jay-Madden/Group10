<template>
  <v-container class="center">
    <navbar />
    <v-card class="center-text center-card pa-4" max-width="400">
      <v-card-title class="justify-center"> Login with Google </v-card-title>
      <v-btn icon x-large outlined @click="login" elevation="2">
        <v-icon x-large color="#000000" text icon> mdi-google </v-icon>
      </v-btn>
    </v-card>
  </v-container>
</template>

<script>
export default {
  layout: 'login',
  methods: {
    async login() {
      await this.$auth.loginWith('google');
    },
  },
};
</script>
