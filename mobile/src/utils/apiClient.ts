import axios, { AxiosInstance, AxiosError } from 'axios';
import { MMKVStorage } from './storage';

const API_BASE_URL = process.env.EXPO_PUBLIC_API_URL || 'http://localhost:5000/api';

const apiClient: AxiosInstance = axios.create({
  baseURL: API_BASE_URL,
  timeout: 30000,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Request interceptor
apiClient.interceptors.request.use(
  (config) => {
    const token = MMKVStorage.getString('accessToken');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

// Response interceptor
apiClient.interceptors.response.use(
  (response) => response,
  async (error: AxiosError) => {
    const originalRequest = error.config as any;

    if (error.response?.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;

      try {
        const refreshToken = MMKVStorage.getString('refreshToken');
        if (!refreshToken) {
          throw new Error('No refresh token available');
        }

        const response = await axios.post(`${API_BASE_URL}/auth/refresh-token`, {
          refreshToken,
        });

        if (response.data.data) {
          MMKVStorage.setString('accessToken', response.data.data.accessToken);
          MMKVStorage.setString('refreshToken', response.data.data.refreshToken);

          originalRequest.headers.Authorization = `Bearer ${response.data.data.accessToken}`;
          return apiClient(originalRequest);
        }
      } catch (refreshError) {
        // Refresh token failed, clear storage and redirect to login
        MMKVStorage.clearAll();
        // Emit logout event or redirect to login
        return Promise.reject(refreshError);
      }
    }

    return Promise.reject(error);
  }
);

export default apiClient;
