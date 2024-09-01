import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import federation from '@originjs/vite-plugin-federation'

export default defineConfig({
  plugins: [
    vue(),
    federation({
      name: 'featureB',
      filename: 'remoteEntry.js',
      exposes: {
        './App': './src/App.vue'
      },
      shared: ['vue']
    })
  ],
  build: {
    target: 'esnext', // to support shared modules
  },
  server: {
    port: 6060
  }
})
