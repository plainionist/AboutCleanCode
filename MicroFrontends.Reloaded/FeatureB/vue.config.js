const ModuleFederationPlugin = require('webpack/lib/container/ModuleFederationPlugin')

module.exports = {
  devServer: {
    port: 6060
  },
  publicPath: 'http://localhost:6060/',
  configureWebpack: {
    optimization: {
      splitChunks: false
    },
    plugins: [
      new ModuleFederationPlugin({
        name: 'featureB',
        filename: 'remoteEntry.js',
        exposes: {
          './App': './src/App.vue'
        },
        shared: {
          vue: {
            singleton: true,
            eager: true,
            requiredVersion: '^3.0.0'
          }
        }
      })
    ]
  }
}
