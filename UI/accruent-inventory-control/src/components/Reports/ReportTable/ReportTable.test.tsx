import React from "react";
import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom';
import ReportTable from './ReportTable';

describe('ReportTable Component', () => {
    const mockReportData = [
        {
            name: 'Product 1',
            code: 'P001',
            type: 'Type1',
            status: 'Active',
            quantity: 10,
            previousBalance: 5,
            balance: 15,
        },
        {
            name: 'Product 2',
            code: 'P002',
            type: 'Type2',
            status: 'Inactive',
            quantity: 20,
            previousBalance: 10,
            balance: 30,
        },
    ];

    test('renders the ReportTable component', () => {
        render(<ReportTable reportData={mockReportData} />);
        const reportTable = screen.getByTestId('report-table');
        expect(reportTable).toBeInTheDocument();
    });

    test('renders the table headers correctly', () => {
        render(<ReportTable reportData={mockReportData} />);
        expect(screen.getByTestId('header-name')).toHaveTextContent('Name');
        expect(screen.getByTestId('header-code')).toHaveTextContent('Code');
        expect(screen.getByTestId('header-type')).toHaveTextContent('Type');
        expect(screen.getByTestId('header-status')).toHaveTextContent('Status');
        expect(screen.getByTestId('header-quantity')).toHaveTextContent('Quantity');
        expect(screen.getByTestId('header-previous-balance')).toHaveTextContent('Previous Balance');
        expect(screen.getByTestId('header-balance')).toHaveTextContent('Balance');
    });

    test('renders rows for each report item', () => {
        render(<ReportTable reportData={mockReportData} />);
        mockReportData.forEach((item, index) => {
            expect(screen.getByTestId(`report-row-${index}`)).toBeInTheDocument();
            expect(screen.getByTestId(`row-${index}-name`)).toHaveTextContent(item.name);
            expect(screen.getByTestId(`row-${index}-code`)).toHaveTextContent(item.code);
            expect(screen.getByTestId(`row-${index}-type`)).toHaveTextContent(item.type);
            expect(screen.getByTestId(`row-${index}-status`)).toHaveTextContent(item.status);
            expect(screen.getByTestId(`row-${index}-quantity`)).toHaveTextContent(item.quantity.toString());
            expect(screen.getByTestId(`row-${index}-previous-balance`)).toHaveTextContent(item.previousBalance.toString());
            expect(screen.getByTestId(`row-${index}-balance`)).toHaveTextContent(item.balance.toString());
        });
    });

    test('renders "No transactions found" when reportData is empty', () => {
        render(<ReportTable reportData={[]} />);
        const noTransactions = screen.getByTestId('no-transactions');
        expect(noTransactions).toBeInTheDocument();
        expect(noTransactions).toHaveTextContent('No transactions found.');
    });

    test('applies the correct class based on the type of the report item', () => {
        render(<ReportTable reportData={mockReportData} />);
        mockReportData.forEach((item, index) => {
            const row = screen.getByTestId(`report-row-${index}`);
            if (item.status) {
                expect(row).toHaveClass(item.type.toLowerCase());
            }
        });
    });
});