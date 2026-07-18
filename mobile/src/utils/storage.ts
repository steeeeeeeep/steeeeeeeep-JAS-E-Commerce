import { MMKV } from 'react-native-mmkv';

const storage = new MMKV();

export const MMKVStorage = {
  setString: (key: string, value: string) => storage.setString(key, value),
  getString: (key: string) => storage.getString(key),
  setNumber: (key: string, value: number) => storage.setNumber(key, value),
  getNumber: (key: string) => storage.getNumber(key),
  setBoolean: (key: string, value: boolean) => storage.setBoolean(key, value),
  getBoolean: (key: string) => storage.getBoolean(key),
  delete: (key: string) => storage.delete(key),
  clearAll: () => storage.clearAll(),
};
