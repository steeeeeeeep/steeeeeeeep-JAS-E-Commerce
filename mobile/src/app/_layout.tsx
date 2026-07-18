import React from 'react';
import { Stack } from 'expo-router';
import { useAuthStore } from '@/stores/authStore';
import { ActivityIndicator, View } from 'react-native';

export default function RootLayout() {
  const { restoreToken, isAuthenticated } = useAuthStore();
  const [isLoading, setIsLoading] = React.useState(true);

  React.useEffect(() => {
    const bootstrapAsync = async () => {
      try {
        restoreToken();
      } catch (e) {
        console.error('Failed to restore token:', e);
      } finally {
        setIsLoading(false);
      }
    };

    bootstrapAsync();
  }, []);

  if (isLoading) {
    return (
      <View className="flex-1 justify-center items-center bg-white">
        <ActivityIndicator size="large" color="#0000ff" />
      </View>
    );
  }

  return (
    <Stack
      screenOptions={{
        headerShown: false,
      }}
    >
      {isAuthenticated ? (
        <Stack.Screen name="(app)" />
      ) : (
        <Stack.Screen name="(auth)" />
      )}
    </Stack>
  );
}
