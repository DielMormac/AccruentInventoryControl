import { IWarehouseTransactionResponse } from "../Warehouse/IWarehouse";

export interface IWarehouseTransactionReportResponse {
    productCode: string;
    date: Date;
    warehouseTransactions: IWarehouseTransactionResponse[];
    balance: number;
}

export interface IWarehouseTransactionReportRequest {
    date: string;
    productCode: string;
}