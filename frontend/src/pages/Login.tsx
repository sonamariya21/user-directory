import { useState } from 'react'
import { useForm } from 'react-hook-form'
import { Navigate, useNavigate } from 'react-router-dom'
import { login } from '../services/api'
import { isAuthenticated, setToken } from '../services/authStorage'

type LoginFormValues = {
  username: string
  password: string
}

const Login = () => {
  const navigate = useNavigate()
  const [formError, setFormError] = useState<string | null>(null)

  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
  } = useForm<LoginFormValues>({
    defaultValues: {
      username: '',
      password: '',
    },
  })

  if (isAuthenticated()) {
    return <Navigate to="/" replace />
  }

  const onSubmit = async (values: LoginFormValues) => {
    setFormError(null)

    try {
      const tokenResponse = await login({
        username: values.username.trim(),
        password: values.password,
      })
      setToken(tokenResponse.accessToken)
      navigate('/', { replace: true })
    } catch {
      setFormError('Invalid username or password.')
    }
  }

  return (
    <section className="panel panel--narrow">
      <div className="panel__header">
        <h1>Login</h1>
      </div>

      <form className="user-form" onSubmit={handleSubmit(onSubmit)} noValidate>
        {formError && (
          <div className="status status--error" role="alert">
            {formError}
          </div>
        )}

        <div className="field">
          <label htmlFor="username">Username</label>
          <input
            id="username"
            type="text"
            autoComplete="username"
            disabled={isSubmitting}
            aria-invalid={Boolean(errors.username)}
            {...register('username', { required: 'Username is required.' })}
          />
          {errors.username && <p className="field__error">{errors.username.message}</p>}
        </div>

        <div className="field">
          <label htmlFor="password">Password</label>
          <input
            id="password"
            type="password"
            autoComplete="current-password"
            disabled={isSubmitting}
            aria-invalid={Boolean(errors.password)}
            {...register('password', { required: 'Password is required.' })}
          />
          {errors.password && <p className="field__error">{errors.password.message}</p>}
        </div>

        <div className="form-actions">
          <button type="submit" className="btn btn--primary" disabled={isSubmitting}>
            {isSubmitting ? 'Signing in…' : 'Login'}
          </button>
        </div>
      </form>
    </section>
  )
}

export default Login
