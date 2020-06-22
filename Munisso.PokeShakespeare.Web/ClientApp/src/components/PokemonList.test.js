import React from 'react';
import { render, unmountComponentAtNode } from 'react-dom';
import { act } from 'react-dom/test-utils';
import { fireEvent } from '@testing-library/react';
import PokemonList from './PokemonList';

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

describe('PokemonList', () => {
  it('renders many pokemons', async () => {
    let data = [{
      pokemon: 'pikachu',
      description: 'pikachu description'
    },
    {
      pokemon: 'bulbasaur',
      description: 'bulbasaur description'
    }];

    act(() => {
      
      render(
          <PokemonList pokemons={data} removeFavourite={() => {}} />
        , container);
      });

    let contents = container.querySelectorAll('.card');
    expect(contents.length).toBe(data.length);
    data.forEach((p, i) => {
      expect(contents[i].querySelector('h2').textContent).toBe(p.pokemon);
    });
  });

  it('renders an empty list', async () => {
    let data = [];

    act(() => {
      
      render(
          <PokemonList pokemons={data} removeFavourite={() => {}} />
        , container);
      });

    let contents = container.querySelectorAll('.card');
    expect(contents.length).toBe(data.length);
  });

  it('calls the removeHandler correctly', async () => {
    let data = [{
      pokemon: 'pikachu',
      description: 'pikachu description'
    }];
    let handler = jest.fn();

    act(() => {
      render(
        <PokemonList pokemons={data} removeFavourite={handler} />
       , container);

      fireEvent.click(container.querySelector('button'));
    });
      
    expect(handler.mock.calls.length).toBe(1);
    expect(handler).toHaveBeenCalledWith(data[0].pokemon);
  });
});
