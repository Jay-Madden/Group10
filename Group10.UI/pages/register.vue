<template>
  <div class="center">
    <v-card :elevation="24" class="mx-auto" min-width="400">
      <v-card-title class="justify-center">Account Registration</v-card-title>
      <v-form ref="form" v-model="valid" lazy-validation>
        <v-col cols="12" sm="12" md="12">
          <p class="font-weight-bold">Name:</p>
          {{ this.$auth.user.name }}
        </v-col>
        <v-col cols="12" sm="12" md="12">
          <p class="font-weight-bold">Email:</p>
          {{ this.$auth.user.email }}
        </v-col>
        <v-col cols="12" sm="12" md="12">
          <v-select
            v-model="accountType"
            label="Account Type"
            dense
            filled
            :items="items"
            persistent-hint
            solo
          ></v-select>
        </v-col>
        <v-flex text-center style="padding-left: 15px; padding-bottom: 15px">
          <v-btn
            rounded="true"
            color="#858988"
            class="mr-4"
            @click="submit"
            :disabled="accountType === 0"
          >
            Submit
          </v-btn>
        </v-flex>
      </v-form>
    </v-card>
  </div>
</template>

<script>
export default {
  layout: 'login',
  data() {
    return {
      items: ['Admin', 'Driver', 'Sponsor'],
      accountType: 0,
    };
  },
  methods: {
    async submit() {
      await this.$http.$post('api/account/register', {
        AccessToken: this.$auth.getToken(this.$auth.strategy.name).substr(7),
        UserRole: this.accountType,
        GoogleUserInfo: this.$auth.user,
      });
      this.$router.push('/');
    },
  },
};
</script>
