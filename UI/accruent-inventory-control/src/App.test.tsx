import React from 'react'; // Explicitly import React to avoid ReferenceError
import { render, screen } from '@testing-library/react';
import App from './App';

describe('App Component', () => {
  test('renders without crashing', () => {
    render(<App />);
  });

  test('renders the Home component', () => {
    render(<App />);
    const homeElement = screen.getByTestId('home-component');
    expect(homeElement).toBeInTheDocument();
  });
});