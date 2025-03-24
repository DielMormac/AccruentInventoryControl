import React from "react";
import { useEffect, useState } from 'react';
import ReportTable from '../../components/Reports/ReportTable/ReportTable';
import ReportForm from '../../components/Reports/ReportForm/ReportForm';
import PageHeader from '../../components/Header/PageHeader/PageHeader';
import { fetchProducts, generateReport } from '../../services/apiService';
import { IWarehouseTransactionReportRequest } from '../../services/Reports/IWarehouseTransactionReport';
import { IWarehouseTransactionResponse } from '../../services/Warehouse/IWarehouse';
import Toast, { IToastProps } from '../../components/Toast/Toast';
import { IProductResponse } from '../../services/Products/IProduct';

interface IReportItem {
    name: string;
    code: string;
    type: string;
    status: string;
    quantity: number;
    previousBalance: number;
    balance: number;
};

function Report() {
    const [reportData, setReportData] = useState([] as IReportItem[]);
    const [toast, setToast] = useState<IToastProps | null>(null); 
    const [products, setProducts] = useState<IProductResponse[]>([]);
    const [selectedProduct, setSelectedProduct] = useState('');

    useEffect(() => {
        if(products.length > 0) return;
    
        const loadProducts = async () => {
          try {
            const data = await fetchProducts();
            setProducts(data);
          } catch (error) {
            console.error('Failed to fetch products:', error);
          }
        };
    
        loadProducts();
    }, []);

    const handleGenerateReport = async (date: string, productCode: string) => {
        try {
            const request: IWarehouseTransactionReportRequest = { date, productCode };
            const response = await generateReport(request);
            
            if(!response)
            {
                setReportData([]);
        
                setToast({
                    title: 'Error',
                    text: `There's no transactions of ${productCode ? 'product code: ' + productCode : 'all products'} on ${date} to be displayed.`,
                    type: 'error',
                    onClose: () => setToast(null),
                });
                return;
            }
            
            const formattedData = response.warehouseTransactions.map((item: IWarehouseTransactionResponse) => ({
                name: item.product.name,
                code: item.product.code,
                type: item.type,
                status: item.status,
                quantity: item.quantity,
                previousBalance: item.previousQuantity,
                balance: item.totalQuantity,
            }));

            setReportData(formattedData);

            setToast({
                title: 'Success',
                text: `Report generated for all transactions of ${productCode ? 'product code: ' + productCode : 'all products'} on ${date}.`,
                type: 'success',
                onClose: () => setToast(null),
            });
            
        } catch (error) {
            console.error('Error generating report:', error);

            setToast({
                title: 'Error',
                text: `Failed to generate report. Please try again. Error: ${error}`,
                type: 'error',
                onClose: () => setToast(null),
            });
        }
    };

    return (
    <div className="report-form" data-testid="report-form">
        <PageHeader 
            title="Stock Report" 
            subtitle="Visualize stock report from products in a specific date" 
            data-testid="page-header"
        />
        {/* Use the ReportForm Component */}
        <ReportForm 
            onGenerateReport={handleGenerateReport} 
            products={products}
            selectedProduct={selectedProduct}
            setSelectedProduct={setSelectedProduct}
            data-testid="report-form-component"
        />
        {/* Use the ReportTable Component */}
        <ReportTable 
            reportData={reportData} 
            data-testid="report-table"
        />
        {toast && (
            <Toast
                title={toast.title}
                text={toast.text}
                type={toast.type}
                onClose={() => setToast(null)}
                data-testid="toast"
            />
        )}
    </div>
    );
}

export default Report;