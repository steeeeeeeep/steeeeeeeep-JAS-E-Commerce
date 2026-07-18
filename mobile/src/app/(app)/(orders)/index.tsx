import React from 'react';
import { View, Text, ScrollView, FlatList } from 'react-native';
import { useOrderStore } from '@/stores/orderStore';
import { useAuthStore } from '@/stores/authStore';
import { useEffect } from 'react';

const ORDER_STATUS = {
  0: 'Pending',
  1: 'Processing',
  2: 'Packed',
  3: 'Shipped',
  4: 'Delivered',
  5: 'Cancelled',
};

const getStatusColor = (status: number) => {
  switch (status) {
    case 0:
      return 'bg-yellow-100 text-yellow-800';
    case 1:
      return 'bg-blue-100 text-blue-800';
    case 2:
      return 'bg-purple-100 text-purple-800';
    case 3:
      return 'bg-indigo-100 text-indigo-800';
    case 4:
      return 'bg-green-100 text-green-800';
    case 5:
      return 'bg-red-100 text-red-800';
    default:
      return 'bg-gray-100 text-gray-800';
  }
};

export default function OrdersScreen() {
  const { orders, fetchOrders, isLoading } = useOrderStore();
  const { user } = useAuthStore();

  useEffect(() => {
    if (user?.id) {
      fetchOrders(user.id);
    }
  }, [user?.id]);

  return (
    <View className="flex-1 bg-gray-50">
      <FlatList
        data={orders}
        renderItem={({ item }) => (
          <View className="bg-white m-3 p-4 rounded-lg border border-gray-200">
            <View className="flex-row justify-between items-center mb-3">
              <Text className="font-bold text-gray-900">{item.orderNumber}</Text>
              <View className={`px-3 py-1 rounded ${getStatusColor(item.status)}`}>
                <Text className="text-xs font-semibold">
                  {ORDER_STATUS[item.status as keyof typeof ORDER_STATUS]}
                </Text>
              </View>
            </View>
            <Text className="text-gray-600 text-sm mb-2">
              Order Date: {new Date(item.createdAt).toLocaleDateString()}
            </Text>
            <Text className="text-lg font-bold text-blue-600">PKR {item.totalAmount.toFixed(0)}</Text>
          </View>
        )}
        keyExtractor={(item) => item.id.toString()}
        ListEmptyComponent={
          <View className="flex-1 justify-center items-center">
            <Text className="text-gray-500">No orders yet</Text>
          </View>
        }
      />
    </View>
  );
}
