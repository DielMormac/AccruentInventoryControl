import React from "react";
import { render, screen, fireEvent } from '@testing-library/react';
import '@testing-library/jest-dom';
import NavBar from './NavBar';
import { MemoryRouter } from "react-router-dom";

test('renders the NavBar component', () => {
  const tabs = ['Tab1', 'Tab2'];
  const activeTab = 'Tab1';
  const setActiveTab = jest.fn();

  render(
    <MemoryRouter>
      <NavBar tabs={tabs} activeTab={activeTab} setActiveTab={setActiveTab} />
    </MemoryRouter>
  );
  const navbarList = screen.getByTestId('navbar-list');
  expect(navbarList).toBeInTheDocument();
});

test('renders all NavBarItem components for the provided tabs', () => {
  const tabs = ['Tab1', 'Tab2', 'Tab3'];
  const activeTab = 'Tab1';
  const setActiveTab = jest.fn();

  render(
    <MemoryRouter>
      <NavBar tabs={tabs} activeTab={activeTab} setActiveTab={setActiveTab} />
    </MemoryRouter>
  );
  tabs.forEach((tab) => {
    const navbarItem = screen.getByTestId(`navbar-item-${tab}`);
    expect(navbarItem).toBeInTheDocument();
  });
});

test('passes the correct props to NavBarItem components', () => {
  const tabs = ['Tab1', 'Tab2'];
  const activeTab = 'Tab1';
  const setActiveTab = jest.fn();

  render(
    <MemoryRouter>
      <NavBar tabs={tabs} activeTab={activeTab} setActiveTab={setActiveTab} />
    </MemoryRouter>
  );
  tabs.forEach((tab) => {
    const navbarItem = screen.getByTestId(`navbar-item-${tab}`);
    expect(navbarItem).toHaveAttribute('data-testid', `navbar-item-${tab}`);
  });
});

test('calls setActiveTab when a NavBarItem is clicked', () => {
  const tabs = ['Tab1', 'Tab2'];
  const activeTab = 'Tab1';
  const setActiveTab = jest.fn();

  render(
    <MemoryRouter>
      <NavBar tabs={tabs} activeTab={activeTab} setActiveTab={setActiveTab} />
    </MemoryRouter>
  );

  const navbarItem = screen.getByTestId('navbar-item-Tab2');
  fireEvent.click(navbarItem);
  expect(setActiveTab).toHaveBeenCalledWith('Tab2');
});