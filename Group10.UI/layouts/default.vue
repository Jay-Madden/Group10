<template>
  <v-app>
    <v-layout v-if="this.$auth.loggedIn">
      <v-navigation-drawer fixed app v-model="drawer" permanent>
        <v-list>
          <v-list-item class="px-2">
            <v-list-item-avatar>
              <v-img :src="this.$auth.user.picture"></v-img>
            </v-list-item-avatar>
          </v-list-item>
          <v-list-item>
            <v-list-item-content>
              <v-list-item-title class="title">
                {{ this.$auth.user.name }}
              </v-list-item-title>
              <v-list-item-subtitle>
                {{ this.$auth.user.email }}
              </v-list-item-subtitle>
            </v-list-item-content>
          </v-list-item>
        </v-list>
        <v-divider />

        <v-list nav>
          <v-list-item block link to="/">
            <v-list-item-icon>
              <v-icon>mdi-home</v-icon>
            </v-list-item-icon>
            <v-list-item-title> Home </v-list-item-title>
          </v-list-item>

          <v-list-item block link to="/catalog">
            <v-list-item-icon>
              <v-icon>mdi-book-open-page-variant-outline</v-icon>
            </v-list-item-icon>
            <v-list-item-title> Catalog </v-list-item-title>
          </v-list-item>

          <v-list-item block link to="/settings">
            <v-list-item-icon>
              <v-icon>mdi-cog</v-icon>
            </v-list-item-icon>
            <v-list-item-title>Settings</v-list-item-title>
          </v-list-item>
        </v-list>
        <template v-slot:append>
          <div class="pa-2">
            <v-btn block link to="/debug"> <v-icon>mdi-logout</v-icon> </v-btn>
          </div>
        </template>
      </v-navigation-drawer>
      <v-main class="overflow-y: auto">
        <nuxt />
      </v-main>
    </v-layout>
  </v-app>
</template>

<script>
export default {
  data() {
    return {
      drawer: true,
      mini: true,
    };
  },
};
</script>
