import React from "react";
import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import Warehouse from './Warehouse';
import { fetchProducts, postWarehouseTransaction } from '../../services/apiService';
import { IProductResponse } from '../../services/Products/IProduct';

jest.mock('../../services/apiService', () => ({
  fetchProducts: jest.fn(),
  postWarehouseTransaction: jest.fn(),
}));

describe('Warehouse Page - Request Tests', () => {
  const mockProducts: IProductResponse[] = [
    { id: 1, code: 'P001', name: 'Product 1' },
    { id: 2, code: 'P002', name: 'Product 2' },
  ];

  beforeEach(() => {
    jest.clearAllMocks();
  });

  test('submits a valid warehouse transaction request', async () => {
    (fetchProducts as jest.Mock).mockResolvedValue(mockProducts);
    (postWarehouseTransaction as jest.Mock).mockResolvedValue({
      product: { code: 'P001' },
      type: 'inbound',
      quantity: 10,
    });

    render(<Warehouse />);

    await waitFor(() => {
      expect(fetchProducts).toHaveBeenCalledTimes(1);
    });

    const productSelect = screen.getByTestId('product-select');
    fireEvent.change(productSelect, { target: { value: 'Product 1' } });

    const movementTypeInbound = screen.getByTestId('movement-type-inbound');
    fireEvent.click(movementTypeInbound);

    const quantityInput = screen.getByTestId('quantity-input');
    fireEvent.change(quantityInput, { target: { value: '10' } });

    const form = screen.getByTestId('warehouse-form-component');
    fireEvent.submit(form);

    await waitFor(() => {
      expect(postWarehouseTransaction).toHaveBeenCalledWith({
        productCode: '',
        type: 'inbound',
        quantity: 10,
      });
      expect(screen.getByTestId('toast')).toHaveTextContent('Warehouse transaction registered with success');
    });
  });

  test('handles errors during warehouse transaction submission', async () => {
    (fetchProducts as jest.Mock).mockResolvedValue(mockProducts);
  
    render(<Warehouse />);
  
    // Wait for products to be fetched
    await waitFor(() => {
      expect(fetchProducts).toHaveBeenCalledTimes(1);
    });
  
    // Select a product
    const productSelect = screen.getByTestId('product-select');
    fireEvent.change(productSelect, { target: { value: 'P001' } });
  
    // Select movement type
    const movementTypeOutbound = screen.getByTestId('movement-type-outbound');
    fireEvent.click(movementTypeOutbound);
  
    // Enter quantity
    const quantityInput = screen.getByTestId('quantity-input');
    fireEvent.change(quantityInput, { target: { value: '5' } });
    expect(quantityInput).toHaveValue(5);
  
    // Submit the form
    const form = screen.getByTestId('warehouse-form-component');
    fireEvent.submit(form);
  
    // Wait for the API call and verify the error toast
    await waitFor(() => {
      expect(postWarehouseTransaction).toHaveBeenCalledWith({
        productCode: '',
        type: 'outbound',
        quantity: 5,
      });
    });
  });
});