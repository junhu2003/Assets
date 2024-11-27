import React from 'react'
import ReactDOM from 'react-dom/client'
// Bootstrap CSS
import "bootstrap/dist/css/bootstrap.min.css";

import App from './App.jsx'
import './index.css'
import Container from 'react-bootstrap/Container';

ReactDOM.createRoot(document.getElementById('root')).render(
    <React.StrictMode>
        <App />
    </React.StrictMode>,
)
