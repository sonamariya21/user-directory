import { useState } from 'react'
import CircularProgress from '@mui/material/CircularProgress'
import { useForm } from 'react-hook-form'
import { useNavigate } from 'react-router-dom'
import { addUser } from '../services/api'
import type { UserFormValues } from '../types/User'
import { userFormRules } from '../validations/userForm'

const defaultValues: UserFormValues = {
  name: '',
  age: '',
  city: '',
  state: '',
  pincode: '',
}

const AddUser = () => {
  const navigate = useNavigate()
  const [formError, setFormError] = useState<string | null>(null)

  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
  } = useForm<UserFormValues>({
    defaultValues,
    mode: 'onBlur',
  })

  const onSubmit = async (values: UserFormValues) => {
    setFormError(null)

    try {
      await addUser({
        name: values.name.trim(),
        age: Number(values.age.trim()),
        city: values.city.trim(),
        state: values.state.trim(),
        pincode: values.pincode.trim(),
      })
      navigate('/', { state: { message: 'User created successfully.' } })
    } catch {
      setFormError('Could not create user. Please try again.')
    }
  }

  return (
    <section className="panel panel--narrow">
      <div className="panel__header">
        <h1>Add user</h1>
      </div>

      <form className="user-form" onSubmit={handleSubmit(onSubmit)} noValidate>
        {formError && (
          <div className="status status--error" role="alert">
            {formError}
          </div>
        )}

        <div className="field">
          <label htmlFor="name">Name</label>
          <input
            id="name"
            type="text"
            disabled={isSubmitting}
            aria-invalid={Boolean(errors.name)}
            aria-describedby={errors.name ? 'name-error' : undefined}
            {...register('name', userFormRules.name)}
          />
          {errors.name && (
            <p id="name-error" className="field__error">
              {errors.name.message}
            </p>
          )}
        </div>

        <div className="field">
          <label htmlFor="age">Age</label>
          <input
            id="age"
            type="text"
            inputMode="numeric"
            disabled={isSubmitting}
            aria-invalid={Boolean(errors.age)}
            aria-describedby={errors.age ? 'age-error' : undefined}
            {...register('age', userFormRules.age)}
          />
          {errors.age && (
            <p id="age-error" className="field__error">
              {errors.age.message}
            </p>
          )}
        </div>

        <div className="field">
          <label htmlFor="city">City</label>
          <input
            id="city"
            type="text"
            disabled={isSubmitting}
            aria-invalid={Boolean(errors.city)}
            aria-describedby={errors.city ? 'city-error' : undefined}
            {...register('city', userFormRules.city)}
          />
          {errors.city && (
            <p id="city-error" className="field__error">
              {errors.city.message}
            </p>
          )}
        </div>

        <div className="field">
          <label htmlFor="state">State</label>
          <input
            id="state"
            type="text"
            disabled={isSubmitting}
            aria-invalid={Boolean(errors.state)}
            aria-describedby={errors.state ? 'state-error' : undefined}
            {...register('state', userFormRules.state)}
          />
          {errors.state && (
            <p id="state-error" className="field__error">
              {errors.state.message}
            </p>
          )}
        </div>

        <div className="field">
          <label htmlFor="pincode">Pincode</label>
          <input
            id="pincode"
            type="text"
            disabled={isSubmitting}
            aria-invalid={Boolean(errors.pincode)}
            aria-describedby={errors.pincode ? 'pincode-error' : undefined}
            {...register('pincode', userFormRules.pincode)}
          />
          {errors.pincode && (
            <p id="pincode-error" className="field__error">
              {errors.pincode.message}
            </p>
          )}
        </div>

        <div className="form-actions">
          <button
            type="button"
            className="btn"
            disabled={isSubmitting}
            onClick={() => navigate('/')}
          >
            Cancel
          </button>
          <button type="submit" className="btn btn--primary" disabled={isSubmitting}>
            {isSubmitting ? (
              <>
                <CircularProgress size={16} sx={{ color: '#fff' }} />
                Saving…
              </>
            ) : (
              'Save'
            )}
          </button>
        </div>
      </form>
    </section>
  )
}

export default AddUser
