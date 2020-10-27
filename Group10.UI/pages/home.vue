<template>
  <div v-if="$auth.loggedIn">
    <v-card width="600" class="pa-md-4 mx-lg-auto" elevation="2">
      <v-chip class="mx-auto">Currently Logged in User</v-chip>
      <div>
        {{ this.$auth.user }}
      </div>
      <div>
        {{ this.$auth.getToken(this.$auth.strategy.name) }}
      </div>
    </v-card>
    <v-btn link to="/">hello</v-btn>
  </div>
  <div v-else>else</div>
</template>

<script>
export default {
  data() {
    return {
      token: 1,
      foo: '',
    };
  },
  methods: {},
};
</script>
