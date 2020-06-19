import React, { Component } from 'react';
import './Card.css';

export class Card extends Component {

    render() {
        return ( 
            <div className="card">
                {this.props.children}
            </div>
        );
    }
}