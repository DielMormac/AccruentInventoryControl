import React, { useState } from 'react';
import { IProductResponse } from '../../../services/Products/IProduct';
import './ReportForm.scss';

interface ReportFormProps {
  onGenerateReport: (movementDate: string, productCode: string) => void;
  products: IProductResponse[];
  selectedProduct: string;
  setSelectedProduct: (productCode: string) => void;
}

const ReportForm: React.FC<ReportFormProps> = ({ 
  onGenerateReport,
  products,
  selectedProduct,
  setSelectedProduct,
}) => {
  const [movementDate, setMovementDate] = useState('');

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    if(!movementDate) return;

    onGenerateReport(movementDate, selectedProduct);
  };

  return (
    <form onSubmit={handleSubmit} data-testid="report-form-component">
      {/* Movement Date */}
      <div className="form-group">
        <label htmlFor="movementDate">Movement Date:</label>
        <input
          type="date"
          id="movementDate"
          className="movement-date"
          value={movementDate}
          onChange={(e) => {
            const date = new Date(e.target.value);
            const formattedDate = date.toISOString().split('T')[0]; // Format as 'YYYY-MM-DD'
            setMovementDate(formattedDate);
          }}
          required
          data-testid="movement-date-input"
        />
      </div>

      {/* Product Code */}
      <div className="form-group">
        <label htmlFor="product">Product:</label>
        <select
          id="product"
          className="selected-product"
          value={selectedProduct}
          onChange={(e) => setSelectedProduct(e.target.value)}
          data-testid="product-select"
        >
          <option value="" data-testid="product-option-all">
            All products
          </option>
          {products
            .sort((a, b) => a.id - b.id)
            .map((product) => (
              <option key={product.id} value={product.code} data-testid={`product-option-${product.code}`}>
                {product.name}
              </option>
            ))}
        </select>
      </div>

      {/* Generate Report Button */}
      <button type="submit" data-testid="generate-report-button">Generate Report</button>
    </form>
  );
};

export default ReportForm;