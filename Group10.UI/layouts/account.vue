<template>
  <v-app>
    <v-main>
      <v-card color="#089981">
        <v-navigation-drawer permanent>
          <v-list>
            <v-list-item class="px-2">
              <v-list-item-avatar>
                <v-img src="kermit.jpg"></v-img>
              </v-list-item-avatar>
            </v-list-item>

            <v-list-item link>
              <v-list-item-content>
                <v-list-item-title class="title"> Welcome! </v-list-item-title>
                <v-list-item-subtitle
                  >kermit_drinking_tea@gmail.com</v-list-item-subtitle
                >
              </v-list-item-content>
            </v-list-item>
          </v-list>

          <v-list nav>
            <v-list-item link to="/">
              <v-list-item-icon>
                <v-icon>mdi-folder</v-icon>
              </v-list-item-icon>
              <v-list-item-title> Home </v-list-item-title>
            </v-list-item>

            <v-list-item link>
              <v-list-item-icon>
                <v-icon>mdi-account-multiple</v-icon>
              </v-list-item-icon>
              <v-list-item-title>Friends</v-list-item-title>
            </v-list-item>

            <v-list-item link>
              <v-list-item-icon>
                <v-icon>mdi-star</v-icon>
              </v-list-item-icon>
              <v-list-item-title>Starred</v-list-item-title>
            </v-list-item>
          </v-list>
        </v-navigation-drawer>
      </v-card>
      <nuxt />
    </v-main>
  </v-app>
</template>
