<template>
  <v-col>
    <h1>{{ counter }}</h1>
    <p>
      <v-btn @click="counter += 1"></v-btn>
    </p>
  </v-col>
</template>

<script>
export default {
  data() {
    return {
      counter: 0,
    }
  },
}
</script>
