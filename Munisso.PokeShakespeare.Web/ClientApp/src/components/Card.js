import React, { Component } from 'react';
import PropTypes from 'prop-types';
import './Card.css';

class Card extends Component {

    render() {
        return (
            <div className="card">
                {this.props.children}
            </div>
        );
    }
}

Card.propTypes = {
    children: PropTypes.oneOfType([
        PropTypes.arrayOf(PropTypes.node),
        PropTypes.element
    ]).isRequired
};

Card.defaultProps = {
    children: null
};

export { Card };