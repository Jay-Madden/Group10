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
                    max-width="300"
                    :src="item.imageurl"
                    :class="{ 'on-hover': hover }"
                  ></v-img>
                </v-card>
              </v-col>
              <v-col cols="auto" align-self="center">
                <v-btn @click="add(item)"> <v-icon>mdi-plus</v-icon> </v-btn>
              </v-col>
              <v-col align-self="center">
                {{ item.name }}
              </v-col>
              <v-spacer />
              <v-card class="ma-2 pa-4">
                <v-container fluid fill-height>
                  <v-row justify="center" align="center">
                    {{ item.pricePts }}
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
    const token = this.$auth.getToken(this.$auth.strategy.name).substr(7);
    this.$http.setToken(token, 'Bearer');
    if (this.$auth.user.role === 'Sponsor') {
      this.items = (
        await this.$http.get('api/catalog/info').then(res => res.json())
      ).products;
    } else {
      this.items = (
        await this.$http
          .get('api/driver/get_sponsor(s)_catalog')
          .then(res => res.json())
      ).products;
    }
  },
  methods: {
    async add(item) {
      if (this.$auth.user.role === 'Sponsor') {
        await this.$http.put('api/sponsor/add_to_catalog', {
          ProductId: item.productId,
        });
      } else {
        this.$store.commit('cart/add', item);
      }
      this.items = this.items.filter(i => i.productId !== item.productId);
    },
  },
};
</script>
