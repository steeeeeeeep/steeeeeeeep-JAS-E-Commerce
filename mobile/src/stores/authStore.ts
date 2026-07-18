import { create } from 'zustand';
import { MMKVStorage } from '@/utils/storage';

interface AuthState {
  isAuthenticated: boolean;
  user: any | null;
  accessToken: string | null;
  refreshToken: string | null;
  isLoading: boolean;
  error: string | null;
  login: (email: string, password: string) => Promise<void>;
  register: (email: string, fullName: string, phoneNumber: string, password: string) => Promise<void>;
  logout: () => void;
  refreshAccessToken: () => Promise<void>;
  restoreToken: () => void;
}

export const useAuthStore = create<AuthState>((set) => ({
  isAuthenticated: false,
  user: null,
  accessToken: null,
  refreshToken: null,
  isLoading: false,
  error: null,

  login: async (email: string, password: string) => {
    set({ isLoading: true, error: null });
    try {
      // Call API
      const response = await fetch(`${process.env.EXPO_PUBLIC_API_URL}/auth/login`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, password }),
      });

      if (!response.ok) throw new Error('Login failed');

      const data = await response.json();
      MMKVStorage.setString('accessToken', data.data.accessToken);
      MMKVStorage.setString('refreshToken', data.data.refreshToken);
      set({
        isAuthenticated: true,
        accessToken: data.data.accessToken,
        refreshToken: data.data.refreshToken,
        user: data.data,
        isLoading: false,
      });
    } catch (error) {
      set({ error: error instanceof Error ? error.message : 'Login failed', isLoading: false });
      throw error;
    }
  },

  register: async (email: string, fullName: string, phoneNumber: string, password: string) => {
    set({ isLoading: true, error: null });
    try {
      const response = await fetch(`${process.env.EXPO_PUBLIC_API_URL}/auth/register`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, fullName, phoneNumber, password, confirmPassword: password }),
      });

      if (!response.ok) throw new Error('Registration failed');

      set({ isLoading: false });
    } catch (error) {
      set({ error: error instanceof Error ? error.message : 'Registration failed', isLoading: false });
      throw error;
    }
  },

  logout: () => {
    MMKVStorage.clearAll();
    set({
      isAuthenticated: false,
      user: null,
      accessToken: null,
      refreshToken: null,
    });
  },

  refreshAccessToken: async () => {
    try {
      const refreshToken = MMKVStorage.getString('refreshToken');
      if (!refreshToken) throw new Error('No refresh token');

      const response = await fetch(`${process.env.EXPO_PUBLIC_API_URL}/auth/refresh-token`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ refreshToken }),
      });

      if (!response.ok) throw new Error('Token refresh failed');

      const data = await response.json();
      MMKVStorage.setString('accessToken', data.data.accessToken);
      set({ accessToken: data.data.accessToken });
    } catch (error) {
      set({ error: error instanceof Error ? error.message : 'Token refresh failed' });
      throw error;
    }
  },

  restoreToken: () => {
    const accessToken = MMKVStorage.getString('accessToken');
    const refreshToken = MMKVStorage.getString('refreshToken');
    if (accessToken && refreshToken) {
      set({
        isAuthenticated: true,
        accessToken,
        refreshToken,
      });
    }
  },
}));
