import { useEffect, useState } from 'react'
import CircularProgress from '@mui/material/CircularProgress'
import { Link, useLocation, useNavigate } from 'react-router-dom'
import { getAllUsers } from '../services/api'
import type { User } from '../types/User'

type LocationState = {
  message?: string
}

const UserList = () => {
  const location = useLocation()
  const navigate = useNavigate()
  const [users, setUsers] = useState<User[]>([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)
  const [toast, setToast] = useState<string | null>(null)

  useEffect(() => {
    const state = location.state as LocationState | null
    if (state?.message) {
      setToast(state.message)
      navigate('.', { replace: true, state: {} })
    }
  }, [location.state, navigate])

  useEffect(() => {
    if (!toast) return
    const timer = window.setTimeout(() => setToast(null), 4000)
    return () => window.clearTimeout(timer)
  }, [toast])

  useEffect(() => {
    let cancelled = false

    const loadUsers = async () => {
      setLoading(true)
      setError(null)
      try {
        const data = await getAllUsers()
        if (!cancelled) setUsers(data)
      } catch {
        if (!cancelled) setError('Could not load users. Check that the API is running and try again.')
      } finally {
        if (!cancelled) setLoading(false)
      }
    }

    void loadUsers()
    return () => {
      cancelled = true
    }
  }, [])

  const handleRetry = async () => {
    setLoading(true)
    setError(null)
    try {
      const data = await getAllUsers()
      setUsers(data)
    } catch {
      setError('Could not load users. Check that the API is running and try again.')
    } finally {
      setLoading(false)
    }
  }

  return (
    <section className="panel">
      <div className="panel__header">
        <h1>Users</h1>
        <Link to="/add" className="btn btn--primary">
          Add user
        </Link>
      </div>

      {toast && (
        <div className="toast toast--success" role="status">
          {toast}
          <button type="button" className="toast__close" onClick={() => setToast(null)} aria-label="Dismiss">
            ×
          </button>
        </div>
      )}

      {loading && (
        <div className="loading" role="status" aria-live="polite">
          <CircularProgress size={28} sx={{ color: '#222' }} />
          <span>Loading users…</span>
        </div>
      )}

      {!loading && error && (
        <div className="status status--error" role="alert">
          <p>{error}</p>
          <button type="button" className="btn" onClick={() => void handleRetry()}>
            Retry
          </button>
        </div>
      )}

      {!loading && !error && users.length === 0 && (
        <div className="status">
          <p>No users found.</p>
          <Link to="/add" className="btn btn--primary">
            Add the first user
          </Link>
        </div>
      )}

      {!loading && !error && users.length > 0 && (
        <div className="table-wrap">
          <table className="user-table">
            <thead>
              <tr>
                <th>Name</th>
                <th>Age</th>
                <th>City</th>
                <th>State</th>
                <th>Pincode</th>
              </tr>
            </thead>
            <tbody>
              {users.map((user) => (
                <tr key={user.id ?? `${user.name}-${user.pincode}`}>
                  <td>{user.name}</td>
                  <td>{user.age}</td>
                  <td>{user.city}</td>
                  <td>{user.state}</td>
                  <td>{user.pincode}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </section>
  )
}

export default UserList
