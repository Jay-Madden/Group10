export default async function ({ redirect, app: { $auth, $http } }) {
  if ($auth.strategy.name === 'local') {
    if ($auth.user.role === 'Driver') {
      redirect('/driver');
    }
    if ($auth.user.role === 'Sponsor') {
      redirect('/sponsor');
    }
    return;
  }
  const accessCode = $auth.getToken($auth.strategy.name).substr(7);
  if (!(await $http.$get(`api/account/check?accessCode=${accessCode}`))) {
    redirect('/register');
    return;
  }

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
