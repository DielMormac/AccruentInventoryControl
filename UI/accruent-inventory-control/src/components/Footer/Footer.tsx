import React from 'react';
import './Footer.scss';

const Footer: React.FC = () => {
  return (
    <footer className="footer" data-testid="footer">
      <p data-testid="footer-description">
        This project is part of the exercise for the Principal Engineering Role at Accruent.
      </p>
      <p className="read-the-docs" data-testid="footer-note">
        It's not an official Accruent product and there's no relation with the company.
      </p>
    </footer>
  );
};

export default Footer;