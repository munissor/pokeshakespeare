import React from 'react';
import { render, unmountComponentAtNode } from 'react-dom';
import { act } from 'react-dom/test-utils';
import { fireEvent } from '@testing-library/react';
import { Search } from './Search';

const noop = () => {};
let container;
let fetchMock;
beforeEach(() => {
  // setup a DOM element as a render target
  container = document.createElement('div');
  document.body.appendChild(container);
  fetchMock = jest.fn();
  global.fetch = fetchMock;
});

afterEach(() => {
  // cleanup on exiting
  unmountComponentAtNode(container);
  container.remove();
  container = null;
});

describe('Search', () => {
  it('renders correctly', async () => {
    act(() => {
      render(
        <Search addFavourite={noop}/>
        , container);
      });

    expect(container.querySelector('input[type="text"]')).not.toBeNull();
    expect(container.querySelector('input[type="submit"]').value).toBe('Search');
  });

  it('sends the correct query to the backend', async () => {
    fetchMock.mockReturnValue(Promise.resolve({
      status: 200,
      json: () => Promise.resolve({ pokemon, description: "test" }),
    }));

    let pokemon = 'pikachu';
    act(() => {
      render(
        <Search addFavourite={noop} />
        , container);
      let textbox = container.querySelector('input[type="text"]');
      fireEvent.change(textbox, { target: { value: pokemon } })
    });

    act(() => {
      fireEvent.click(container.querySelector('input[type="submit"]'));
    });
        
    expect(fetchMock.mock.calls.length).toBe(1);
    expect(fetchMock).toHaveBeenCalledWith(`/pokemon/${pokemon}`);
  });
  
  it('does not call the backend with an empty search', async () => {
    act(() => {
      render(
        <Search addFavourite={noop} />
        , container);
    });

    act(() => {
      fireEvent.click(container.querySelector('input[type="submit"]'));
    });
        
    expect(fetchMock).not.toHaveBeenCalled();
    expect(container.querySelector('.error').textContent).toMatch(Search.MESSAGES.EmptyQuery);
  });

  it('renders the result', async () => {
    fetchMock.mockReturnValue(Promise.resolve({
      status: 200,
      json: () => Promise.resolve({ pokemon, description: "test" }),
    }));

    let pokemon = 'pikachu';
    act(() => {
      render(
        <Search addFavourite={noop} />
        , container);
      let textbox = container.querySelector('input[type="text"]');
      fireEvent.change(textbox, { target: { value: pokemon } })
    });

    await act(async () => {
      fireEvent.click(container.querySelector('input[type="submit"]'));
    });
        
    expect(container.querySelector('h2').textContent).toBe(pokemon);
    expect(container.querySelector('p').textContent).toBe("test");
  });


  it('renders the not found error', async () => {
    fetchMock.mockReturnValue(Promise.resolve({
      status: 404,
    }));

    let pokemon = 'invalid';
    act(() => {
      render(
        <Search addFavourite={noop} />
        , container);
      let textbox = container.querySelector('input[type="text"]');
      fireEvent.change(textbox, { target: { value: pokemon } })
    });

    await act(async () => {
      fireEvent.click(container.querySelector('input[type="submit"]'));
    });
        
    expect(container.querySelector('h2')).toBeNull();
    expect(container.querySelector('.error').textContent).toMatch(Search.MESSAGES.NotFound);
  });

  it('renders the API failure error', async () => {
    fetchMock.mockReturnValue(Promise.resolve({
      status: 500,
    }));

    let pokemon = 'pikachu';
    act(() => {
      render(
        <Search addFavourite={noop} />
        , container);
      let textbox = container.querySelector('input[type="text"]');
      fireEvent.change(textbox, { target: { value: pokemon } })
    });

    await act(async () => {
      fireEvent.click(container.querySelector('input[type="submit"]'));
    });
        
    expect(container.querySelector('h2')).toBeNull();
    expect(container.querySelector('.error').textContent).toMatch(Search.MESSAGES.ServerError);
  });


  it('renders an error if fetch fails', async () => {
    fetchMock.mockReturnValue(Promise.reject({}));

    let pokemon = 'pikachu';
    act(() => {
      render(
        <Search addFavourite={noop} />
        , container);
      let textbox = container.querySelector('input[type="text"]');
      fireEvent.change(textbox, { target: { value: pokemon } })
    });

    await act(async () => {
      fireEvent.click(container.querySelector('input[type="submit"]'));
    });
        
    expect(container.querySelector('h2')).toBeNull();
    expect(container.querySelector('.error').textContent).toMatch(Search.MESSAGES.ConnectionFailure);
  });

  it('allows to save a pokemon to favourites', async () => {
    let handler = jest.fn();
    fetchMock.mockReturnValue(Promise.resolve({
      status: 200,
      json: () => Promise.resolve({ pokemon, description: 'test' }),
    }));

    let pokemon = 'pikachu';
    act(() => {
      render(
        <Search addFavourite={handler} />
        , container);
      let textbox = container.querySelector('input[type="text"]');
      fireEvent.change(textbox, { target: { value: pokemon } })
    });

    await act(async () => {
      fireEvent.click(container.querySelector('input[type="submit"]'));
    });
    
    act(() => {
      fireEvent.click(container.querySelector('.addFav'));
    });
        
    expect(handler.mock.calls).toHaveLength(1);
    expect(handler).toHaveBeenCalledWith({pokemon, description: 'test'})
  });
});