import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import './SiteHeader.css';

export class SiteHeader extends Component {
  render () {
    return (
      <header className="main">
        <div className="header_content">
          <h1>PokeShakespeare</h1>
          <ul className="navigation">
            <li><Link to="/">Search</Link></li>
            <li><Link to="/favourites">Favourites</Link></li>
          </ul>
        </div>
      </header>
    );
  }
}
