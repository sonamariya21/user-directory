import { NavLink, useNavigate } from 'react-router-dom'
import { clearToken, isAuthenticated } from '../services/authStorage'

const NavBar = () => {
  const navigate = useNavigate()
  const loggedIn = isAuthenticated()

  const handleLogout = () => {
    clearToken()
    navigate('/login', { replace: true })
  }

  return (
    <header className="navbar">
      <div className="navbar__brand">User Directory</div>
      <nav className="navbar__links" aria-label="Main">
        {loggedIn ? (
          <>
            <NavLink to="/" end className={({ isActive }) => (isActive ? 'nav-link active' : 'nav-link')}>
              List
            </NavLink>
            <NavLink to="/add" className={({ isActive }) => (isActive ? 'nav-link active' : 'nav-link')}>
              Add
            </NavLink>
            <button type="button" className="btn" onClick={handleLogout}>
              Logout
            </button>
          </>
        ) : (
          <NavLink to="/login" className={({ isActive }) => (isActive ? 'nav-link active' : 'nav-link')}>
            Login
          </NavLink>
        )}
      </nav>
    </header>
  )
}

export default NavBar
