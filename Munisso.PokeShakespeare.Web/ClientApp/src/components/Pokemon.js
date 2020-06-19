import React, { Component } from 'react';

export class Pokemon extends Component {
  
  render () {
    return (
        <div>
            <h2>{this.props.pokemon.pokemon}</h2>
            <p>{this.props.pokemon.description}</p>
        </div>
    );
  }
}
