import { Config } from 'jest';

const config: Config = {
  preset: 'ts-jest',
  testEnvironment: 'jest-environment-jsdom',
  transform: {
    '^.+\\.(ts|tsx)$': 'babel-jest', // Use Babel to transform TypeScript and JSX
  },
  moduleNameMapper: {
    '\\.(css|scss)$': 'identity-obj-proxy', // Mock CSS/SCSS imports
    '\\.svg$': '<rootDir>/svgMock.js', // Mock SVG imports
  },
  setupFilesAfterEnv: ['<rootDir>/jest.setup.ts'], // Setup file for Jest
};

export default config;