import React from 'react';
import { View, Text, FlatList, ActivityIndicator } from 'react-native';
import { CartItem } from '@/stores/cartStore';

interface CartItemListProps {
  items: CartItem[];
  isLoading: boolean;
  onRemove: (cartItemId: number) => void;
  onUpdateQuantity: (cartItemId: number, quantity: number) => void;
}

export const CartItemList: React.FC<CartItemListProps> = ({
  items,
  isLoading,
  onRemove,
  onUpdateQuantity,
}) => {
  if (isLoading) {
    return <ActivityIndicator size="large" color="#0000ff" className="mt-10" />;
  }

  if (items.length === 0) {
    return (
      <View className="flex-1 justify-center items-center">
        <Text className="text-gray-500 text-lg">Your cart is empty</Text>
      </View>
    );
  }

  return (
    <FlatList
      data={items}
      keyExtractor={(item) => item.id.toString()}
      renderItem={({ item }) => (
        <View className="bg-white border-b border-gray-200 p-4 flex-row justify-between items-center">
          <View className="flex-1">
            <Text className="font-semibold text-gray-900" numberOfLines={1}>
              {item.productName}
            </Text>
            <Text className="text-gray-600 text-sm">PKR {item.unitPrice.toFixed(0)}</Text>
            <View className="flex-row items-center mt-2">
              <Text
                onPress={() => onUpdateQuantity(item.id, Math.max(1, item.quantity - 1))}
                className="text-lg px-2 bg-gray-200 rounded"
              >
                −
              </Text>
              <Text className="mx-3 font-semibold">{item.quantity}</Text>
              <Text
                onPress={() => onUpdateQuantity(item.id, item.quantity + 1)}
                className="text-lg px-2 bg-gray-200 rounded"
              >
                +
              </Text>
            </View>
          </View>
          <View className="items-end">
            <Text className="font-bold text-gray-900 mb-2">PKR {item.totalPrice.toFixed(0)}</Text>
            <Text
              onPress={() => onRemove(item.id)}
              className="text-red-500 font-semibold"
            >
              Remove
            </Text>
          </View>
        </View>
      )}
    />
  );
};
