﻿const path = require('path');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const { ProvidePlugin } = require('webpack');

module.exports = {
  entry: {
    site: './js/site.js',
    validation: './js/validation.js',
    orders: './js/orders.index.js',
    clients: './js/clients.index.js',
    products: './js/products.index.js'
  },
  output: {
    filename: '[name].entry.js',
    path: path.resolve(__dirname, '..', 'wwwroot', 'dist'),
  },
  devtool: 'source-map',
  mode: 'development',
  module: {
    rules: [
      {
        test: /\.css$/i,
        use: [
          MiniCssExtractPlugin.loader,
          { loader: 'css-loader', options: { url: false, sourceMap: true } },
        ],
      },
    ],
  },
  plugins: [
    new MiniCssExtractPlugin({
      filename: '[name].css',
    }),
    new ProvidePlugin({
      $: 'jquery',
      jQuery: 'jquery',
    }),
  ],
};