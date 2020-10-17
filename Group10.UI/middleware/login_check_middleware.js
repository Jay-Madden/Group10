export default async function ({ redirect, app: { $auth, $http } }) {
  if ($auth.strategy.name === 'local') {
    return;
  }
  const accessCode = $auth.getToken($auth.strategy.name).substr(7);
  if (!(await $http.$get(`api/account/check?accessCode=${accessCode}`))) {
    redirect('/');
  }
}
