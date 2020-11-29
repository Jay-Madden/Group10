export const state = () => ({
  isAdmin: false,
});

export const mutations = {
  set(state) {
    state.isAdmin = true;
  },
};
