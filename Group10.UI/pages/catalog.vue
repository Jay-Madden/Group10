<template>
  <v-flex>
    <v-layout justify-center align-center>
      <v-card v-card class="ma-5" elevation="5" outlined
        ><v-card-title> Etsy Catalog </v-card-title>
      </v-card>
    </v-layout>

    <v-card class="ma-4" elevation="2" outlined>
      <v-list class="overflow-y-auto">
        <v-list-item v-for="(item, i) in items" :key="i">
          <v-row>
            <v-col align-self="center">
              <b>{{ i }}:</b>
            </v-col>
            <v-col align-self="center">
              {{ item.name }}
            </v-col>
            <v-col align-self="center">
              {{ item.description }}
            </v-col>
            <v-card class="ma-2">
              <v-col> ${{ item.cost }} </v-col>
            </v-card>
          </v-row>
        </v-list-item>
      </v-list>
    </v-card>
  </v-flex>
</template>

<script>
export default {
  data() {
    return {
      items: Array.from({ length: 30 }, () => ({
        name: 'ItemName',
        cost: '12',
        description: 'This is an item',
      })),
    };
  },
};
</script>
