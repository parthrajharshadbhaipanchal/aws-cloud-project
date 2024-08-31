import './App.css';
import Sidebar from './components/Sidebar';
import Home from './pages/Home';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap-icons/font/bootstrap-icons.css';
import CreateJobApplicationForm from './components/CreateJobApplicationForm';

function App() {
  return (
    <div className='row col-12' style={{ height: '100vh' }}>
      <div className='col-2'>
        <Sidebar></Sidebar>
      </div>
      <div className='col-10'>
        <Router>
          <Routes>
            <Route path="/" element={<Navigate to="/home" />} />
            <Route path="/home" element={<Home />} />
            <Route path="/create" element={<CreateJobApplicationForm />} />
          </Routes>
        </Router>
      </div>
    </div>
  );
}

export default App;
