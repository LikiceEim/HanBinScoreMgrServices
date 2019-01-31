/*
 * @Author: Willynn 
 * @Date: 2018-12-15 15:06:03 
 * @Last Modified by: mikey.zhaopeng
 * @Last Modified time: 2019-01-27 23:15:05
 */
import axios from 'axios'
import store from '@/store/store'
import Cookies from 'js-cookie'
import {Modal} from 'iview'  //引入ivew库
import router from '../router'

// console.log(process.env.API_HOST)
// create an axios instance
const service = axios.create({
	// baseURL: process.env.API_HOST, // api的base_url
	// baseURL: "http://localhost:2892/", // api的base_url
	// baseURL: "http://111.231.200.224:8842/", // api的base_url   
	baseURL: "http://jfapi.51jtb.cn/",
	// baseURL: "http://192.168.0.105:2892/", // api的base_url
	// baseURL: "http://integralapi.elaocloud.com/"
	// baseURL: "http://jfapi.51jtb.cn/"
	// timeout: 30000 // request timeout
})

// request interceptor
service.interceptors.request.use(config => {
	// Do something before request is sent
	// config.headers['key'] = process.env.key;// key 用于安全性验证
	// config.headers['value'] = process.env.value;// value 用于安全性验证
	// config.headers['Access-Control-Allow-Origin'] = '*';
	if(config.method === 'post') {
		if(config.data === undefined){
			config.data = {};
			config.data.Token = Cookies.get('token');
		}else{
			config.data.Token = Cookies.get('token');
		}
		if(config.url.indexOf('HanBinScoreService.svc/UploadFile') < 0){
		// if(config.url.indexOf('HanBin/ScoreService/UploadFile') < 0){
			// config.data = JSON.stringify(config.data);
			config.headers['Content-Type'] = 'application/x-www-form-urlencoded;charset=UTF-8';
		}
		// if (config.url.indexOf('HanBin/DownloadService/downloadFile') !== -1) {
		if (config.url.indexOf('DownloadService.svc/downloadFile') !== -1) {
			config.responseType = 'blob';
			// config.headers['Content-Type'] = 'application/x-www-form-urlencoded;charset=UTF-8';
		}
	// config.headers['token'] = Cookies.get('token');
	
	// config.headers.set('token', Cookies.get('token'))
    // config.data = config.data;
  }
  return config;  //添加这一行
}, error => {
	// Do something with request error
  Promise.reject(error)
})

// respone interceptor
service.interceptors.response.use(
	response => {
		/**
     * 下面的注释为通过response自定义code来标示请求状态，当code返回如下情况为权限有问题，登出并返回到登录页
     * 如通过xmlhttprequest 状态码标识 逻辑可写在下面error中
     */
		// if(response.request.responseURL.indexOf('HanBin/DownloadService/downloadFile') !== -1){
		if(response.request.responseURL.indexOf('DownloadService.svc/downloadFile') !== -1){
			return response;
		}
		// 如果是JWT_ERR,跳转到登录页面
		let res;
		if(response.data.Reason == 'JWT_ERR'){
			
			Modal.confirm({
				width:300,
				title:'请退出重新登录',
				content:'您的登录已超时，请退出重新登录！',
				onOk(){
					router.push("/Login");
				}
			});
			return false;
		}
		res = response.data;
		return response.data;
	}, error => {
		return Promise.reject(error)
	}
)

export default service
  