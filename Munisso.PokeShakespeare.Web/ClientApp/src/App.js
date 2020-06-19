import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import Search from './containers/Search';
import Favourites from './containers/FavouritePokemons';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Search} />
        <Route path='/favourites' component={Favourites} />
      </Layout>
    );
  }
}
