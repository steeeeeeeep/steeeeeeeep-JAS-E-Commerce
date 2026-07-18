import React from 'react';
import { View, Text, ScrollView, Image } from 'react-native';
import { Button } from '@/components/Button';
import { useProductStore } from '@/stores/productStore';
import { useEffect } from 'react';

export default function HomeScreen() {
  const { products, fetchProducts, isLoading } = useProductStore();

  useEffect(() => {
    fetchProducts();
  }, []);

  // Featured products (top 5)
  const featuredProducts = products.slice(0, 5);

  return (
    <ScrollView className="flex-1 bg-white">
      {/* Banner */}
      <View className="bg-blue-600 h-48 justify-center items-center">
        <Text className="text-white text-3xl font-bold">Welcome to JAS</Text>
        <Text className="text-white text-lg mt-2">Premium Shopping Experience</Text>
      </View>

      {/* Flash Sale */}
      <View className="p-4">
        <Text className="text-xl font-bold text-gray-900 mb-4">⚡ Flash Sale</Text>
        <ScrollView horizontal showsHorizontalScrollIndicator={false}>
          {featuredProducts.map((product) => (
            <View key={product.id} className="bg-gray-100 rounded-lg p-3 mr-3">
              <Image source={{ uri: product.images[0] }} className="w-24 h-24 rounded" />
              <Text className="text-sm font-semibold mt-2" numberOfLines={1}>
                {product.name}
              </Text>
              <Text className="text-red-600 font-bold">-30%</Text>
            </View>
          ))}
        </ScrollView>
      </View>

      {/* Recommended */}
      <View className="p-4">
        <Text className="text-xl font-bold text-gray-900 mb-4">Recommended for You</Text>
        {isLoading ? (
          <Text>Loading...</Text>
        ) : (
          featuredProducts.map((product) => (
            <View key={product.id} className="bg-gray-50 p-3 mb-3 rounded-lg flex-row">
              <Image source={{ uri: product.images[0] }} className="w-20 h-20 rounded" />
              <View className="flex-1 ml-3 justify-center">
                <Text className="font-semibold text-gray-900">{product.name}</Text>
                <Text className="text-green-600 font-bold">PKR {product.price}</Text>
              </View>
            </View>
          ))
        )}
      </View>

      <View className="p-4">
        <Button title="Shop Now" onPress={() => {}} variant="primary" size="lg" />
      </View>
    </ScrollView>
  );
}
