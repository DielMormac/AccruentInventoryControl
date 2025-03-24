import React, { useEffect, useState } from 'react';
import { fetchProducts } from '../../services/apiService';
import { IProductResponse } from '../../services/Products/IProduct';
import PageHeader from '../../components/Header/PageHeader/PageHeader';
import WarehouseForm from '../../components/Warehouse/WarehourForm/WarehouseForm';
import { postWarehouseTransaction } from '../../services/apiService';
import { IWarehouseTransactionRequest } from '../../services/Warehouse/IWarehouse';
import Toast, { IToastProps } from '../../components/Toast/Toast';

const Warehouse: React.FC = () => {
  const [products, setProducts] = useState<IProductResponse[]>([]);
  const [selectedProduct, setSelectedProduct] = useState('');
  const [movementType, setMovementType] = useState<'inbound' | 'outbound'>('inbound');
  const [quantity, setQuantity] = useState(0);
  const [toast, setToast] = useState<IToastProps | null>(null);  

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

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    console.log({
      productCode: selectedProduct,
      movementType,
      quantity,
    });

    const request: IWarehouseTransactionRequest = {
      productCode: selectedProduct,
      type: movementType,
      quantity,
    }

    const response = await postWarehouseTransaction(request);

    setToast({
      title: 'Success',
      text: `Warehouse transaction registered with success. Product Code: ${response.product.code}, Type: ${response.type}, Quantity: ${response.quantity}.`,
      type: 'success',
      onClose: () => setToast(null),
  });
  };

  return (
    <div className="warehouse-form" data-testid="warehouse-form">
      <PageHeader
        title="Warehouse Transactions"
        subtitle="Register the inbound and outbound warehouse transactions"
        data-testid="page-header"
      />
      <WarehouseForm
        products={products}
        selectedProduct={selectedProduct}
        setSelectedProduct={setSelectedProduct}
        movementType={movementType}
        setMovementType={setMovementType}
        quantity={quantity}
        setQuantity={setQuantity}
        handleSubmit={handleSubmit}
        data-testid="warehouse-form-component"
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
};

export default Warehouse;