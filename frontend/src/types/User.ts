export interface User {
  id?: number
  name: string
  age: number
  city: string
  state: string
  pincode: string
}

export type UserFormValues = {
  name: string
  age: string
  city: string
  state: string
  pincode: string
}
