export const pushUserToList = (state, { user }) => {
    state.users.push(user)
}

export const setEditModal = (state, isShow) => {
    state.isShowEditModal = isShow;
}

export const setEditUser = (state, user) => {
    state.currentUserEdit = user;
}

export const loadUser = (state, users) => {
    state.users = users;
}
