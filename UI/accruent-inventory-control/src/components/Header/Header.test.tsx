import React from "react";
import { render, screen } from '@testing-library/react';
import './Header.scss';
import Header from "./Header";

test('renders the header element', () => {
  render(<Header />);
  const headerElement = screen.getByTestId('header');
  expect(headerElement).toBeInTheDocument();
});

test('renders the header link with correct href', () => {
  render(<Header />);
  const linkElement = screen.getByTestId('header-link');
  expect(linkElement).toBeInTheDocument();
  expect(linkElement).toHaveAttribute('href', 'https://www.accruent.com/');
  expect(linkElement).toHaveAttribute('target', '_blank');
  expect(linkElement).toHaveAttribute('rel', 'noopener noreferrer');
});

test('renders the header logo with correct attributes', () => {
  render(<Header />);
  const logoElement = screen.getByTestId('header-logo');
  expect(logoElement).toBeInTheDocument();
  expect(logoElement).toHaveAttribute('alt', 'Accruent logo');
  expect(logoElement).toHaveClass('logo');
});

test('renders the header title with correct text', () => {
  render(<Header />);
  const titleElement = screen.getByTestId('header-title');
  expect(titleElement).toBeInTheDocument();
  expect(titleElement).toHaveTextContent('Accruent Inventory Control');
});