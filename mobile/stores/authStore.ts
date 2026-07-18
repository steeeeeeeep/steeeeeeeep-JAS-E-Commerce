import { create } from 'zustand';
import * as SecureStore from 'expo-secure-store';
import { apiClient } from '@/api/client';

interface User {
  id: string;
  name: string;
  email: string;
}

interface AuthStore {
  user: User | null;
  token: string | null;
  isAuthenticated: boolean;
  loading: boolean;
  login: (email: string, password: string) => Promise<void>;
  register: (name: string, email: string, password: string) => Promise<void>;
  logout: () => Promise<void>;
  initializeAuth: () => Promise<void>;
  fetchUserProfile: () => Promise<void>;
}

export const useAuthStore = create<AuthStore>((set) => ({
  user: null,
  token: null,
  isAuthenticated: false,
  loading: false,

  login: async (email: string, password: string) => {
    set({ loading: true });
    try {
      const response = await apiClient.post('/auth/login', { email, password });
      const { token, user } = response.data;
      await SecureStore.setItemAsync('auth_token', token);
      set({ token, user, isAuthenticated: true, loading: false });
    } catch (error) {
      set({ loading: false });
      throw error;
    }
  },

  register: async (name: string, email: string, password: string) => {
    set({ loading: true });
    try {
      await apiClient.post('/auth/register', { name, email, password });
      set({ loading: false });
    } catch (error) {
      set({ loading: false });
      throw error;
    }
  },

  logout: async () => {
    try {
      await SecureStore.deleteItemAsync('auth_token');
      set({ user: null, token: null, isAuthenticated: false });
    } catch (error) {
      console.error('Logout error:', error);
    }
  },

  initializeAuth: async () => {
    try {
      const token = await SecureStore.getItemAsync('auth_token');
      if (token) {
        set({ token, isAuthenticated: true });
        apiClient.defaults.headers.common['Authorization'] = `Bearer ${token}`;
      }
    } catch (error) {
      console.error('Auth initialization error:', error);
    }
  },

  fetchUserProfile: async () => {
    set({ loading: true });
    try {
      const response = await apiClient.get('/auth/profile');
      set({ user: response.data, loading: false });
    } catch (error) {
      set({ loading: false });
    }
  }
}));