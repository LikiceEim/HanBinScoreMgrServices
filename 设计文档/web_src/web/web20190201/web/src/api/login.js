/*
 * @Author: Willynn 
 * @Date: 2018-12-15 15:58:28 
 * @Last Modified by: mikey.zhaopeng
 * @Last Modified time: 2019-01-21 23:43:49
 */
import request from '@/utils/request'

export function loginFun(query) {
  debugger;
  return request({
    url: 'HanBinSystemService.svc/Login',
    //url: 'HanBin/SystemService/Login',
    method: 'post',
    data: query
  })
}
