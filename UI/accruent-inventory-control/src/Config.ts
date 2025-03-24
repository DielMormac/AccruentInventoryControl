const config = {
    apiBaseUrl: process.env.REACT_APP_API_BASE_URL || 'http://localhost:5116/api/v1', // Default to localhost if not set
    timeout: 10000, // Timeout for API requests
};

export default config;