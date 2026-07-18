import React from 'react';
import { View, Text, ScrollView, FlatList } from 'react-native';
import { ProductCard } from '@/components/ProductCard';
import { useProductStore } from '@/stores/productStore';
import { useEffect } from 'react';

export default function ProductsScreen() {
  const { products, fetchProducts, isLoading } = useProductStore();

  useEffect(() => {
    fetchProducts();
  }, []);

  return (
    <View className="flex-1 bg-gray-50 p-2">
      <FlatList
        data={products}
        numColumns={2}
        columnWrapperStyle={{ justifyContent: 'space-between' }}
        renderItem={({ item }) => (
          <ProductCard
            id={item.id}
            name={item.name}
            price={item.price}
            discountPrice={item.discountPrice}
            image={item.images[0]}
            rating={item.rating}
            onPress={() => {}}
          />
        )}
        keyExtractor={(item) => item.id.toString()}
        ListEmptyComponent={<Text>No products available</Text>}
      />
    </View>
  );
}
