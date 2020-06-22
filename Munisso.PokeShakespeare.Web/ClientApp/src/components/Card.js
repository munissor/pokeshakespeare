import React, { Component } from 'react';
import PropTypes from 'prop-types';
import './Card.css';

class Card extends Component {

    render() {
        var testid = this.props['data-test-id'];
        return (
            <div className="card" data-test-id={testid}>
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