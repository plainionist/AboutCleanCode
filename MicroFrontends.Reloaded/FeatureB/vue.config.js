const ModuleFederationPlugin = require('webpack/lib/container/ModuleFederationPlugin')

module.exports = {
  devServer: {
    port: 6060
  },
  configureWebpack: {
    plugins: [
      new ModuleFederationPlugin({
        name: 'featureB',
        filename: 'remoteEntry.js',
        exposes: {
          './App': './src/App.vue'
        }
      })
    ]
  }
}
