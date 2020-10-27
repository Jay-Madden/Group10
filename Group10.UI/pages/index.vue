<template>
  <div v-if="$auth.loggedIn">
    <v-flex>
      <v-layout justify-center align-center>
        <v-card class="ma-5" dark elevation="5" outlined>
          <v-card-title class="justify-center"> Current Points </v-card-title>
          <v-divider />
          <v-card-title class="justify-center">0</v-card-title>
        </v-card>
      </v-layout>
      <v-row>
        <v-col>
          <v-card class="ma-5" dark elevation="5" outlined>
            <v-card-title class="justify-center">
              Current Sponsors
            </v-card-title>
            <v-divider />
          </v-card>
        </v-col>
        <v-col>
          <v-card class="ma-5" dark elevation="5" outlined>
            <v-card-title class="justify-center"> Past Orders </v-card-title>
            <v-divider />
          </v-card>
        </v-col>
      </v-row>
    </v-flex>
  </div>
</template>

<script>
export default {
  middleware: 'login_check_middleware',
};
</script>
