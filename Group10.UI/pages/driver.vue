<template>
  <v-flex>
    <v-layout justify-center align-center>
      <v-card class="ma-5" elevation="5" outlined>
        <v-card-title class="justify-center"> Current Points </v-card-title>
        <v-divider />
        <v-card-title class="justify-center">{{
          this.points || 0
        }}</v-card-title>
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
                <b>{{ sponsor.email }}</b>
              </v-col>
            </v-row>
          </v-list>
        </v-card>
        <v-card class="ma-5" elevation="5" outlined>
          <v-card-title class="justify-center">
            Available Sponsors
          </v-card-title>
          <v-divider />
          <v-list max-height="500" class="overflow-y-auto">
            <v-row v-for="(sponsor, i) in a_sponsors" :key="i">
              <v-col class="px-6">
                <b>{{ sponsor.email }}</b>
              </v-col>
            </v-row>
          </v-list>
        </v-card>
      </v-col>
      <v-col>
        <v-card class="ma-5" elevation="5" outlined>
          <v-card-title class="justify-center">
            Petition Sponsors
          </v-card-title>
          <v-divider />
          <v-row justify="center" align="center">
            <v-text-field
              v-model="email"
              class="pa-5"
              label="Enter Email"
            ></v-text-field>
            <v-btn :disabled="email === ''" @click="petition">
              <v-icon>mdi-check</v-icon>
            </v-btn>
            <v-divider />
          </v-row>
        </v-card>
        <v-card class="ma-5" elevation="5" outlined>
          <v-card-title class="justify-center"> Past Orders </v-card-title>
          <v-divider />
          <v-list max-height="400" class="overflow-y-auto">
            <v-list-item v-for="(item, i) in this.orders" :key="i">
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
                  <v-col align-self="center">
                    {{ item.name }}
                  </v-col>
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
      </v-col>
      <v-col>
        <v-card class="ma-5" elevation="5" outlined>
          <v-card-title class="justify-center"> Messages </v-card-title>
          <v-divider />
          <v-list max-height="500" class="overflow-y-auto">
            <v-row v-for="(message, i) in messages" :key="i">
              <v-col class="px-6">
                <b>{{ message }}</b>
              </v-col>
            </v-row>
          </v-list>
        </v-card>
      </v-col>
    </v-row>
  </v-flex>
</template>

<script>
export default {
  data() {
    return {
      sponsors: [],
      a_sponsors: [],
      email: '',
      messages: [],
      points: 0,
      orders: [],
    };
  },
  async fetch() {
    const token = this.$auth.getToken(this.$auth.strategy.name).substr(7);
    this.$http.setToken(token, 'Bearer');

    this.a_sponsors = (
      await this.$http.get('api/driver/all_sponsors').then(res => res.json())
    ).sponsors;

    this.sponsors = (
      await this.$http.get('api/driver/sponsors').then(res => res.json())
    ).sponsors;

    this.orders = (
      await this.$http.get('api/driver/get_orders').then(res => res.json())
    ).products;

    this.messages = (
      await this.$http
        .get('api/driver/getNotifications')
        .then(res => res.json())
    ).notifications;

    this.points = (
      await this.$http.get('api/driver/points').then(res => res.json())
    ).points;
  },

  methods: {
    async petition() {
      const id = this.a_sponsors.find(x => x.email === this.email).appUserId;
      await this.$http.put(`api/driver/petition_sponsor?sponsorId=${id}`);
    },
  },
};
</script>
