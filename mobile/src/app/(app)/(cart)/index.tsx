import React from 'react';
import { View, Text, ScrollView } from 'react-native';
import { Button } from '@/components/Button';
import { CartItemList } from '@/components/CartItemList';
import { useCartStore } from '@/stores/cartStore';

export default function CartScreen() {
  const { items, subtotal, clearCart, removeItem, updateQuantity } = useCartStore();

  const shippingFee = items.length > 0 ? 500 : 0;
  const total = subtotal + shippingFee;

  return (
    <View className="flex-1 bg-white">
      <CartItemList
        items={items}
        isLoading={false}
        onRemove={removeItem}
        onUpdateQuantity={updateQuantity}
      />

      {items.length > 0 && (
        <View className="border-t border-gray-200 p-4">
          <View className="flex-row justify-between mb-2">
            <Text className="text-gray-600">Subtotal</Text>
            <Text className="font-semibold">PKR {subtotal.toFixed(0)}</Text>
          </View>
          <View className="flex-row justify-between mb-4 pb-4 border-b border-gray-200">
            <Text className="text-gray-600">Shipping</Text>
            <Text className="font-semibold">PKR {shippingFee}</Text>
          </View>
          <View className="flex-row justify-between mb-4">
            <Text className="text-lg font-bold">Total</Text>
            <Text className="text-lg font-bold text-blue-600">PKR {total.toFixed(0)}</Text>
          </View>
          <Button title="Proceed to Checkout" onPress={() => {}} variant="primary" size="lg" />
          <Button
            title="Clear Cart"
            onPress={clearCart}
            variant="secondary"
            size="md"
          />
        </View>
      )}
    </View>
  );
}
