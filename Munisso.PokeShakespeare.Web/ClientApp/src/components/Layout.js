import React, { Component } from 'react';
import { SiteHeader } from './SiteHeader';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        <SiteHeader />
        <div className="content">
          {this.props.children}
        </div>
      </div>
    );
  }
}
