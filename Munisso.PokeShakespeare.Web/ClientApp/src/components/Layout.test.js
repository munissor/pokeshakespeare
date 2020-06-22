import React from 'react';
import ReactDOM from 'react-dom';
import { MemoryRouter } from 'react-router-dom';
import { render, unmountComponentAtNode } from 'react-dom';
import { act } from 'react-dom/test-utils';
import { Layout } from './Layout';

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

describe('Layout', () => {
  it('wraps the content into a "content" node', async () => {
    act(() => {
      render(
        <MemoryRouter>
          <Layout>
            <div className='children' />
          </Layout>
        </MemoryRouter>, container);
      });
    expect(container.querySelector('.content .children')).not.toBeNull();
  });

  it('renders the header', async () => {
    act(() => {
      render(
        <MemoryRouter>
          <Layout>
            <div className='children' />
          </Layout>
        </MemoryRouter>, container);
      });
    expect(container.querySelector('header')).not.toBeNull();
  });

});

