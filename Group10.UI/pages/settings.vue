<template>
  <v-container fill-height fluid>
    <v-row justify="center" align="center">
      <v-card outlined elevation="4" justify="center" class="ma-2 pa-5">
        <v-card-title> Settings </v-card-title>
        <v-divider />
        <v-switch v-model="$vuetify.theme.dark" inset label="Dark Mode" />
      </v-card>
    </v-row>
  </v-container>
</template>
