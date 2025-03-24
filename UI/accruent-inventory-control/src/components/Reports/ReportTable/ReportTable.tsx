import React from 'react';
import './ReportTable.scss';

interface IReportItem {
  name: string;
  code: string;
  type: string;
  status: string;
  quantity: number;
  previousBalance: number;
  balance: number;
}

interface ReportTableProps {
  reportData: IReportItem[];
}

const ReportTable: React.FC<ReportTableProps> = ({ reportData }) => {
  return (
    <div className="report-table" data-testid="report-table">
      <h2 data-testid="report-table-title">Report Results</h2>
      <table data-testid="report-table-element">
        <thead>
          <tr>
            <th data-testid="header-name">Name</th>
            <th data-testid="header-code">Code</th>
            <th data-testid="header-type">Type</th>
            <th data-testid="header-status">Status</th>
            <th data-testid="header-quantity">Quantity</th>
            <th data-testid="header-previous-balance">Previous Balance</th>
            <th data-testid="header-balance">Balance</th>
          </tr>
        </thead>
        <tbody>
          {reportData.length > 0 ? (
            reportData.map((item, index) => (
              <tr
                key={index}
                className={item.status ? item.type.toLowerCase() : ''}
                data-testid={`report-row-${index}`}
              >
                <td data-testid={`row-${index}-name`}>{item.name}</td>
                <td data-testid={`row-${index}-code`}>{item.code}</td>
                <td data-testid={`row-${index}-type`}>{item.type}</td>
                <td data-testid={`row-${index}-status`}>{item.status}</td>
                <td data-testid={`row-${index}-quantity`}>{item.quantity}</td>
                <td data-testid={`row-${index}-previous-balance`}>{item.previousBalance}</td>
                <td data-testid={`row-${index}-balance`}>{item.balance}</td>
              </tr>
            ))
          ) : (
            <tr>
              <td
                colSpan={7}
                className="no-transactions"
                data-testid="no-transactions"
              >
                No transactions found.
              </td>
            </tr>
          )}
        </tbody>
      </table>
    </div>
  );
};

export default ReportTable;