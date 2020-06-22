import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { SiteHeader } from './SiteHeader';

class Layout extends Component {
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

Layout.propTypes = {
  children: PropTypes.oneOfType([
      PropTypes.arrayOf(PropTypes.node),
      PropTypes.element
  ]).isRequired
};

Layout.defaultProps = {
  children: null
};

export { Layout };
