import React from "react";
import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import Report from './Report';
import { fetchProducts, generateReport } from '../../services/apiService';
import { IProductResponse } from '../../services/Products/IProduct';


jest.mock('../../services/apiService', () => ({
    fetchProducts: jest.fn(),
    generateReport: jest.fn(),
}));

describe('Report Page', () => {
    const mockProducts: IProductResponse[] = [
        { id: 1, code: 'P001', name: 'Product 1' },
        { id: 2, code: 'P002', name: 'Product 2' },
    ];

    beforeEach(() => {
        jest.clearAllMocks();
    });

    test('renders the Report page with all components', async () => {
        (fetchProducts as jest.Mock).mockResolvedValue(mockProducts);

        render(<Report />);

        expect(screen.getByTestId('page-header')).toBeInTheDocument();
        expect(screen.getByTestId('report-form-component')).toBeInTheDocument();
        expect(screen.getByTestId('report-table')).toBeInTheDocument();

        await waitFor(() => {
            expect(fetchProducts).toHaveBeenCalledTimes(1);
        });
    });

    test('loads and displays products in the ReportForm', async () => {
        (fetchProducts as jest.Mock).mockResolvedValue(mockProducts);

        render(<Report />);

        await waitFor(() => {
            expect(fetchProducts).toHaveBeenCalledTimes(1);
        });

        const productSelect = screen.getByTestId('product-select');
        fireEvent.change(productSelect, { target: { value: 'P001' } });

        expect(productSelect).toHaveValue('P001');
    });

    test('handles successful report generation', async () => {
        (fetchProducts as jest.Mock).mockResolvedValue(mockProducts);
        (generateReport as jest.Mock).mockResolvedValue({
            warehouseTransactions: [
                {
                    product: { name: 'Product 1', code: 'P001' },
                    type: 'inbound',
                    status: 'Active',
                    quantity: 10,
                    previousQuantity: 5,
                    totalQuantity: 15,
                },
            ],
        });

        render(<Report />);

        await waitFor(() => {
            expect(fetchProducts).toHaveBeenCalledTimes(1);
        });

        const movementDateInput = screen.getByTestId('movement-date-input');
        fireEvent.change(movementDateInput, { target: { value: '2023-01-01' } });

        const form = screen.getByTestId('report-form-component');
        fireEvent.submit(form);

        await waitFor(() => {
            expect(generateReport).toHaveBeenCalledTimes(1);
            expect(screen.getByTestId('toast')).toHaveTextContent('Success');
        });
    });

    test('handles report generation with no transactions', async () => {
        (fetchProducts as jest.Mock).mockResolvedValue(mockProducts);
        (generateReport as jest.Mock).mockResolvedValue(null);

        render(<Report />);

        await waitFor(() => {
            expect(fetchProducts).toHaveBeenCalledTimes(1);
        });

        const movementDateInput = screen.getByTestId('movement-date-input');
        fireEvent.change(movementDateInput, { target: { value: '2023-01-01' } });

        const form = screen.getByTestId('report-form-component');
        fireEvent.submit(form);

        await waitFor(() => {
            expect(generateReport).toHaveBeenCalledTimes(1);
            expect(screen.getByTestId('toast')).toHaveTextContent("There's no transactions");
        });
    });

    test('handles report generation failure', async () => {
        (fetchProducts as jest.Mock).mockResolvedValue(mockProducts);
        (generateReport as jest.Mock).mockRejectedValue(new Error('Failed to generate report'));

        render(<Report />);

        await waitFor(() => {
            expect(fetchProducts).toHaveBeenCalledTimes(1);
        });

        const movementDateInput = screen.getByTestId('movement-date-input');
        fireEvent.change(movementDateInput, { target: { value: '2023-01-01' } });

        const form = screen.getByTestId('report-form-component');
        fireEvent.submit(form);

        await waitFor(() => {
            expect(generateReport).toHaveBeenCalledTimes(1);
            expect(screen.getByTestId('toast')).toHaveTextContent('Failed to generate report');
        });
    });
});