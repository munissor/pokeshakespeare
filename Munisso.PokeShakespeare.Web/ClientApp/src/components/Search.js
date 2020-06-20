import React, { Component } from 'react';
import {Card} from './Card';
import {Spinner} from './Spinner';
import {Pokemon} from './Pokemon';

export class Search extends Component {
  static displayName = Search.name;

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
    event.preventDefault();
    if(this.state.query) {
      this.setState({ loading: true, pokemon: null, error: null });
      const response = await fetch(`/pokemon/${this.state.query}`);
      let error = null;
      let pokemon = null;
      if ( response.status === 200 ) {
        pokemon = await response.json();
      }
      else if( response.status === 404 ) {
        error = "That pokemon doesn't exist!";
      }
      else {
        error = "There was a problem with your search, please retry";
      }

      this.setState({ loading: false, error, pokemon });
    }
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
          <button onClick={() => this.props.addFavourite(this.state.pokemon)}>
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
            type="text" 
            value={this.state.query}
            onChange={this.handleQueryChange}></input>
          <input type="submit" value="Search" />
        </form>
        {this.renderSpinner()}
        {this.renderPokemon()}
        {this.renderError()}
      </Card>
    );
  }
}
