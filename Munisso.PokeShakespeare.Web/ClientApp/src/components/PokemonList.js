import React, { Component } from 'react';
import { Pokemon } from './Pokemon';
import PropTypes from 'prop-types';
import { Card } from './Card';


class PokemonList extends Component {
  
  render () {
    return (this.props.pokemons || []).map(p => {
        return (
            <Card data-test-id={"fav-" + p.name} key={p.name}>
                <Pokemon pokemon={p} />
                <button onClick={() => this.props.removeFavourite(p.name)}>
                    Remove
                </button>
            </Card>
        );
    })
  }
}

PokemonList.propTypes = {
  pokemons: PropTypes.arrayOf(PropTypes.shape({
    name: PropTypes.string.isRequired,
    description: PropTypes.string.isRequired
  })).isRequired,
  removeFavourite: PropTypes.func.isRequired,
};

PokemonList.defaultProps = {
  pokemons: null,
};

export default PokemonList;