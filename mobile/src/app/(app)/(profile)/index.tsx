import React from 'react';
import { View, Text, ScrollView, TouchableOpacity } from 'react-native';
import { Button } from '@/components/Button';
import { useAuthStore } from '@/stores/authStore';
import { useRouter } from 'expo-router';

export default function ProfileScreen() {
  const { user, logout } = useAuthStore();
  const router = useRouter();

  const handleLogout = () => {
    logout();
    router.replace('/');
  };

  return (
    <ScrollView className="flex-1 bg-white p-4">
      {/* Profile Header */}
      <View className="items-center mb-6 mt-4">
        <View className="w-20 h-20 bg-blue-600 rounded-full justify-center items-center mb-3">
          <Text className="text-white text-3xl font-bold">
            {user?.fullName?.charAt(0).toUpperCase()}
          </Text>
        </View>
        <Text className="text-2xl font-bold text-gray-900">{user?.fullName}</Text>
        <Text className="text-gray-600 mt-1">{user?.email}</Text>
      </View>

      {/* Profile Options */}
      <View className="space-y-3 mb-6">
        <TouchableOpacity className="bg-gray-50 p-4 rounded-lg flex-row justify-between items-center">
          <Text className="font-semibold text-gray-900">📋 Edit Profile</Text>
          <Text className="text-gray-400">›</Text>
        </TouchableOpacity>
        <TouchableOpacity className="bg-gray-50 p-4 rounded-lg flex-row justify-between items-center">
          <Text className="font-semibold text-gray-900">📍 Saved Addresses</Text>
          <Text className="text-gray-400">›</Text>
        </TouchableOpacity>
        <TouchableOpacity className="bg-gray-50 p-4 rounded-lg flex-row justify-between items-center">
          <Text className="font-semibold text-gray-900">❤️ Wishlist</Text>
          <Text className="text-gray-400">›</Text>
        </TouchableOpacity>
        <TouchableOpacity className="bg-gray-50 p-4 rounded-lg flex-row justify-between items-center">
          <Text className="font-semibold text-gray-900">🔔 Notifications</Text>
          <Text className="text-gray-400">›</Text>
        </TouchableOpacity>
        <TouchableOpacity className="bg-gray-50 p-4 rounded-lg flex-row justify-between items-center">
          <Text className="font-semibold text-gray-900">🔐 Change Password</Text>
          <Text className="text-gray-400">›</Text>
        </TouchableOpacity>
      </View>

      {/* Logout */}
      <View className="mt-auto pt-4">
        <Button
          title="Logout"
          onPress={handleLogout}
          variant="danger"
          size="lg"
        />
      </View>
    </ScrollView>
  );
}
