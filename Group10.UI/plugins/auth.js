export default async function ({ $auth, $http }) {
  if (!$auth.loggedIn) {
    return;
  }

  const accessCode = $auth.getToken($auth.strategy.name).substr(7);

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
}
