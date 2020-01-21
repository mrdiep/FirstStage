export function getUser(state) {
  return state.users.find(x => x.id === 'diep');
}