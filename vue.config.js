module.exports = {
  devServer: {
    disableHostCheck: true,
    proxy: {
      '/': {
        target: 'http://118.31.108.144:8080',
        changeOrigin: true,
        secure: false,
        ws: true,
        pathrewrite: {
          '^api/': '',
        },
      },
    },
  },
  transpileDependencies: ['vuetify'],
}
