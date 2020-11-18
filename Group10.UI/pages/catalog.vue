<template>
  <v-flex>
    <v-layout justify-center align-center>
      <v-card v-card class="ma-5" elevation="5" outlined
        ><v-card-title> Etsy Catalog </v-card-title>
      </v-card>
    </v-layout>
    <v-card class="ma-4" elevation="2" outlined>
      <v-container v-if="$fetchState.pending" fluid fill-height>
        <v-row justify="center" align="center">
          <v-col>
            <v-progress-circular indeterminate />
          </v-col>
        </v-row>
      </v-container>
      <v-container v-else-if="$fetchState.error" fluid fill-height>
        <v-row justify="center" align="center">
          <v-col>An error occurred :(</v-col>
        </v-row>
      </v-container>
      <v-list v-else class="overflow-y-auto">
        <v-list-item v-for="(item, i) in items" :key="i">
          <v-hover v-slot="{ hover }">
            <v-row>
              <v-col cols="auto" align-self="center">
                <b>{{ i }}:</b>
              </v-col>
              <v-col align-self="center">
                <v-card class="ma-4" elevation="3">
                  <v-img
                    :max-height="hover ? 150 : 50"
                    :src="item.image"
                    :class="{ 'on-hover': hover }"
                  ></v-img>
                </v-card>
              </v-col>
              <v-col align-self="center">
                {{ item.title }}
              </v-col>
              <v-spacer />
              <v-card class="ma-2 pa-4">
                <v-container fluid fill-height>
                  <v-row justify="center" align="center">
                    {{ Math.round(item.price) }}
                  </v-row>
                </v-container>
              </v-card>
            </v-row>
          </v-hover>
        </v-list-item>
      </v-list>
    </v-card>
  </v-flex>
</template>

<script>
export default {
  data() {
    return {
      items: [],
    };
  },
  async fetch() {
    this.items = await fetch('api/catalog/info').then(res => res.json());
  },
};
</script>
