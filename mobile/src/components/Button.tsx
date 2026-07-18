import React from 'react';
import { View, Text, TouchableOpacity, ActivityIndicator } from 'react-native';

interface ButtonProps {
  title: string;
  onPress: () => void;
  loading?: boolean;
  disabled?: boolean;
  variant?: 'primary' | 'secondary' | 'danger';
  size?: 'sm' | 'md' | 'lg';
}

export const Button: React.FC<ButtonProps> = ({
  title,
  onPress,
  loading = false,
  disabled = false,
  variant = 'primary',
  size = 'md',
}) => {
  const getVariantStyles = () => {
    switch (variant) {
      case 'secondary':
        return 'bg-gray-200';
      case 'danger':
        return 'bg-red-500';
      default:
        return 'bg-blue-600';
    }
  };

  const getSizeStyles = () => {
    switch (size) {
      case 'sm':
        return 'px-3 py-2';
      case 'lg':
        return 'px-6 py-4';
      default:
        return 'px-4 py-3';
    }
  };

  return (
    <TouchableOpacity
      onPress={onPress}
      disabled={disabled || loading}
      className={`${getVariantStyles()} ${getSizeStyles()} rounded-lg flex-row items-center justify-center ${disabled ? 'opacity-50' : ''}`}
    >
      {loading ? (
        <ActivityIndicator color="white" />
      ) : (
        <Text className="text-white font-bold text-center">{title}</Text>
      )}
    </TouchableOpacity>
  );
};
