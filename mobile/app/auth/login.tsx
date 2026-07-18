import React, { useState } from 'react';
import { View, Text, TextInput, TouchableOpacity, ScrollView } from 'react-native';
import { useRouter } from 'expo-router';
import { useAuthStore } from '@/stores/authStore';

export default function LoginScreen() {
  const router = useRouter();
  const { login, loading } = useAuthStore();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const handleLogin = async () => {
    try {
      await login(email, password);
      router.replace('/(tabs)');
    } catch (error) {
      console.error('Login failed:', error);
    }
  };

  return (
    <ScrollView className="flex-1 bg-white">
      <View className="px-4 py-16 items-center">
        <Text className="text-4xl font-bold text-blue-600 mb-2">JAS</Text>
        <Text className="text-gray-600 mb-12">E-Commerce</Text>
      </View>

      <View className="px-4 mb-8">
        <Text className="text-2xl font-bold text-gray-900 mb-8">Login</Text>

        <View className="mb-4">
          <Text className="text-gray-700 font-semibold mb-2">Email</Text>
          <TextInput
            className="border border-gray-300 rounded-lg px-4 py-3"
            placeholder="Enter your email"
            value={email}
            onChangeText={setEmail}
            keyboardType="email-address"
            editable={!loading}
          />
        </View>

        <View className="mb-8">
          <Text className="text-gray-700 font-semibold mb-2">Password</Text>
          <TextInput
            className="border border-gray-300 rounded-lg px-4 py-3"
            placeholder="Enter your password"
            value={password}
            onChangeText={setPassword}
            secureTextEntry
            editable={!loading}
          />
        </View>

        <TouchableOpacity
          className="bg-blue-600 py-4 rounded-lg items-center mb-4"
          onPress={handleLogin}
          disabled={loading}
        >
          <Text className="text-white font-bold text-lg">Login</Text>
        </TouchableOpacity>

        <View className="flex-row justify-center">
          <Text className="text-gray-600">Don't have an account? </Text>
          <TouchableOpacity onPress={() => router.push('/auth/register')}>
            <Text className="text-blue-600 font-bold">Register</Text>
          </TouchableOpacity>
        </View>
      </View>
    </ScrollView>
  );
}