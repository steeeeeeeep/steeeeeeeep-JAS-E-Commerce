import React from 'react';
import { View, Text, ScrollView } from 'react-native';

export default function HomeScreen() {
  return (
    <ScrollView className="flex-1 bg-white">
      <View className="bg-blue-600 px-4 py-8 items-center">
        <Text className="text-white text-3xl font-bold">Welcome to JAS</Text>
        <Text className="text-blue-100 text-center mt-2">Discover amazing products</Text>
      </View>
    </ScrollView>
  );
}