import React, { Component } from 'react';
import { Pokemon } from './Pokemon';
import { Card } from './Card';


export default class PokemonList extends Component {
  
  render () {
    return this.props.pokemons.map(p => {
        return (
            <Card key={p.pokemon}>
                <Pokemon pokemon={p} />
                <button onClick={() => this.props.removeFavourite(p.pokemon)}>
                    Remove
                </button>
            </Card>
        );
    })
  }
}
