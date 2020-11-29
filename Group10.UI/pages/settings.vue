<template>
  <v-container fill-height fluid>
    <v-row justify="center" align="center">
      <v-card outlined elevation="4" justify="center" class="ma-2 pa-5">
        <v-card-title> Settings </v-card-title>
        <v-divider />
        <v-switch v-model="$vuetify.theme.dark" inset label="Dark Mode" />
        <v-select
          v-if="this.$store.state.admin.isAdmin === true"
          v-model="accountType"
          label="Account Type"
          dense
          filled
          :items="items"
          persistent-hint
          solo
        ></v-select>
        <v-btn
          v-if="this.$store.state.admin.isAdmin === true"
          rounded="true"
          color="#858988"
          class="mr-4"
          @click="submit"
          :disabled="accountType === 0"
        >
          Submit
        </v-btn>
      </v-card>
    </v-row>
  </v-container>
</template>

<script>
export default {
  data() {
    return {
      items: ['Driver', 'Sponsor'],
      accountType: 0,
    };
  },
  methods: {
    submit() {
      const updatedUser = { ...this.$auth.user };
      updatedUser.role = this.accountType;
      this.$auth.setUser(updatedUser);
    },
  },
};
</script>
