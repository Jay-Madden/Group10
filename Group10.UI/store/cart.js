export const state = () => ({
  list: [],
});

export const mutations = {
  add(state, text) {
    state.list.push(text);
  },
  remove(state, { cart }) {
    state.list.splice(state.list.indexOf(cart), 1);
  },
  clear(state) {
    state.list = [];
  },
};
