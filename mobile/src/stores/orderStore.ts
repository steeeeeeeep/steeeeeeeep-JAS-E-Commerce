import { create } from 'zustand';

interface Order {
  id: number;
  orderNumber: string;
  status: number;
  totalAmount: number;
  createdAt: string;
}

interface OrderState {
  orders: Order[];
  currentOrder: Order | null;
  isLoading: boolean;
  error: string | null;
  fetchOrders: (userId: number) => Promise<void>;
  createOrder: (orderData: any) => Promise<void>;
  getOrderDetails: (orderId: number) => Promise<void>;
}

export const useOrderStore = create<OrderState>((set) => ({
  orders: [],
  currentOrder: null,
  isLoading: false,
  error: null,

  fetchOrders: async (userId: number) => {
    set({ isLoading: true, error: null });
    try {
      const response = await fetch(`${process.env.EXPO_PUBLIC_API_URL}/order/user-orders`);
      if (!response.ok) throw new Error('Failed to fetch orders');
      const data = await response.json();
      set({ orders: data.data?.items || [], isLoading: false });
    } catch (error) {
      set({ error: error instanceof Error ? error.message : 'Failed to fetch orders', isLoading: false });
    }
  },

  createOrder: async (orderData: any) => {
    set({ isLoading: true, error: null });
    try {
      const response = await fetch(`${process.env.EXPO_PUBLIC_API_URL}/order`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(orderData),
      });
      if (!response.ok) throw new Error('Failed to create order');
      const data = await response.json();
      set({ currentOrder: data.data, isLoading: false });
    } catch (error) {
      set({ error: error instanceof Error ? error.message : 'Failed to create order', isLoading: false });
    }
  },

  getOrderDetails: async (orderId: number) => {
    set({ isLoading: true, error: null });
    try {
      const response = await fetch(`${process.env.EXPO_PUBLIC_API_URL}/order/${orderId}`);
      if (!response.ok) throw new Error('Failed to fetch order details');
      const data = await response.json();
      set({ currentOrder: data.data, isLoading: false });
    } catch (error) {
      set({ error: error instanceof Error ? error.message : 'Failed to fetch order', isLoading: false });
    }
  },
}));
