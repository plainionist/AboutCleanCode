const ModuleFederationPlugin = require('webpack/lib/container/ModuleFederationPlugin')

module.exports = {
  configureWebpack: {
    plugins: [
      new ModuleFederationPlugin({
        name: 'app',
        remotes: {
          featureA: 'featureA@http://localhost:7070/remoteEntry.js',
          featureB: 'featureB@http://localhost:6060/remoteEntry.js'
        }
      })
    ]
  }
}
