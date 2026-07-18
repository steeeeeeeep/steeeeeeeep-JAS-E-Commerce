import { create } from 'zustand';

interface Product {
  id: number;
  name: string;
  description: string;
  price: number;
  discountPrice?: number;
  images: string[];
  rating: number;
  reviewCount: number;
  categoryId: number;
  variants?: any[];
}

interface ProductState {
  products: Product[];
  favorites: Product[];
  isLoading: boolean;
  error: string | null;
  fetchProducts: () => Promise<void>;
  addFavorite: (product: Product) => void;
  removeFavorite: (productId: number) => void;
  isFavorite: (productId: number) => boolean;
}

export const useProductStore = create<ProductState>((set, get) => ({
  products: [],
  favorites: [],
  isLoading: false,
  error: null,

  fetchProducts: async () => {
    set({ isLoading: true, error: null });
    try {
      const response = await fetch(`${process.env.EXPO_PUBLIC_API_URL}/products`);
      if (!response.ok) throw new Error('Failed to fetch products');
      const data = await response.json();
      set({ products: data.data || [], isLoading: false });
    } catch (error) {
      set({ error: error instanceof Error ? error.message : 'Failed to fetch products', isLoading: false });
    }
  },

  addFavorite: (product: Product) => {
    set((state) => ({
      favorites: [...state.favorites, product],
    }));
  },

  removeFavorite: (productId: number) => {
    set((state) => ({
      favorites: state.favorites.filter((p) => p.id !== productId),
    }));
  },

  isFavorite: (productId: number) => {
    return get().favorites.some((p) => p.id === productId);
  },
}));
