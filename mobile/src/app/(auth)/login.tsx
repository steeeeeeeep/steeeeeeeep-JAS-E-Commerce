import React from 'react';
import { View, ScrollView, Text, TouchableOpacity, KeyboardAvoidingView } from 'react-native';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import { useRouter } from 'expo-router';
import { Button } from '@/components/Button';
import { FormField } from '@/components/FormField';
import { useAuthStore } from '@/stores/authStore';

const loginSchema = z.object({
  email: z.string().email('Invalid email address'),
  password: z.string().min(6, 'Password must be at least 6 characters'),
});

type LoginFormData = z.infer<typeof loginSchema>;

export default function LoginScreen() {
  const router = useRouter();
  const { login, isLoading } = useAuthStore();
  const { control, handleSubmit, formState: { errors } } = useForm<LoginFormData>({
    resolver: zodResolver(loginSchema),
  });

  const onSubmit = async (data: LoginFormData) => {
    try {
      await login(data.email, data.password);
      router.replace('/(app)');
    } catch (error) {
      console.error('Login error:', error);
    }
  };

  return (
    <KeyboardAvoidingView behavior="padding" className="flex-1">
      <ScrollView className="flex-1 bg-white p-4 pt-12">
        {/* Header */}
        <View className="mb-8">
          <Text className="text-4xl font-bold text-gray-900 mb-2">Welcome Back</Text>
          <Text className="text-gray-600">Login to your account</Text>
        </View>

        {/* Form */}
        <FormField
          name="email"
          control={control}
          label="Email"
          placeholder="your@email.com"
          keyboardType="email-address"
          error={errors.email?.message}
        />
        <FormField
          name="password"
          control={control}
          label="Password"
          placeholder="••••••••"
          secureTextEntry
          error={errors.password?.message}
        />

        {/* Forgot Password */}
        <TouchableOpacity className="mb-6">
          <Text className="text-blue-600 font-semibold">Forgot Password?</Text>
        </TouchableOpacity>

        {/* Login Button */}
        <Button
          title="Login"
          onPress={handleSubmit(onSubmit)}
          loading={isLoading}
          variant="primary"
          size="lg"
        />

        {/* Sign Up Link */}
        <View className="mt-6 flex-row justify-center">
          <Text className="text-gray-600">Don't have an account? </Text>
          <TouchableOpacity onPress={() => router.push('/register')}>
            <Text className="text-blue-600 font-bold">Sign Up</Text>
          </TouchableOpacity>
        </View>
      </ScrollView>
    </KeyboardAvoidingView>
  );
}
