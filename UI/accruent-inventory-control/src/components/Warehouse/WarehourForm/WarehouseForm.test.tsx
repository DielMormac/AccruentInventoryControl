import React from "react";
import { render, screen, fireEvent } from '@testing-library/react';
import WarehouseForm from './WarehouseForm';
import { IProductResponse } from '../../../services/Products/IProduct';

describe('WarehouseForm Component', () => {
  const mockSetSelectedProduct = jest.fn();
  const mockSetMovementType = jest.fn();
  const mockSetQuantity = jest.fn();
  const mockHandleSubmit = jest.fn();

  const mockProducts: IProductResponse[] = [
    { id: 1, code: 'P001', name: 'Product 1' },
    { id: 2, code: 'P002', name: 'Product 2' },
  ];

  beforeEach(() => {
    jest.clearAllMocks();
  });

  test('renders the form with all elements', () => {
    render(
      <WarehouseForm
        products={mockProducts}
        selectedProduct=""
        setSelectedProduct={mockSetSelectedProduct}
        movementType="inbound"
        setMovementType={mockSetMovementType}
        quantity={1}
        setQuantity={mockSetQuantity}
        handleSubmit={mockHandleSubmit}
      />
    );

    expect(screen.getByTestId('warehouse-form-component')).toBeInTheDocument();
    expect(screen.getByTestId('product-select')).toBeInTheDocument();
    expect(screen.getByTestId('movement-type-inbound')).toBeInTheDocument();
    expect(screen.getByTestId('movement-type-outbound')).toBeInTheDocument();
    expect(screen.getByTestId('quantity-input')).toBeInTheDocument();
    expect(screen.getByTestId('submit-button')).toBeInTheDocument();
  });

  test('renders all product options', () => {
    render(
      <WarehouseForm
        products={mockProducts}
        selectedProduct=""
        setSelectedProduct={mockSetSelectedProduct}
        movementType="inbound"
        setMovementType={mockSetMovementType}
        quantity={1}
        setQuantity={mockSetQuantity}
        handleSubmit={mockHandleSubmit}
      />
    );

    const defaultOption = screen.getByTestId('product-option-default');
    expect(defaultOption).toBeInTheDocument();
    expect(defaultOption).toHaveTextContent('Select a product');

    mockProducts.forEach((product) => {
      const productOption = screen.getByTestId(`product-option-${product.code}`);
      expect(productOption).toBeInTheDocument();
      expect(productOption).toHaveTextContent(product.name);
    });
  });

  test('calls setSelectedProduct when a product is selected', () => {
    render(
      <WarehouseForm
        products={mockProducts}
        selectedProduct=""
        setSelectedProduct={mockSetSelectedProduct}
        movementType="inbound"
        setMovementType={mockSetMovementType}
        quantity={1}
        setQuantity={mockSetQuantity}
        handleSubmit={mockHandleSubmit}
      />
    );

    const productSelect = screen.getByTestId('product-select');
    fireEvent.change(productSelect, { target: { value: 'P001' } });

    expect(mockSetSelectedProduct).toHaveBeenCalledWith('P001');
  });

  test('calls setMovementType when a movement type is selected', () => {
    render(
      <WarehouseForm
        products={mockProducts}
        selectedProduct=""
        setSelectedProduct={mockSetSelectedProduct}
        movementType="inbound"
        setMovementType={mockSetMovementType}
        quantity={1}
        setQuantity={mockSetQuantity}
        handleSubmit={mockHandleSubmit}
      />
    );

    const outboundRadio = screen.getByTestId('movement-type-outbound');
    fireEvent.click(outboundRadio);

    expect(mockSetMovementType).toHaveBeenCalledWith('outbound');
  });

  test('calls setQuantity when the quantity is changed', () => {
    render(
      <WarehouseForm
        products={mockProducts}
        selectedProduct=""
        setSelectedProduct={mockSetSelectedProduct}
        movementType="inbound"
        setMovementType={mockSetMovementType}
        quantity={1}
        setQuantity={mockSetQuantity}
        handleSubmit={mockHandleSubmit}
      />
    );

    const quantityInput = screen.getByTestId('quantity-input');
    fireEvent.change(quantityInput, { target: { value: '5' } });

    expect(mockSetQuantity).toHaveBeenCalledWith(5);
  });

  test('calls handleSubmit when the form is submitted', () => {
    render(
      <WarehouseForm
        products={mockProducts}
        selectedProduct="P001"
        setSelectedProduct={mockSetSelectedProduct}
        movementType="inbound"
        setMovementType={mockSetMovementType}
        quantity={1}
        setQuantity={mockSetQuantity}
        handleSubmit={mockHandleSubmit}
      />
    );

    const form = screen.getByTestId('warehouse-form-component');
    fireEvent.submit(form);

    expect(mockHandleSubmit).toHaveBeenCalled();
  });
});