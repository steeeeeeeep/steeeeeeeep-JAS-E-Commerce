import React from 'react';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import { Tabs } from 'expo-router';
import { Text, View } from 'react-native';

const Tab = createBottomTabNavigator();

export default function AppLayout() {
  return (
    <Tabs
      screenOptions={{
        headerShown: true,
        tabBarStyle: {
          backgroundColor: '#fff',
          borderTopColor: '#e5e7eb',
          borderTopWidth: 1,
        },
        tabBarLabelStyle: {
          fontSize: 12,
          fontWeight: '600',
        },
        tabBarActiveTintColor: '#2563eb',
        tabBarInactiveTintColor: '#9ca3af',
      }}
    >
      <Tabs.Screen
        name="(home)"
        options={{
          title: 'Home',
          tabBarLabel: 'Home',
        }}
      />
      <Tabs.Screen
        name="(products)"
        options={{
          title: 'Products',
          tabBarLabel: 'Products',
        }}
      />
      <Tabs.Screen
        name="(cart)"
        options={{
          title: 'Cart',
          tabBarLabel: 'Cart',
        }}
      />
      <Tabs.Screen
        name="(orders)"
        options={{
          title: 'Orders',
          tabBarLabel: 'Orders',
        }}
      />
      <Tabs.Screen
        name="(profile)"
        options={{
          title: 'Profile',
          tabBarLabel: 'Profile',
        }}
      />
    </Tabs>
  );
}
