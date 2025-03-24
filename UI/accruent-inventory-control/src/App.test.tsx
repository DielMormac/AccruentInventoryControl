import React from "react";
import { render, screen } from '@testing-library/react';
import App from './App';

jest.mock('./components/DbInitializer/DbInitializer', () => jest.fn(() => <div data-testid="db-initializer">Mock DbInitializer</div>));
jest.mock('./components/NavBar/NavBar', () => jest.fn(() => <div data-testid="navbar">Mock NavBar</div>));
jest.mock('./pages/Warehouse/Warehouse', () => jest.fn(() => <div data-testid="warehouse-page">Mock Warehouse</div>));
jest.mock('./pages/Report/Report', () => jest.fn(() => <div data-testid="report-page">Mock Report</div>));
jest.mock('./components/Header/Header', () => jest.fn(() => <div data-testid="header">Mock Header</div>));
jest.mock('./components/Footer/Footer', () => jest.fn(() => <div data-testid="footer">Mock Footer</div>));

describe('App Component', () => {
  beforeEach(() => {
    jest.clearAllMocks();
  });

  test('renders Header and Footer components', () => {
    render(<App />);
    expect(screen.getByTestId('header')).toBeInTheDocument();
    expect(screen.getByTestId('footer')).toBeInTheDocument();
  });

  test('renders DbInitializer when database is not initialized', () => {
    render(<App />);
    expect(screen.getByTestId('db-initializer')).toBeInTheDocument();
  });
});