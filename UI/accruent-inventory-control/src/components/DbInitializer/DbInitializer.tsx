import React, { useState } from 'react';
import { getInMemoryDatabaseInitializer } from '../../services/apiService';
import './DbInitializer.scss';
import Toast, { IToastProps } from '../Toast/Toast';

interface DbInitializerProps {
  onDatabaseInitialized: () => void;
}

const DbInitializer: React.FC<DbInitializerProps> = ({ onDatabaseInitialized }) => {
  const [isInitializing, setIsInitializing] = useState(false);
  const [toast, setToast] = useState<IToastProps | null>(null);

  const initializeDatabase = async () => {
    setIsInitializing(true);
    try {
      const initialized = await getInMemoryDatabaseInitializer();
      if (initialized) {
        console.log('In-memory database initialized successfully.');

        onDatabaseInitialized();
      }
    } catch (error) {
      console.error('Failed to initialize in-memory database:', error);

      setToast({
        title: 'Error',
        text: 'Failed to initialize in-memory database.',
        type: 'error',
        onClose: () => setToast(null),
      });
    } finally {
      setIsInitializing(false);
    }
  };

  return (
    <div className="db-initializer" data-testid="db-initializer">
      <button
        onClick={initializeDatabase}
        className="btn btn-primary"
        disabled={isInitializing}
        data-testid="initialize-button"
      >
        {isInitializing ? 'Initializing...' : 'Initialize InMemory Database'}
      </button>
      {toast && (
        <Toast
          title={toast.title}
          text={toast.text}
          type={toast.type}
          onClose={() => setToast(null)}
          data-testid="toast"
        />
      )}
    </div>
  );
};

export default DbInitializer;