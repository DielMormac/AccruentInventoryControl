import React from "react";
import { render, screen } from '@testing-library/react';
import Footer from './Footer';

describe('Footer Component', () => {
  test('renders the footer element', () => {
    render(<Footer />);
    const footerElement = screen.getByTestId('footer');
    expect(footerElement).toBeInTheDocument();
  });

  test('renders the footer description', () => {
    render(<Footer />);
    const descriptionElement = screen.getByTestId('footer-description');
    expect(descriptionElement).toBeInTheDocument();
    expect(descriptionElement).toHaveTextContent(
      'This project is part of the exercise for the Principal Engineering Role at Accruent.'
    );
  });

  test('renders the footer note', () => {
    render(<Footer />);
    const noteElement = screen.getByTestId('footer-note');
    expect(noteElement).toBeInTheDocument();
    expect(noteElement).toHaveTextContent(
      "It's not an official Accruent product and there's no relation with the company."
    );
  });

  test('applies the correct class to the footer', () => {
    render(<Footer />);
    const footerElement = screen.getByTestId('footer');
    expect(footerElement).toHaveClass('footer');
  });
});