import React from "react";
import { render, screen, fireEvent } from '@testing-library/react';
import ReportForm from './ReportForm';
import { IProductResponse } from '../../../services/Products/IProduct';

describe('ReportForm Component', () => {
  const mockOnGenerateReport = jest.fn();
  const mockSetSelectedProduct = jest.fn();
  const mockProducts: IProductResponse[] = [
    { id: 1, code: 'P001', name: 'Product 1' },
    { id: 2, code: 'P002', name: 'Product 2' },
  ];

  beforeEach(() => {
    jest.clearAllMocks();
  });

  test('renders the form with all elements', () => {
    render(
      <ReportForm
        onGenerateReport={mockOnGenerateReport}
        products={mockProducts}
        selectedProduct=""
        setSelectedProduct={mockSetSelectedProduct}
      />
    );

    expect(screen.getByTestId('report-form-component')).toBeInTheDocument();
    expect(screen.getByTestId('movement-date-input')).toBeInTheDocument();
    expect(screen.getByTestId('product-select')).toBeInTheDocument();
    expect(screen.getByTestId('generate-report-button')).toBeInTheDocument();
  });

  test('renders all product options including "All products"', () => {
    render(
      <ReportForm
        onGenerateReport={mockOnGenerateReport}
        products={mockProducts}
        selectedProduct=""
        setSelectedProduct={mockSetSelectedProduct}
      />
    );

    const allProductsOption = screen.getByTestId('product-option-all');
    expect(allProductsOption).toBeInTheDocument();
    expect(allProductsOption).toHaveTextContent('All products');

    mockProducts.forEach((product) => {
      const productOption = screen.getByTestId(`product-option-${product.code}`);
      expect(productOption).toBeInTheDocument();
      expect(productOption).toHaveTextContent(product.name);
    });
  });

  test('calls setSelectedProduct when a product is selected', () => {
    render(
      <ReportForm
        onGenerateReport={mockOnGenerateReport}
        products={mockProducts}
        selectedProduct=""
        setSelectedProduct={mockSetSelectedProduct}
      />
    );

    const productSelect = screen.getByTestId('product-select');
    fireEvent.change(productSelect, { target: { value: 'P001' } });

    expect(mockSetSelectedProduct).toHaveBeenCalledWith('P001');
  });

  test('updates movement date when a date is selected', () => {
    render(
      <ReportForm
        onGenerateReport={mockOnGenerateReport}
        products={mockProducts}
        selectedProduct=""
        setSelectedProduct={mockSetSelectedProduct}
      />
    );

    const movementDateInput = screen.getByTestId('movement-date-input');
    fireEvent.change(movementDateInput, { target: { value: '2023-01-01' } });

    expect(movementDateInput).toHaveValue('2023-01-01');
  });

  test('calls onGenerateReport with correct values on form submission', () => {
    render(
      <ReportForm
        onGenerateReport={mockOnGenerateReport}
        products={mockProducts}
        selectedProduct="P001"
        setSelectedProduct={mockSetSelectedProduct}
      />
    );

    const movementDateInput = screen.getByTestId('movement-date-input');
    fireEvent.change(movementDateInput, { target: { value: '2023-01-01' } });

    const form = screen.getByTestId('report-form-component');
    fireEvent.submit(form);

    expect(mockOnGenerateReport).toHaveBeenCalledWith('2023-01-01', 'P001');
  });

  test('prevents form submission if movement date is not selected', () => {
    render(
      <ReportForm
        onGenerateReport={mockOnGenerateReport}
        products={mockProducts}
        selectedProduct="P001"
        setSelectedProduct={mockSetSelectedProduct}
      />
    );
  
    const form = screen.getByTestId('report-form-component');
    fireEvent.submit(form);
  
    expect(mockOnGenerateReport).not.toHaveBeenCalled();
  });
});