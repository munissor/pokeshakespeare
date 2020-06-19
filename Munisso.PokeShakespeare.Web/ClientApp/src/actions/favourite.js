export const addFavourite = pokemon => ({
    type: 'ADD_FAVOURITE',
    pokemon,
});

export const removeFavourite = pokemonName => ({
    type: 'REMOVE_FAVOURITE',
    pokemonName,
});