import { connect } from 'react-redux'
import { removeFavourite } from '../actions/favourite'
import PokemonList from '../components/PokemonList'


const mapStateToProps = state => ({
    pokemons: state.favourites,
});

const mapDispatchToProps = dispatch => ({
    removeFavourite: pokemonName => dispatch(removeFavourite(pokemonName))
})

export default connect(mapStateToProps, mapDispatchToProps)(PokemonList);