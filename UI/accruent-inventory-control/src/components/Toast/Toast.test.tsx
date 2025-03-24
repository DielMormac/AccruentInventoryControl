import React from "react";
import { render, screen, fireEvent } from '@testing-library/react';
import '@testing-library/jest-dom';
import Toast, { IToastProps } from './Toast';

describe('Toast Component', () => {
  const mockOnClose = jest.fn();

  const defaultProps: IToastProps = {
    title: 'Test Title',
    text: 'Test Text',
    type: 'success',
    timeout: 2,
    onClose: mockOnClose,
  };

  beforeEach(() => {
    jest.clearAllMocks();
  });

  test('renders the Toast component with correct title and text', () => {
    render(<Toast {...defaultProps} />);
    expect(screen.getByTestId('toast')).toBeInTheDocument();
    expect(screen.getByTestId('toast-title')).toHaveTextContent('Test Title');
    expect(screen.getByTestId('toast-body')).toHaveTextContent('Test Text');
  });

  test('applies the correct class based on the type', () => {
    render(<Toast {...defaultProps} type="error" />);
    const toastElement = screen.getByTestId('toast');
    expect(toastElement).toHaveClass('toast-error');
  });

  test('calls onClose when the close button is clicked', () => {
    render(<Toast {...defaultProps} />);
    const closeButton = screen.getByTestId('toast-close');
    fireEvent.click(closeButton);
    expect(mockOnClose).toHaveBeenCalledTimes(1);
  });

  test('calls onClose after the timeout', () => {
    jest.useFakeTimers();
    render(<Toast {...defaultProps} />);
    jest.advanceTimersByTime(2000); // Advance time by 2 seconds
    expect(mockOnClose).toHaveBeenCalledTimes(1);
    jest.useRealTimers();
  });

  test('cleans up the timer on unmount', () => {
    jest.useFakeTimers();
    const { unmount } = render(<Toast {...defaultProps} />);
    unmount();
    jest.advanceTimersByTime(2000); // Advance time by 2 seconds
    expect(mockOnClose).not.toHaveBeenCalled();
    jest.useRealTimers();
  });
});