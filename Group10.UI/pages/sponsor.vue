<template>
  <v-flex>
    <v-row>
      <v-col :cols="checkItemSelected(selectedDriver) ? 4 : null">
        <v-card class="ma-5" elevation="5" outlined>
          <v-card-title class="justify-center"> Current Drivers </v-card-title>
          <v-divider />
          <v-list max-height="500" class="overflow-y-auto">
            <v-list-item-group v-model="selectedDriver">
              <v-list-item v-for="(driver, i) in drivers" :key="i">
                <v-row>
                  <v-col class="pl-10">
                    <b>{{ driver.appUser.email }}</b>
                  </v-col>
                  <v-col> </v-col>
                </v-row>
              </v-list-item>
            </v-list-item-group>
          </v-list>
        </v-card>
      </v-col>
      <v-col v-if="checkItemSelected(selectedDriver)">
        <v-card class="ma-5" elevation="5" outlined>
          <v-card-title class="justify-center">
            {{ drivers[selectedDriver].email }}
          </v-card-title>
          <v-divider />
          <v-card elevation="3" outlined class="ma-4 px-5">
            <v-card-title class="justify-center">Points</v-card-title>
            <v-divider class="pt-5" />
            <h2 class="text-center">
              <b>{{ drivers[selectedDriver].points }}</b>
            </h2>
            <v-row>
              <v-col align-self="center" :cols="1">
                <v-btn @click="removePoints" outlined fab small>
                  <v-icon>mdi-minus</v-icon>
                </v-btn>
              </v-col>
              <v-col align-self="center">
                <v-slider
                  class="mt-5"
                  ticks="always"
                  tick-size="0"
                  v-model="value"
                  thumb-label
                  step="5"
                />
              </v-col>
              <v-col :cols="1" align-self="center">
                <v-btn @click="addPoints" outlined fab small>
                  <v-icon>mdi-plus</v-icon>
                </v-btn>
              </v-col>
            </v-row>
          </v-card>
          <v-row justify="center" align="center">
            <v-btn class="ma-5" @click="removeDriver">
              <v-icon>mdi-close</v-icon>
            </v-btn>
          </v-row>
        </v-card>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <v-card class="ma-5" elevation="5" outlined>
          <v-card-title class="justify-center"> Available Drivers</v-card-title>
          <v-divider />
          <v-list max-height="500" class="overflow-y-auto">
            <v-row v-for="(driver, i) in a_drivers" :key="i">
              <v-col class="px-6">
                <b>{{ driver.email }}</b>
              </v-col>
            </v-row>
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
      <v-col>
        <v-card class="ma-5" elevation="5" outlined>
          <v-card-title class="justify-center"> Claim Drivers</v-card-title>
          <v-divider />
          <v-row justify="center" align="center">
            <v-text-field
              v-model="email"
              class="pa-5"
              label="Enter Email"
            ></v-text-field>
            <v-btn :disabled="email === ''" @click="claim">
              <v-icon>mdi-check</v-icon>
            </v-btn>
            <v-divider />
          </v-row>
        </v-card>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <v-card class="ma-5" elevation="5" outlined>
          <v-card-title class="justify-center"> Current Catalog </v-card-title>
          <v-divider />
          <v-list max-height="500" class="overflow-y-auto">
            <v-list class="overflow-y-auto">
              <v-list-item v-for="(item, i) in catalog" :key="i">
                <v-hover v-slot="{ hover }">
                  <v-row>
                    <v-col cols="auto" align-self="center">
                      <b>{{ i }}:</b>
                    </v-col>
                    <v-col align-self="center">
                      <v-card class="ma-4" elevation="3">
                        <v-img
                          :max-height="hover ? 150 : 50"
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
      selectedDriver: null,
      key: 0,
      drivers: [],
      value: 0,
      messages: 0,
      email: '',
      a_drivers: [],
      catalog: [],
    };
  },
  async fetch() {
    const token = this.$auth.getToken(this.$auth.strategy.name).substr(7);
    this.$http.setToken(token, 'Bearer');
    this.drivers = (
      await this.$http.get('api/sponsor/drivers').then(res => res.json())
    ).drivers;
    this.messages = (
      await this.$http
        .get('api/driver/getNotifications')
        .then(res => res.json())
    ).notifications;
    this.a_drivers = (
      await this.$http.get('api/sponsor/all_drivers').then(res => res.json())
    ).drivers;
    this.catalog = await this.$http
      .get('api/sponsor/get_catalog')
      .then(res => res.json());
  },
  methods: {
    checkItemSelected(item) {
      return !(item === null || item === undefined);
    },
    async remove(item) {
      await this.$http.post('api/sponsor/remove_from_catalog', {
        ProductId: item.productId,
      });
      this.catalog = this.catalog.filter(i => i.productId !== item.productId);
    },
    async claim() {
      const id = this.a_drivers.find(x => x.email === this.email).appUserId;
      await this.$http.put(`api/sponsor/claim_driver?driverId=${id}`);
      this.$nuxt.refresh();
    },
    async removeDriver() {
      await this.$http.put(
        `api/sponsor/remove_driver?driverId=${
          this.drivers[this.selectedDriver].appUserId
        }`
      );
      this.selectedDriver = null;
      this.email = null;
      this.$nuxt.refresh();
    },
    async addPoints() {
      const email = this.drivers[this.selectedDriver].appUserId;
      await this.$http.get(
        `api/sponsor/givePoints?points=${this.value}&driverId=${email}`
      );
      this.drivers = (
        await this.$http.get('api/sponsor/drivers').then(res => res.json())
      ).drivers;
    },
    async removePoints() {
      const email = this.drivers[this.selectedDriver].appUserId;
      await this.$http.get(
        `api/sponsor/takePoints?points=${this.value}&driverId=${email}`
      );
      this.drivers = (
        await this.$http.get('api/sponsor/drivers').then(res => res.json())
      ).drivers;
    },
  },
};
</script>
