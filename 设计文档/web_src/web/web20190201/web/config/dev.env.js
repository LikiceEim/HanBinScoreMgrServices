'use strict'
const merge = require('webpack-merge')
const prodEnv = require('./prod.env')

module.exports = merge(prodEnv, {
  NODE_ENV: '"development"',//开发环境
  ENV_CONFIG: '"dev"',
  key: '123456',
  value: '123456',
  API_HOST:"http://192.168.0.106:2892/"
})
