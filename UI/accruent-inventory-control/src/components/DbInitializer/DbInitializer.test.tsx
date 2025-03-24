import React from "react";
import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import '@testing-library/jest-dom'; // Import jest-dom for custom matchers
import DbInitializer from './DbInitializer';
import { getInMemoryDatabaseInitializer } from '../../services/apiService';

jest.mock('../../services/apiService', () => ({
  getInMemoryDatabaseInitializer: jest.fn(),
}));

jest.mock('../Toast/Toast', () => jest.fn(() => <div data-testid="toast">Mock Toast</div>));

describe('DbInitializer Component', () => {
  const mockOnDatabaseInitialized = jest.fn();

  beforeEach(() => {
    jest.clearAllMocks();
  });

  afterAll(() => {
    jest.restoreAllMocks();
  });

  test('renders the initialize button', () => {
    render(<DbInitializer onDatabaseInitialized={mockOnDatabaseInitialized} />);
    const button = screen.getByTestId('initialize-button');
    expect(button).toBeInTheDocument();
    expect(button).toHaveTextContent('Initialize InMemory Database');
  });

  test('disables the button while initializing', async () => {
    (getInMemoryDatabaseInitializer as jest.Mock).mockResolvedValue(true);

    render(<DbInitializer onDatabaseInitialized={mockOnDatabaseInitialized} />);
    const button = screen.getByTestId('initialize-button');

    fireEvent.click(button);
    expect(button).toBeDisabled();
    await waitFor(() => expect(button).not.toBeDisabled());
  });

  test('calls onDatabaseInitialized on successful initialization', async () => {
    (getInMemoryDatabaseInitializer as jest.Mock).mockResolvedValue(true);

    render(<DbInitializer onDatabaseInitialized={mockOnDatabaseInitialized} />);
    const button = screen.getByTestId('initialize-button');

    fireEvent.click(button);
    await waitFor(() => expect(mockOnDatabaseInitialized).toHaveBeenCalledTimes(1));
  });

  test('shows a toast on initialization failure', async () => {
    (getInMemoryDatabaseInitializer as jest.Mock).mockRejectedValue(new Error('Initialization failed'));

    render(<DbInitializer onDatabaseInitialized={mockOnDatabaseInitialized} />);
    const button = screen.getByTestId('initialize-button');

    fireEvent.click(button);
    await waitFor(() => expect(screen.getByTestId('toast')).toBeInTheDocument());
    expect(screen.getByTestId('toast')).toHaveTextContent('Mock Toast');
  });

  test('does not call onDatabaseInitialized on initialization failure', async () => {
    (getInMemoryDatabaseInitializer as jest.Mock).mockRejectedValue(new Error('Initialization failed'));

    render(<DbInitializer onDatabaseInitialized={mockOnDatabaseInitialized} />);
    const button = screen.getByTestId('initialize-button');

    fireEvent.click(button);
    await waitFor(() => expect(mockOnDatabaseInitialized).not.toHaveBeenCalled());
  });
});