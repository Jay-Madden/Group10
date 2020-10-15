<template>
  <div v-if="$auth.loggedIn">
    <v-btn rounded="true" color="#858988" class="mr-4" @click="test">
      hellothere
    </v-btn>
    <v-btn rounded="true" color="#858988" class="mr-4" @click="test2">
      general grevious
    </v-btn>
  </div>
  <div v-else>else</div>
</template>

<script>
export default {
  layout: 'account',
};
</script>
