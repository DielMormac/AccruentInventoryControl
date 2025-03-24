import React from 'react';
import NavBarItem from './NavBarItem';

interface NavBarProps {
  tabs: string[];
  activeTab: string;
  setActiveTab: (tab: string) => void;
}

const NavBar: React.FC<NavBarProps> = ({ tabs, activeTab, setActiveTab }) => {
  return (
    <div className="navbar-list" data-testid="navbar-list">
      {tabs.map((tab) => (
        <NavBarItem
          key={tab}
          tab={tab}
          activeTab={activeTab}
          setActiveTab={setActiveTab}
          data-testid={`navbar-item-${tab}`}
        />
      ))}
    </div>
  );
};

export default NavBar;