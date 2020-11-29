<template>
  <div v-if="$auth.loggedIn" to="index/"></div>
</template>

<script>
export default {
  middleware: 'login_check_middleware',
};
</script>
