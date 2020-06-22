import React from 'react';
import ReactDOM from 'react-dom';
import { render, unmountComponentAtNode } from "react-dom";
import { act } from "react-dom/test-utils";
import { Card } from './Card';

let container;
beforeEach(() => {
  // setup a DOM element as a render target
  container = document.createElement("div");
  document.body.appendChild(container);
});

afterEach(() => {
  // cleanup on exiting
  unmountComponentAtNode(container);
  container.remove();
  container = null;
});

it('wraps the content into a card node', async () => {
  act(() => {
    render(
      <Card>
        <div className="content" />
      </Card>, container);
    });
  expect(container.querySelector(".card .content")).not.toBeNull();
});
