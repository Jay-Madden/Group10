<template>
  <v-flex>
    <v-layout justify-center align-center>
      <v-card v-card class="ma-5" elevation="5" outlined
        ><v-card-title> Cart </v-card-title>
      </v-card>
    </v-layout>
    <v-card class="ma-4" elevation="2" outlined>
      <div v-if="items.length < 1">No Current Items in cart</div>
      <v-list>
        <v-list-item v-for="(item, i) in this.$store.state.cart.list" :key="i">
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
                <v-btn @click="remove(item)">
                  <v-icon>mdi-minus</v-icon>
                </v-btn>
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
    <v-layout justify-center align-center>
      <v-card v-card class="ma-5" elevation="5" outlined>
        <v-btn @click="buy">
          <v-icon>mdi-check</v-icon>
        </v-btn>
      </v-card>
      <v-card
        v-if="this.status !== null"
        v-card
        class="ma-5"
        elevation="5"
        outlined
        ><v-card-title> {{ this.status }} </v-card-title>
      </v-card>
    </v-layout>
  </v-flex>
</template>

<script>
export default {
  data() {
    return {
      items: this.$store.state.cart.list,
      status: null,
    };
  },
  methods: {
    remove(item) {
      this.$store.commit('cart/remove', item);
    },
    async buy() {
      const foo = this.items.map(x => x.productId.toString());
      try {
        await this.$http.post('api/order/purchase_cart_order', {
          Products: foo,
        });
      } catch (e) {
        this.status = 'Insufficient points';
        return;
      }
      this.status = 'Purchase succesful';
      this.$store.commit('cart/clear');
      this.$nuxt.refresh();
    },
  },
};
</script>
