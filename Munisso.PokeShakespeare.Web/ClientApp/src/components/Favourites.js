import React, { Component } from 'react';
import {Card} from './Card';

export class Favourites extends Component {
  static displayName = Favourites.name;

  render () {
    return (
      <Card>
        Favs
      </Card>
    );
  }
}
