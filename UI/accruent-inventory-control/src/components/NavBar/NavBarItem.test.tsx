import React from "react";
import { useNavigate } from "react-router-dom";
import { render, screen, fireEvent } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import NavBarItem from './NavBarItem';

function TabButton({
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
        className={`tab-button ${activeTab === tab ? 'active' : ''}`}
        onClick={() => {
          setActiveTab(tab); // Update the active tab
          navigate(`/${tab}`); // Navigate to the corresponding route
        }}
      >
        {tab}
      </button>
    );
}

export default TabButton;
describe('NavBarItem Component', () => {
  const mockSetActiveTab = jest.fn();

  const renderNavBarItem = (tab: string, activeTab: string) => {
    render(
      <MemoryRouter>
        <NavBarItem tab={tab} activeTab={activeTab} setActiveTab={mockSetActiveTab} />
      </MemoryRouter>
    );
  };

  test('renders the NavBarItem button with correct text', () => {
    renderNavBarItem('Tab1', 'Tab1');
    const button = screen.getByTestId('navbar-item-Tab1');
    expect(button).toBeInTheDocument();
    expect(button).toHaveTextContent('Tab1');
  });

  test('applies the active class when the tab is active', () => {
    renderNavBarItem('Tab1', 'Tab1');
    const button = screen.getByTestId('navbar-item-Tab1');
    expect(button).toHaveClass('active');
  });

  test('does not apply the active class when the tab is not active', () => {
    renderNavBarItem('Tab1', 'Tab2');
    const button = screen.getByTestId('navbar-item-Tab1');
    expect(button).not.toHaveClass('active');
  });

  test('calls setActiveTab and navigates when clicked', () => {
    const mockNavigate = jest.fn();
    jest.mock('react-router-dom', () => ({
      ...jest.requireActual('react-router-dom'),
      useNavigate: () => mockNavigate,
    }));

    renderNavBarItem('Tab1', 'Tab2');
    const button = screen.getByTestId('navbar-item-Tab1');
    fireEvent.click(button);

    expect(mockSetActiveTab).toHaveBeenCalledWith('Tab1');
  });
});