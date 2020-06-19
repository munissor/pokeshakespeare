import React, { Component } from 'react';
import './Spinner.css';

// https://github.com/athanstan/css-pokeball
export class Spinner extends Component {
  
  render () {
    return (
        <div className="poke_box">
            <div className="pokeball">
                <div className="pokeball__button"></div>
            </div>   
        </div>
    );
  }
}
