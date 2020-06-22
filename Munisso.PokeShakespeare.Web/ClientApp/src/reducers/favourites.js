const favourites = (state = [], action) => {
    switch (action.type) {
        case 'ADD_FAVOURITE':
            if (!state.find( (i) => i.name === action.pokemon.name )){
                return [
                    ...state,
                    action.pokemon
                ];
            }
            return state;
        case 'REMOVE_FAVOURITE':
            return state.filter((i) => i.name !== action.pokemonName);
        default:
            return state;
    }
}

export default favourites;