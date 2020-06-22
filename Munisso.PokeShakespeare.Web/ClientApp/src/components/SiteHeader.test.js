import React from 'react';
import { render, unmountComponentAtNode } from 'react-dom';
import { MemoryRouter } from 'react-router-dom';
import { act } from 'react-dom/test-utils';
import { SiteHeader } from './SiteHeader';

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

describe('SiteHeader', () => {
  it('renders the navigation', async () => {
    act(() => {
      render(
          <MemoryRouter>
            <SiteHeader />
          </MemoryRouter>
        , container);
      });

    let contents = container.querySelectorAll('a');
    expect(contents).toHaveLength(2);
    let heading = container.querySelector('.header_content');
    expect(heading.textContent).toMatch('Search');
    expect(heading.textContent).toMatch('Favourites');
  });
});