import React from 'react'; // Explicitly import React to avoid ReferenceError
import { render, screen, fireEvent, waitFor } from '@testing-library/react'; // Import required testing utilities
import Home from './Home';

describe('Home Component', () => {
    test('renders the Header component', () => {
        render(<Home />);
        const headerElement = screen.getByTestId('header');
        expect(headerElement).toBeInTheDocument();
    });

    test('renders the DbInitializer component when database is not initialized', () => {
        render(<Home />);
        const dbInitializerElement = screen.getByTestId('db-initializer');
        expect(dbInitializerElement).toBeInTheDocument();
    });

    test('renders the NavBar component when database is initialized', () => {
        render(<Home />);
        const dbInitializerButton = screen.getByTestId('initialize-button');
        fireEvent.click(dbInitializerButton); // Simulate database initialization

        waitFor(async () => {
            const toast = screen.getByTestId('toast');
            expect(toast).toBeInTheDocument();
        });

        waitFor(async () => {
            const navBarElement = await screen.findByTestId('navbar-list');
            expect(navBarElement).toBeInTheDocument();
        });
    });

    test('renders the Footer component', () => {
        render(<Home />);
        const footerElement = screen.getByTestId('footer');
        expect(footerElement).toBeInTheDocument();
    });

    test('renders the correct routes', () => {
        render(<Home />);
        const dbInitializerButton = screen.getByTestId('initialize-button');
        fireEvent.click(dbInitializerButton); // Simulate database initialization

        waitFor(async () => {
            const toast = await screen.getByTestId('toast');
            expect(toast).toBeInTheDocument();
        });

        const homeRoute = screen.getByTestId('home-component');
        fireEvent.click(homeRoute);
        expect(screen.getByTestId('home-component')).toBeInTheDocument();
    });
});