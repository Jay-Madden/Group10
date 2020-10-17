export default function ({ store, redirect, app: { $auth, $http, router } }) {
  /*
  redirect('/');

  if (!$auth.loggedIn || store.state.authStore.isRegistering) {
    return;
  }

  const accessCode = $auth.getToken($auth.strategy.name).substr(7);

  if (await $http.$get(`api/account/check?accessCode=${accessCode}`)) {
    redirect('/home');

    return;
  }

  debugger;
  try {
    await $auth.loginWith('local', {
      data: {
        AccessToken: accessCode,
        GoogleUserInfo: $auth.user,
      },
    });

    const token = $auth.getToken($auth.strategy.name).substr(7);

    $http.setToken(token, 'Bearer');
  } catch (e) {
    console.log(e);
  }
  */
}
