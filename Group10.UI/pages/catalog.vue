<template>
  <v-flex>
    <v-layout justify-center align-center>
      <v-card v-card class="ma-5" elevation="5" outlined
        ><v-card-title> Etsy Catalog </v-card-title>
      </v-card>
    </v-layout>
    <v-card class="ma-4" elevation="2" outlined>
      <p v-if="$fetchState.pending">
        <v-progress-circular indeterminate />
      </p>
      <p v-else-if="$fetchState.error">An error occurred :(</p>
      <v-list class="overflow-y-auto">
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
                    :src="item.url_170x135"
                    :class="{ 'on-hover': hover }"
                  ></v-img>
                </v-card>
              </v-col>
              <v-col align-self="center">
                {{ item.title }}
              </v-col>
              <v-col align-self="center">
                {{ item.description }}
              </v-col>
              <v-card class="ma-2">
                <v-col> ${{ item.price }} </v-col>
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
