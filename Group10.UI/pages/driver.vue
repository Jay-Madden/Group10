<template>
  <v-flex>
    <v-layout justify-center align-center>
      <v-card class="ma-5" elevation="5" outlined>
        <v-card-title class="justify-center"> Current Points </v-card-title>
        <v-divider />
        <v-card-title class="justify-center">0</v-card-title>
      </v-card>
    </v-layout>
    <v-row>
      <v-col>
        <v-card class="ma-5" elevation="5" outlined>
          <v-card-title class="justify-center"> Current Sponsors </v-card-title>
          <v-divider />
          <v-list max-height="500" class="overflow-y-auto">
            <v-row v-for="(sponsor, i) in sponsors" :key="i">
              <v-col class="px-6">
                <b>{{ sponsor.name }}</b>
              </v-col>
            </v-row>
          </v-list>
        </v-card>
      </v-col>
      <v-col>
        <v-card class="ma-5" elevation="5" outlined>
          <v-card-title class="justify-center"> Past Orders </v-card-title>
          <v-divider />
        </v-card>
      </v-col>
    </v-row>
  </v-flex>
</template>

<script>
export default {
  data() {
    return {
      sponsors: [
        {
          name: 'SponsorName1',
        },
        {
          name: 'SponsorName2',
        },
      ],
    };
  },
};
</script>
