import React from 'react';
import { TouchableOpacity, Text, ActivityIndicator } from 'react-native';

interface ButtonProps {
  onPress: () => void;
  title: string;
  loading?: boolean;
  disabled?: boolean;
  variant?: 'primary' | 'secondary';
}

export const Button: React.FC<ButtonProps> = ({
  onPress,
  title,
  loading = false,
  disabled = false,
  variant = 'primary'
}) => {
  const bgColor = variant === 'primary' ? 'bg-blue-600' : 'bg-gray-200';
  const textColor = variant === 'primary' ? 'text-white' : 'text-gray-900';

  return (
    <TouchableOpacity
      className={`${bgColor} py-3 rounded-lg items-center`}
      onPress={onPress}
      disabled={disabled || loading}
    >
      {loading ? (
        <ActivityIndicator color={variant === 'primary' ? 'white' : 'black'} />
      ) : (
        <Text className={`font-bold text-lg ${textColor}`}>{title}</Text>
      )}
    </TouchableOpacity>
  );
};