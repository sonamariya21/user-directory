import type { RegisterOptions } from 'react-hook-form'
import type { UserFormValues } from '../types/User'

/** Shared regex patterns for user form fields */
export const userFormPatterns = {
  /** Any characters, length 2–100 (applied to trimmed value) */
  name: /^.{2,100}$/,
  /** Whole number 0–120 */
  age: /^(?:[0-9]|[1-9][0-9]|1[01][0-9]|120)$/,
  /** At least one non-whitespace character */
  requiredText: /\S+/,
  /** Alphanumeric pincode, 4–10 chars */
  pincode: /^[A-Za-z0-9]{4,10}$/,
} as const

export const userFormMessages = {
  name: {
    required: 'Name is required.',
    pattern: 'Name must be between 2 and 100 characters.',
  },
  age: {
    required: 'Age is required.',
    pattern: 'Age must be a whole number between 0 and 120.',
  },
  city: {
    required: 'City is required.',
    pattern: 'City is required.',
  },
  state: {
    required: 'State is required.',
    pattern: 'State is required.',
  },
  pincode: {
    required: 'Pincode is required.',
    pattern: 'Pincode must be 4-10 letters or numbers.',
  },
} as const

function matches(pattern: RegExp, value: string): boolean {
  return pattern.test(value)
}

export const userFormRules: Record<keyof UserFormValues, RegisterOptions<UserFormValues>> = {
  name: {
    required: userFormMessages.name.required,
    validate: (value) =>
      matches(userFormPatterns.name, String(value).trim()) || userFormMessages.name.pattern,
  },
  age: {
    required: userFormMessages.age.required,
    pattern: {
      value: userFormPatterns.age,
      message: userFormMessages.age.pattern,
    },
  },
  city: {
    required: userFormMessages.city.required,
    validate: (value) =>
      matches(userFormPatterns.requiredText, String(value).trim()) ||
      userFormMessages.city.pattern,
  },
  state: {
    required: userFormMessages.state.required,
    validate: (value) =>
      matches(userFormPatterns.requiredText, String(value).trim()) ||
      userFormMessages.state.pattern,
  },
  pincode: {
    required: userFormMessages.pincode.required,
    pattern: {
      value: userFormPatterns.pincode,
      message: userFormMessages.pincode.pattern,
    },
  },
}
