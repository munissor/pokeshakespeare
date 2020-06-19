const x = [];

const favourites = (state = x, action) => {
    switch (action.type) {
        case 'ADD_FAVOURITE':
            if (!state.find( (i) => i.pokemon === action.pokemon.pokemon )){
                return [
                    ...state,
                    action.pokemon
                ];
            }
            return state;
        case 'REMOVE_FAVOURITE':
            return state.filter((i) => i.pokemon !== action.pokemonName);
        default:
            return state;
    }
}

export default favourites;