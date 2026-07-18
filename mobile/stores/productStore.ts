import { create } from 'zustand';
import { apiClient } from '@/api/client';

interface Product {
  id: string;
  name: string;
  price: number;
  image: string;
}

interface ProductStore {
  products: Product[];
  loading: boolean;
  fetchProducts: () => Promise<void>;
}

export const useProductStore = create<ProductStore>((set) => ({
  products: [],
  loading: false,

  fetchProducts: async () => {
    set({ loading: true });
    try {
      const response = await apiClient.get('/products');
      set({ products: response.data, loading: false });
    } catch (error) {
      set({ loading: false });
    }
  }
}));