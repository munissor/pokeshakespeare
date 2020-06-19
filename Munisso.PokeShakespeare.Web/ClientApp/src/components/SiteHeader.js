import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './SiteHeader.css';

export class SiteHeader extends Component {

  constructor (props) {
    super(props);
  }

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
