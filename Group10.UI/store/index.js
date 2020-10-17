export const state = () => {
  return {
    isRegistering: 0,
  };
};

export const mutations = {
  setRegisterStatus(state, val) {
    state.isRegistering = val;
  },
};
