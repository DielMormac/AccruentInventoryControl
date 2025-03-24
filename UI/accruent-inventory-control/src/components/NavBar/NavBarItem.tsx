import React from "react";
import { useNavigate } from "react-router-dom";
import './NavBarItem.scss';

function NavBarItem({
  tab,
  activeTab,
  setActiveTab,
  }: {
  tab: string;
  activeTab: string;
  setActiveTab: (tab: string) => void;
  }) 
  {
  const navigate = useNavigate();
  
  return (
    <button
    key={tab}
    className={`navbar-item-button ${activeTab === tab ? 'active' : ''} ${tab}`}
    data-testid={`navbar-item-${tab}`} // Added data-testid for testing
    onClick={() => {
      setActiveTab(tab); // Update the active tab
      navigate(`/${tab}`); // Navigate to the corresponding route
    }}
    >
    {tab}
    </button>
  );
}

export default NavBarItem;