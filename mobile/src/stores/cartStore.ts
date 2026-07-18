import { create } from 'zustand';

interface CartItem {
  id: number;
  productId: number;
  productName: string;
  quantity: number;
  unitPrice: number;
  totalPrice: number;
}

interface CartState {
  items: CartItem[];
  totalItems: number;
  subtotal: number;
  isLoading: boolean;
  error: string | null;
  addItem: (item: CartItem) => void;
  removeItem: (cartItemId: number) => void;
  updateQuantity: (cartItemId: number, quantity: number) => void;
  clearCart: () => void;
  calculateTotals: () => void;
}

export const useCartStore = create<CartState>((set, get) => ({
  items: [],
  totalItems: 0,
  subtotal: 0,
  isLoading: false,
  error: null,

  addItem: (item: CartItem) => {
    set((state) => {
      const existingItem = state.items.find((i) => i.id === item.id);
      if (existingItem) {
        return {
          items: state.items.map((i) =>
            i.id === item.id ? { ...i, quantity: i.quantity + item.quantity } : i
          ),
        };
      }
      return { items: [...state.items, item] };
    });
    get().calculateTotals();
  },

  removeItem: (cartItemId: number) => {
    set((state) => ({
      items: state.items.filter((i) => i.id !== cartItemId),
    }));
    get().calculateTotals();
  },

  updateQuantity: (cartItemId: number, quantity: number) => {
    set((state) => ({
      items: state.items.map((i) =>
        i.id === cartItemId ? { ...i, quantity } : i
      ),
    }));
    get().calculateTotals();
  },

  clearCart: () => {
    set({ items: [], totalItems: 0, subtotal: 0 });
  },

  calculateTotals: () => {
    const items = get().items;
    const totalItems = items.reduce((sum, item) => sum + item.quantity, 0);
    const subtotal = items.reduce((sum, item) => sum + item.totalPrice, 0);
    set({ totalItems, subtotal });
  },
}));
