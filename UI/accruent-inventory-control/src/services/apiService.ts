import axios from 'axios';
import { IProductResponse } from './Products/IProduct';
import { IWarehouseTransactionRequest, IWarehouseTransactionResponse } from './Warehouse/IWarehouse';
import { IWarehouseTransactionReportRequest, IWarehouseTransactionReportResponse } from './Reports/IWarehouseTransactionReport';
import config from '../Config';

const apiClient = axios.create({
    baseURL: config.apiBaseUrl,
    timeout: config.timeout,
    headers: {
        'Content-Type': 'application/json',
    },
    withCredentials: false,
});

// Fetch Products
export const fetchProducts = async (): Promise<IProductResponse[]> => {
    try {
        const response = await apiClient.get<IProductResponse[]>('/Product');
        return response.data;
    } catch (error) {
        console.error('Error fetching products:', error);
        throw error;
    }
};

// Fetch Warehouse Transactions
export const fetchWarehouseTransactions = async (): Promise<IWarehouseTransactionResponse[]> => {
    try {
        const response = await apiClient.get<IWarehouseTransactionResponse[]>('/WarehouseTransaction');
        return response.data;
    } catch (error) {
        console.error('Error fetching warehouse transactions:', error);
        throw error;
    }
};

// Post InMemoryDatabaseInitializer
export const getInMemoryDatabaseInitializer = async () => {
    try {
        const response = await apiClient.get('/InMemoryDatabaseInitializer');
        return response.status === 200;
    } catch (error) {
        console.error('Error posting warehouse transaction:', error);
        throw error;
    }
};

// Post Warehouse Transaction
export const postWarehouseTransaction = async (
    data: IWarehouseTransactionRequest
): Promise<IWarehouseTransactionResponse> => {
    try {
        const response = await apiClient.post<IWarehouseTransactionResponse>('/WarehouseTransaction', data);
        return response.data;
    } catch (error) {
        console.error('Error posting warehouse transaction:', error);
        throw error;
    }
};

// Generate Report
export const generateReport = async (
    data: IWarehouseTransactionReportRequest
): Promise<IWarehouseTransactionReportResponse> => {
    try {
        const response = await apiClient.post<IWarehouseTransactionReportResponse>(
            '/Report/WarehouseTransaction',
            data
        );
        return response.data;
    } catch (error) {
        console.error('Error generating report:', error);
        throw error;
    }
};

export default apiClient;