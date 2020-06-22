import React, { Component } from 'react';
import PropTypes from 'prop-types';

class Pokemon extends Component {
  
  render () {
    return (
        <div>
            <h2>{this.props.pokemon.name}</h2>
            <p>{this.props.pokemon.description}</p>
        </div>
    );
  }
}

Pokemon.propTypes = {
  pokemon: PropTypes.shape({
    name: PropTypes.string.isRequired,
    description: PropTypes.string.isRequired
  }).isRequired,
};

Pokemon.defaultProps = {
  pokemon: {},
};

export { Pokemon };