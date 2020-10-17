<template>
  <div v-if="$auth.loggedIn">
    <v-flex>
      <v-layout justify-center align-center>
        <v-card dark max-width="400" color="#082c6c" outlined>
          <v-card-title class="justify-center">
            Current Amount of Points Accumulated:
          </v-card-title>
          <v-card-title class="justify-center">0</v-card-title>
        </v-card>
      </v-layout>
    </v-flex>
  </div>

  <div v-else>else</div>
</template>

<script>
export default {
  layout: 'account',
};
</script>
