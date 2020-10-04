<template>
  <div class="center">
    <v-card :elevation="24" class="mx-auto" max-width="400" color="#089981">
      <v-card-title class="justify-center">Login Page</v-card-title>
      <v-form ref="form" v-model="valid" lazy-validation>
        <v-col cols="12" sm="12" md="12">
          <v-text-field
            v-model="name"
            :counter="25"
            :rules="nameRules"
            label="Username"
            required
          ></v-text-field>
        </v-col>
        <v-col cols="12" sm="12" md="12">
          <v-text-field
            v-model="password"
            :type="show1 ? 'text' : 'password'"
            name="input-10-1"
            label="Password"
            :counter="25"
          ></v-text-field>
        </v-col>
        <v-flex text-center style="padding-left: 15px; padding-bottom: 15px">
          <v-btn rounded="true" color="#858988" class="mr-4" @click="validate">
            Submit
          </v-btn>
        </v-flex>

        <v-flex text-center style="padding-left: 15px">
          <v-btn rounded="true" color="#858988" class="mr-4" @click="reset">
            Reset Form
          </v-btn>
        </v-flex>
      </v-form>
      <a>
        <center href="url" style="color: #000000">Signup for an Account</center>
      </a>
      <template>
        <nuxt-link to="/home">Home page</nuxt-link>
      </template>
    </v-card>
  </div>
</template>
