import React from 'react';
import { render, unmountComponentAtNode } from 'react-dom';
import { act } from 'react-dom/test-utils';
import { Pokemon } from './Pokemon';

let container;
beforeEach(() => {
  // setup a DOM element as a render target
  container = document.createElement('div');
  document.body.appendChild(container);
});

afterEach(() => {
  // cleanup on exiting
  unmountComponentAtNode(container);
  container.remove();
  container = null;
});

describe('Pokemon', () => {
  it('renders the pokemon information', async () => {
    let data = {
      pokemon: 'pikachu',
      description: 'pikachu description'
    };

    act(() => {
      
      render(
          <Pokemon pokemon={data} />
        , container);
      });

    expect(container.querySelector('h2').textContent).toBe(data.pokemon);
    expect(container.querySelector('p').textContent).toBe(data.description);
  });


});

