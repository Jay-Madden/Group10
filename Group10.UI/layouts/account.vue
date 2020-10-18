<template>
  <v-app>
    <v-navigation-drawer v-model="drawer" dark class="background" temporary app>
      <v-list>
        <v-list-item class="px-2">
          <v-list-item-avatar>
            <v-img :src="this.$auth.user.picture"></v-img>
          </v-list-item-avatar>
        </v-list-item>
        <v-list-item>
          <v-list-item-content>
            <v-list-item-title class="title">
              Welcome {{ this.$auth.user.userName }}
            </v-list-item-title>
            <v-list-item-subtitle>
              {{ this.$auth.user.email }}
            </v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
      </v-list>

      <v-list nav>
        <v-list-item link>
          <v-list-item-icon>
            <v-icon>mdi-home</v-icon>
          </v-list-item-icon>
          <v-list-item-title> Home </v-list-item-title>
        </v-list-item>

        <v-list-item link>
          <v-list-item-icon>
            <v-icon>mdi-book-open-page-variant-outline</v-icon>
          </v-list-item-icon>
          <v-list-item-title> Catalog </v-list-item-title>
        </v-list-item>

        <v-list-item link>
          <v-list-item-icon>
            <v-icon>mdi-account-group</v-icon>
          </v-list-item-icon>
          <v-list-item-title>View Your Sponsors</v-list-item-title>
        </v-list-item>

        <v-list-item link>
          <v-list-item-icon>
            <v-icon>mdi-cog</v-icon>
          </v-list-item-icon>
          <v-list-item-title>Settings</v-list-item-title>
        </v-list-item>
      </v-list>
      <template v-slot:append>
        <div class="pa-2">
          <v-btn block link to="/"> Logout </v-btn>
        </div>
      </template>
    </v-navigation-drawer>
    <nuxt />
    <v-app-bar-nav-icon @click.stop="drawer = !drawer">
      <v-icon dark> mdi-menu </v-icon>
    </v-app-bar-nav-icon>
  </v-app>
</template>

<script>
export default {
  data() {
    return {
      drawer: null,
    };
  },
};
</script>
