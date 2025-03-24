import React from 'react';
import accruentLogo from '../../assets/accruent-logo-color.svg';
import './Header.scss';

const Header: React.FC = () => {
  return (
    <header className="header" data-testid="header">
      <a
        href="https://www.accruent.com/"
        target="_blank"
        rel="noopener noreferrer"
        data-testid="header-link"
      >
        <img
          src={accruentLogo}
          className="logo"
          alt="Accruent logo"
          data-testid="header-logo"
        />
      </a>
      <h2 data-testid="header-title">Accruent Inventory Control</h2>
    </header>
  );
};

export default Header;