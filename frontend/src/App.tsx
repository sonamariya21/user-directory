import { BrowserRouter, Navigate, Routes, Route } from 'react-router-dom'
import NavBar from './components/NavBar'
import ProtectedRoute from './components/ProtectedRoute'
import UserList from './pages/UserList'
import AddUser from './pages/AddUser'
import Login from './pages/Login'

const App = () => {
  return (
    <BrowserRouter>
      <NavBar />
      <main className="page">
        <Routes>
          <Route path="/login" element={<Login />} />
          <Route element={<ProtectedRoute />}>
            <Route path="/" element={<UserList />} />
            <Route path="/add" element={<AddUser />} />
          </Route>
          <Route path="*" element={<Navigate to="/" replace />} />
        </Routes>
      </main>
    </BrowserRouter>
  )
}

export default App
