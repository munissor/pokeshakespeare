import React, { Component } from 'react';
import PropTypes from 'prop-types';
import {Card} from './Card';
import {Spinner} from './Spinner';
import {Pokemon} from './Pokemon';
import './Search.css';

class Search extends Component {
  static displayName = Search.name;
  static MESSAGES = {
    NotFound: "That pokemon doesn't exist!",
    ServerError: "There was a problem with your search, please retry",
    EmptyQuery: "Enter a Pokemon name and search again",
    ConnectionFailure: "Unable to reach the server, is your connection working?",
  };

  constructor() {
    super();

    this.state = {
        loading: false,
        query: '',
        pokemon: null,
        error: null,
    };

    this.handleSearch = this.handleSearch.bind(this);
    this.handleQueryChange = this.handleQueryChange.bind(this);
  }

  handleQueryChange(event) {
    const { value } = event.target;
    this.setState(() => ({ query: value }));
  }
  
  async handleSearch(event) {
    let error = null;
    let pokemon = null;
    event.preventDefault();
    if(this.state.query) {
      try {
        this.setState({ loading: true, pokemon: null, error: null });
        const response = await fetch(`/pokemon/${this.state.query}`);
        if ( response.status === 200 ) {
          pokemon = await response.json();
        }
        else if( response.status === 404 ) {
          error = Search.MESSAGES.NotFound;
        }
        else {
          error = Search.MESSAGES.ServerError;
        }
      }
      catch {
        // a fetch promise throws if the connection cannot be made
        error = Search.MESSAGES.ConnectionFailure;
      }
    }
    else {
      error = Search.MESSAGES.EmptyQuery;
    }
    this.setState({ loading: false, error, pokemon });
  }

  renderSpinner() {
    if(this.state.loading ) {
      return (
        <Spinner />
      )
    }
  }

  renderPokemon() {
    if(this.state.pokemon ) {
      return (
        <div>
          <Pokemon pokemon={this.state.pokemon} />
          <button className="addFav" onClick={() => this.props.addFavourite(this.state.pokemon)}>
            Add to favourites
          </button>
        </div>
      )
    }
  }

  renderError() {
    if(this.state.error ) {
      return (
        <div className="error">{this.state.error}</div>
      )
    }
  }

  render () {
    return (
      <Card>
        <form onSubmit={this.handleSearch}>
          <label htmlFor="query">Pokemon</label>
          <input name="query" 
            className="search"
            type="text" 
            value={this.state.query}
            onChange={this.handleQueryChange}></input>
          <input className="search" type="submit" value="Search" />
        </form>
        {this.renderSpinner()}
        {this.renderPokemon()}
        {this.renderError()}
      </Card>
    );
  }
}


Search.propTypes = {
  addFavourite: PropTypes.func.isRequired,
};


export { Search };
