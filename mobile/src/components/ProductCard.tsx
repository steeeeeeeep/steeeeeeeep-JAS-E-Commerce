import React from 'react';
import { View, Text, Image, TouchableOpacity, Dimensions } from 'react-native';
import { useProductStore } from '@/stores/productStore';

interface ProductCardProps {
  id: number;
  name: string;
  price: number;
  discountPrice?: number;
  image: string;
  rating: number;
  onPress: () => void;
}

const { width } = Dimensions.get('window');
const cardWidth = (width - 24) / 2;

export const ProductCard: React.FC<ProductCardProps> = ({
  id,
  name,
  price,
  discountPrice,
  image,
  rating,
  onPress,
}) => {
  const { isFavorite, addFavorite, removeFavorite } = useProductStore();
  const favorite = isFavorite(id);

  const toggleFavorite = () => {
    if (favorite) {
      removeFavorite(id);
    } else {
      addFavorite({ id, name, price, discountPrice, images: [image], rating, reviewCount: 0, categoryId: 0 });
    }
  };

  const discount = discountPrice ? Math.round(((price - discountPrice) / price) * 100) : 0;

  return (
    <TouchableOpacity
      onPress={onPress}
      className="bg-white rounded-lg overflow-hidden shadow-sm mb-4"
      style={{ width: cardWidth }}
    >
      <View className="relative">
        <Image source={{ uri: image }} className="w-full h-40 bg-gray-200" />
        {discount > 0 && (
          <View className="absolute top-2 right-2 bg-red-500 px-2 py-1 rounded">
            <Text className="text-white text-xs font-bold">-{discount}%</Text>
          </View>
        )}
        <TouchableOpacity
          onPress={toggleFavorite}
          className="absolute top-2 left-2 bg-white rounded-full p-2"
        >
          <Text className="text-lg">{favorite ? '❤️' : '🤍'}</Text>
        </TouchableOpacity>
      </View>
      <View className="p-3">
        <Text className="text-sm font-semibold text-gray-800 mb-1" numberOfLines={2}>
          {name}
        </Text>
        <View className="flex-row items-center justify-between">
          <View>
            {discountPrice ? (
              <>
                <Text className="text-xs text-gray-400 line-through">PKR {price.toFixed(0)}</Text>
                <Text className="text-lg font-bold text-gray-900">PKR {discountPrice.toFixed(0)}</Text>
              </>
            ) : (
              <Text className="text-lg font-bold text-gray-900">PKR {price.toFixed(0)}</Text>
            )}
          </View>
          <View className="flex-row items-center bg-yellow-400 px-2 py-1 rounded">
            <Text className="text-xs font-semibold">{rating.toFixed(1)}</Text>
            <Text className="text-xs ml-1">⭐</Text>
          </View>
        </View>
      </View>
    </TouchableOpacity>
  );
};
