import React from "react";
import { render, screen } from '@testing-library/react';
import PageHeader from './PageHeader';

test('renders the PageHeader component', () => {
  render(<PageHeader title="Test Title" subtitle="Test Subtitle" />);
  const pageHeaderElement = screen.getByTestId('page-header');
  expect(pageHeaderElement).toBeInTheDocument();
});

test('renders the title with correct text', () => {
  render(<PageHeader title="Test Title" subtitle="Test Subtitle" />);
  const titleElement = screen.getByTestId('page-header-title');
  expect(titleElement).toBeInTheDocument();
  expect(titleElement).toHaveTextContent('Test Title');
});

test('renders the subtitle with correct text', () => {
  render(<PageHeader title="Test Title" subtitle="Test Subtitle" />);
  const subtitleElement = screen.getByTestId('page-header-subtitle');
  expect(subtitleElement).toBeInTheDocument();
  expect(subtitleElement).toHaveTextContent('Test Subtitle');
});

test('applies the correct class to the PageHeader container', () => {
  render(<PageHeader title="Test Title" subtitle="Test Subtitle" />);
  const pageHeaderElement = screen.getByTestId('page-header');
  expect(pageHeaderElement).toHaveClass('page-header');
});