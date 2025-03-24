import React from 'react';
import { IProductResponse } from '../../../services/Products/IProduct';
import './WarehouseForm.scss';

interface WarehouseFormProps {
  products: IProductResponse[];
  selectedProduct: string;
  setSelectedProduct: (productCode: string) => void;
  movementType: 'inbound' | 'outbound';
  setMovementType: (type: 'inbound' | 'outbound') => void;
  quantity: number;
  setQuantity: (quantity: number) => void;
  handleSubmit: (e: React.FormEvent) => void;
}

const WarehouseForm: React.FC<WarehouseFormProps> = ({
  products,
  selectedProduct,
  setSelectedProduct,
  movementType,
  setMovementType,
  quantity,
  setQuantity,
  handleSubmit,
}) => {
  return (
    <form onSubmit={handleSubmit} data-testid="warehouse-form-component">
      {/* Product Dropdown */}
      <div className="form-group">
        <label htmlFor="product">Product:</label>
        <select
          id="product"
          className="warehouse-selected-product"
          value={selectedProduct}
          onChange={(e) => setSelectedProduct(e.target.value)}
          required
          data-testid="product-select"
        >
          <option value="" disabled data-testid="product-option-default">
            Select a product
          </option>
          {products
            .sort((a, b) => a.id - b.id)
            .map((product) => (
              <option
                key={product.id}
                value={product.code}
                data-testid={`product-option-${product.code}`}
              >
                {product.name}
              </option>
            ))}
        </select>
      </div>

      {/* Movement Type Radio */}
      <div className="form-group">
        <label>Movement Type:</label>
        <div>
          <label>
            <input
              type="radio"
              name="movementType"
              value="inbound"
              checked={movementType === 'inbound'}
              onChange={(e) => setMovementType(e.target.value as 'inbound' | 'outbound')}
              data-testid="movement-type-inbound"
            />
            Inbound
          </label>
          <label>
            <input
              type="radio"
              name="movementType"
              value="outbound"
              checked={movementType === 'outbound'}
              onChange={(e) => setMovementType(e.target.value as 'inbound' | 'outbound')}
              data-testid="movement-type-outbound"
            />
            Outbound
          </label>
        </div>
      </div>

      {/* Quantity Input */}
      <div className="form-group">
        <label htmlFor="quantity">Quantity:</label>
        <input
          type="number"
          id="quantity"
          className="quantity-input"
          value={quantity}
          onChange={(e) => setQuantity(Number(e.target.value))}
          min="1"
          required
          data-testid="quantity-input"
        />
      </div>

      {/* Submit Button */}
      <button type="submit" data-testid="submit-button">Submit</button>
    </form>
  );
};

export default WarehouseForm;