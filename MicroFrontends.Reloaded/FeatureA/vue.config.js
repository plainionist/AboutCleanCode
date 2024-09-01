const ModuleFederationPlugin = require('webpack/lib/container/ModuleFederationPlugin')

module.exports = {
  devServer: {
    port: 7070
  },
  publicPath: 'http://localhost:7070',
  configureWebpack: {
    optimization: {
      splitChunks: false
    },
    plugins: [
      new ModuleFederationPlugin({
        name: 'featureA',
        filename: 'remoteEntry.js',
        exposes: {
          './App': './src/App.vue'
        }
      })
    ]
  }
}
