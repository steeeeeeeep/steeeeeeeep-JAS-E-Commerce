import { create } from 'zustand';
import { apiClient } from '@/api/client';

interface Order {
  id: string;
  total: number;
  status: string;
}

interface OrderStore {
  orders: Order[];
  loading: boolean;
  fetchOrders: () => Promise<void>;
}

export const useOrderStore = create<OrderStore>((set) => ({
  orders: [],
  loading: false,

  fetchOrders: async () => {
    set({ loading: true });
    try {
      const response = await apiClient.get('/orders');
      set({ orders: response.data, loading: false });
    } catch (error) {
      set({ loading: false });
    }
  }
}));