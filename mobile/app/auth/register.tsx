import React, { useState } from 'react';
import { View, Text, TextInput, TouchableOpacity, ScrollView } from 'react-native';
import { useRouter } from 'expo-router';
import { useAuthStore } from '@/stores/authStore';

export default function RegisterScreen() {
  const router = useRouter();
  const { register, loading } = useAuthStore();
  const [name, setName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const handleRegister = async () => {
    try {
      await register(name, email, password);
      router.replace('/auth/login');
    } catch (error) {
      console.error('Registration failed:', error);
    }
  };

  return (
    <ScrollView className="flex-1 bg-white">
      <View className="px-4 py-16 items-center">
        <Text className="text-4xl font-bold text-blue-600 mb-2">JAS</Text>
        <Text className="text-gray-600 mb-12">E-Commerce</Text>
      </View>

      <View className="px-4 mb-8">
        <Text className="text-2xl font-bold text-gray-900 mb-8">Create Account</Text>

        <View className="mb-4">
          <Text className="text-gray-700 font-semibold mb-2">Full Name</Text>
          <TextInput
            className="border border-gray-300 rounded-lg px-4 py-3"
            placeholder="Enter your full name"
            value={name}
            onChangeText={setName}
            editable={!loading}
          />
        </View>

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
          onPress={handleRegister}
          disabled={loading}
        >
          <Text className="text-white font-bold text-lg">Create Account</Text>
        </TouchableOpacity>

        <View className="flex-row justify-center">
          <Text className="text-gray-600">Already have an account? </Text>
          <TouchableOpacity onPress={() => router.push('/auth/login')}>
            <Text className="text-blue-600 font-bold">Login</Text>
          </TouchableOpacity>
        </View>
      </View>
    </ScrollView>
  );
}