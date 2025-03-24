import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import './App.scss'
import Warehouse from './pages/Warehouse/Warehouse';
import Report from './pages/Report/Report';
import Header from './components/Header/Header';
import { useState } from 'react';
import NavBar from './components/NavBar/NavBar';
import Footer from './components/Footer/Footer';
import DbInitializer from './components/DbInitializer/DbInitializer';

function App() {
  const tabs = ['Warehouse', 'Report'];
  const [activeTab, setActiveTab] = useState('Warehouse');
  const [databaseInitialized, setDatabaseInitialized] = useState(false);

  return (
    <>
      <Header/>
      {!databaseInitialized ? (
        <DbInitializer onDatabaseInitialized={() => setDatabaseInitialized(true)} />
      ) : (
        <Router>
          <NavBar tabs={tabs} activeTab={activeTab} setActiveTab={setActiveTab} />
          <Routes>
            <Route path="/" element={<Navigate to="/Warehouse" replace />} />
            <Route path="/Warehouse" element={<Warehouse />} />
            <Route path="/Report" element={<Report />} />
          </Routes>
        </Router>
      )}
      <Footer />
    </>
  );
}

export default App;