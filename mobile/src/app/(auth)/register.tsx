import React from 'react';
import { View, ScrollView, Text, TouchableOpacity, KeyboardAvoidingView } from 'react-native';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import { useRouter } from 'expo-router';
import { Button } from '@/components/Button';
import { FormField } from '@/components/FormField';
import { useAuthStore } from '@/stores/authStore';

const registerSchema = z.object({
  email: z.string().email('Invalid email address'),
  fullName: z.string().min(2, 'Full name must be at least 2 characters'),
  phoneNumber: z.string().min(10, 'Invalid phone number'),
  password: z.string().min(8, 'Password must be at least 8 characters'),
  confirmPassword: z.string(),
}).refine((data) => data.password === data.confirmPassword, {
  message: "Passwords don't match",
  path: ['confirmPassword'],
});

type RegisterFormData = z.infer<typeof registerSchema>;

export default function RegisterScreen() {
  const router = useRouter();
  const { register: registerUser, isLoading } = useAuthStore();
  const { control, handleSubmit, formState: { errors } } = useForm<RegisterFormData>({
    resolver: zodResolver(registerSchema),
  });

  const onSubmit = async (data: RegisterFormData) => {
    try {
      await registerUser(data.email, data.fullName, data.phoneNumber, data.password);
      router.push('/login');
    } catch (error) {
      console.error('Registration error:', error);
    }
  };

  return (
    <KeyboardAvoidingView behavior="padding" className="flex-1">
      <ScrollView className="flex-1 bg-white p-4 pt-8">
        {/* Header */}
        <View className="mb-6">
          <Text className="text-4xl font-bold text-gray-900 mb-2">Create Account</Text>
          <Text className="text-gray-600">Join JAS Shopping Today</Text>
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
          name="fullName"
          control={control}
          label="Full Name"
          placeholder="John Doe"
          error={errors.fullName?.message}
        />
        <FormField
          name="phoneNumber"
          control={control}
          label="Phone Number"
          placeholder="+92 300 1234567"
          keyboardType="phone-pad"
          error={errors.phoneNumber?.message}
        />
        <FormField
          name="password"
          control={control}
          label="Password"
          placeholder="••••••••"
          secureTextEntry
          error={errors.password?.message}
        />
        <FormField
          name="confirmPassword"
          control={control}
          label="Confirm Password"
          placeholder="••••••••"
          secureTextEntry
          error={errors.confirmPassword?.message}
        />

        {/* Register Button */}
        <Button
          title="Create Account"
          onPress={handleSubmit(onSubmit)}
          loading={isLoading}
          variant="primary"
          size="lg"
        />

        {/* Sign In Link */}
        <View className="mt-6 flex-row justify-center mb-8">
          <Text className="text-gray-600">Already have an account? </Text>
          <TouchableOpacity onPress={() => router.push('/login')}>
            <Text className="text-blue-600 font-bold">Sign In</Text>
          </TouchableOpacity>
        </View>
      </ScrollView>
    </KeyboardAvoidingView>
  );
}
