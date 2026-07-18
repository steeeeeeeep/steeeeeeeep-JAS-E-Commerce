import React from 'react';
import { View, Text, TextInput, TouchableOpacity } from 'react-native';
import { Controller, FieldValues, Control } from 'react-hook-form';

interface FormFieldProps<T extends FieldValues> {
  name: keyof T;
  control: Control<T>;
  placeholder: string;
  label: string;
  secureTextEntry?: boolean;
  keyboardType?: 'default' | 'email-address' | 'numeric' | 'phone-pad';
  error?: string;
}

export const FormField = React.forwardRef<TextInput, FormFieldProps<any>>((
  { name, control, placeholder, label, secureTextEntry, keyboardType, error },
  ref
) => (
  <Controller
    control={control}
    name={name}
    render={({ field: { onChange, onBlur, value } }) => (
      <View className="mb-4">
        <Text className="text-gray-700 font-semibold mb-2">{label}</Text>
        <TextInput
          ref={ref}
          className={`border-2 rounded-lg px-4 py-3 text-gray-900 ${
            error ? 'border-red-500' : 'border-gray-300'
          }`}
          placeholder={placeholder}
          onBlur={onBlur}
          onChangeText={onChange}
          value={value}
          secureTextEntry={secureTextEntry}
          keyboardType={keyboardType}
        />
        {error && <Text className="text-red-500 text-sm mt-1">{error}</Text>}
      </View>
    )}
  />
));
