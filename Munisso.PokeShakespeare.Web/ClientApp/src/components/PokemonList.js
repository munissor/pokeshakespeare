import React, { Component } from 'react';
import { Pokemon } from './Pokemon';
import PropTypes from 'prop-types';
import { Card } from './Card';


class PokemonList extends Component {
  
  render () {
    return (this.props.pokemons || []).map(p => {
        return (
            <Card data-test-id={"fav-" + p.pokemon} key={p.pokemon}>
                <Pokemon pokemon={p} />
                <button onClick={() => this.props.removeFavourite(p.pokemon)}>
                    Remove
                </button>
            </Card>
        );
    })
  }
}

PokemonList.propTypes = {
  pokemons: PropTypes.arrayOf(PropTypes.shape({
    pokemon: PropTypes.string.isRequired,
    description: PropTypes.string.isRequired
  })).isRequired,
  removeFavourite: PropTypes.func.isRequired,
};

PokemonList.defaultProps = {
  pokemons: null,
};

export default PokemonList;