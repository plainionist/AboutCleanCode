import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import federation from '@originjs/vite-plugin-federation'

export default defineConfig({
  plugins: [
    vue(),
    federation({
      name: 'app',
      remotes: {
        featureA: 'http://localhost:7070/assets/remoteEntry.js',
        featureB: 'http://localhost:6060/assets/remoteEntry.js'
      }
    })
  ]
})
