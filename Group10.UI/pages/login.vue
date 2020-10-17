<template>
  <v-container class="center">
    <navbar />
    <v-card class="center-text center-card pa-4" max-width="400">
      <v-card-title class="justify-center"> Login with Google </v-card-title>
      <v-card class="center-card" elevation="0" color="#FFFFFF" max-width="50">
        <v-btn icon @click="login">
          <v-icon x-large color="#000000" text icon> mdi-google </v-icon>
        </v-btn>
      </v-card>
    </v-card>
  </v-container>
</template>

<script>
export default {
  data() {
    return {
      valid: true,
      user: this.$auth.user,
    };
  },

  methods: {
    test() {
      debugger;
    },
    async login() {
      await this.$auth.loginWith('google');
    },
    async login2() {
      this.$store.commit('authStore/setRegisterStatus', true);
      const foo = this.$store.state.authStore.isRegistering;
      console.log(foo);
      debugger;
      await this.$auth.loginWith('google');
    },
  },
};
</script>
