import axios from 'axios'
import type { User } from '../types/User'
import { clearToken, getToken } from './authStorage'

export type LoginRequest = {
  username: string
  password: string
}

export type TokenResponse = {
  accessToken: string
  tokenType: string
  expiresInSeconds: number
}

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
})

api.interceptors.request.use((config) => {
  const token = getToken()
  if (token) {
    config.headers.Authorization = `Bearer ${token}`
  }
  return config
})

api.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      clearToken()
      if (window.location.pathname !== '/login') {
        window.location.assign('/login')
      }
    }
    return Promise.reject(error)
  },
)

export const login = async (credentials: LoginRequest): Promise<TokenResponse> => {
  const { data } = await api.post<TokenResponse>('/auth/login', credentials)
  return data
}

export const getAllUsers = async (): Promise<User[]> => {
  const { data } = await api.get<User[]>('/user-directory/get-all-users')
  return data
}

export const addUser = async (user: Omit<User, 'id'>): Promise<User> => {
  const { data } = await api.post<User>('/user-directory/add-user', user)
  return data
}

export default api
