// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
// 引入vuex  add by lwj---2018.12.15
import store from './store/store'
Vue.config.productionTip = false

//引入iview
import iView from 'iview'  //引入ivew库
import 'iview/dist/styles/iview.css'  // 使用 CSS 引入样式
Vue.use(iView)

// 引入公共样式
// import "@/assets/css/base.css"
import "../static/css/base.css"

//引入axios
import axios from "axios";
import Cookies from 'js-cookie';
Vue.prototype.$axios = axios;

// 引入md5加密
import md5 from 'js-md5';
Vue.prototype.$md5 = md5;

// 引入moment插件
import moment from 'moment'
Vue.prototype.$moment = moment;

// Vue.prototype.$http= axios

//开发和生产 环境选择
//默认配置
//axios.defaults.baseURL = 'http://localhist:3000'
//axios.defaults.baseURL = '/api'

// 引入echarts
 import echarts from 'echarts'
 Vue.prototype.$echarts = echarts;

/* eslint-disable no-new */
//import 'babel-polyfill'//promise给IE做下兼容性处理//npm install --save babel-polyfill

new Vue({
  el: '#app',
  router,
  store,// 引入vuex add by lwj---2018.12.15
  components: { App },
  template: '<App/>'
})
