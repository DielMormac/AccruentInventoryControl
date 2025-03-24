import React from 'react';
import './PageHeader.scss';

interface PageHeaderProps {
  title: string;
  subtitle: string;
}

const PageHeader: React.FC<PageHeaderProps> = ({ title, subtitle }) => {
  return (
    <div className="page-header" data-testid="page-header">
      <h2 data-testid="page-header-title">{title}</h2>
      <h4 data-testid="page-header-subtitle">{subtitle}</h4>
    </div>
  );
};

export default PageHeader;