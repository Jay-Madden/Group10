<template>
  <div class="center">
    <v-card :elevation="24" class="mx-auto" max-width="400">
      <v-card-title class="justify-center">Account Information</v-card-title>
      <v-form ref="form" v-model="valid" lazy-validation>
        <v-col cols="12" sm="12" md="12">
          <v-text-field
            v-model="name"
            :counter="25"
            :rules="nameRules"
            label="First Name"
            required
          ></v-text-field>
        </v-col>
        <v-col cols="12" sm="12" md="12">
          <v-text-field
            v-model="name"
            :counter="25"
            :rules="nameRules"
            label="Last Name"
            required
          ></v-text-field>
        </v-col>
        <v-col cols="12" sm="12" md="12">
          <v-text-field
            name="input-10-1"
            label="E-Mail"
            :counter="25"
          ></v-text-field>
        </v-col>
        <v-col cols="12" sm="12" md="12">
          <v-text-field
            name="input-10-1"
            label="Phone Number"
            :counter="10"
          ></v-text-field>
        </v-col>
        <v-col cols="12" sm="12" md="12">
          <v-text-field
            name="input-10-1"
            label="Driver ID"
            :counter="10"
          ></v-text-field>
        </v-col>
        <v-flex text-center style="padding-left: 15px; padding-bottom: 15px">
          <v-btn rounded="true" color="#858988" class="mr-4" @click="validate">
            Submit
          </v-btn>
        </v-flex>

        <v-flex text-center style="padding-left: 15px; padding-bottom: 15px">
          <v-btn rounded="true" color="#858988" class="mr-4" @click="reset">
            Reset Form
          </v-btn>
        </v-flex>
      </v-form>
    </v-card>
  </div>
</template>

<script>
export default {
  layout: 'account',
};
</script>
