import React, { useEffect } from 'react';
import './Toast.scss';

export interface IToastProps {
  title: string;
  text: string;
  type: 'success' | 'error'; // Determines the color (green for success, red for error)
  timeout?: number; // Timeout in seconds (default = 5 seconds)
  onClose: () => void; // Callback to close the toast
}

const Toast: React.FC<IToastProps> = ({ title, text, type, timeout = 2, onClose }) => {
  useEffect(() => {
    const timer = setTimeout(() => {
      onClose();
    }, timeout * 1000);

    return () => clearTimeout(timer); // Cleanup the timer on unmount
  }, [timeout, onClose]);

  return (
    <div className={`toast toast-${type}`} data-testid="toast">
      <div className="toast-header" data-testid="toast-header">
        <strong data-testid="toast-title">{title}</strong>
        <button className="toast-close" onClick={onClose} data-testid="toast-close">
          &times;
        </button>
      </div>
      <div className="toast-body" data-testid="toast-body">{text}</div>
    </div>
  );
};

export default Toast;