import { IProductResponse } from "../Products/IProduct";

export interface IWarehouseTransactionResponse {
    id: number;
    product: IProductResponse;
    quantity: number;
    type: string;
    status: string;
    date: Date;
    previousQuantity: number;
    totalQuantity: number;
}

export interface IWarehouseTransactionRequest {
    productCode: string;
    type: 'inbound' | 'outbound';
    quantity: number;
}