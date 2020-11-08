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
                    <b>{{ driver.name }}</b>
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
            {{ drivers[selectedDriver].name }}
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
                <v-btn outlined fab small>
                  <v-icon>mdi-plus</v-icon>
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
                <v-btn outlined fab small>
                  <v-icon>mdi-minus</v-icon>
                </v-btn>
              </v-col>
            </v-row>
          </v-card>
          <v-card class="ma-5" elevation="5" outlined>
            <v-card-title class="justify-center"> Points History </v-card-title>
            <v-divider />
          </v-card>
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
      drivers: [
        {
          name: 'DriverName1',
          points: '100',
        },
        {
          name: 'DriverName2',
          points: '200',
        },
      ],
    };
  },
  methods: {
    checkItemSelected(item) {
      return !(item === null || item === undefined);
    },
  },
};
</script>
